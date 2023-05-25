import axios, { AxiosError, AxiosResponse } from "axios"
import { store } from "../stores/store"
import { error } from "console"
import { router } from "../router/Routes"
import { toast } from "react-toastify"
import { Income, IncomeFormValues } from "../models/income"
import { PasswordChangeFormValues, User, UserFormValues } from "../models/user"
import { SavingFormValues } from "../models/saving"
import { ExpenditureFormValues } from "../models/expenditure"
import { FutureIncome, FutureIncomeFormValues } from "../models/spendingPlan/futureIncome"
import { FutureExpenditure, FutureExpenditureFormValues } from "../models/spendingPlan/futureExpenditure"
import { FutureSaving, FutureSavingFormValues } from "../models/spendingPlan/futureSaving"
import { Category } from "../models/extras/category"
import { Profile, ProfileFormValues } from "../models/profile"
import { AccountName } from "../models/extras/accountName"
import { forEachChild } from "typescript"
import React from "react"
import { GoalName } from "../models/extras/goalName"

const sleep = (delay: number) =>{
  return new Promise((resolve) => {
    setTimeout(resolve, delay)
  })
}

axios.defaults.baseURL = process.env.REACT_APP_API_URL

const responseBody = <T> (response: AxiosResponse<T>) => response.data

axios.interceptors.request.use(config => {
  const token = store.commonStore.token
  const budgetName = store.commonStore.budgetName

  if(budgetName && config.baseURL){
    config.baseURL += `/${budgetName}`
  }

  if(token && config.headers){
    config.headers.Authorization = `Bearer ${token}`
    }
  return config
})

axios.interceptors.response.use(async response => {
  if(process.env.NODE_ENV === 'development') await sleep(1000)
  return response
}, (error: AxiosError) => {
  const {data, status, config} = error.response! as AxiosResponse;
  switch(status) {
    case 400:
      if(config.method === 'get' && data.errors.hasOwnProperty('id')){
        router.navigate('/not-found');
      }
      if(data.errors) {
        const modalStateErrors = [];
        for (const key in data.errors){
          if(data.errors[key]) {
            modalStateErrors.push(data.errors[key]);
            toast.error(data.errors[key][0])
          }
        }
        throw modalStateErrors.flat();
      }else {
        if(data.errors.Date) {
          toast.error(data.errors.Date[0])
        }
        else {
          toast.error(error.status)
        }
      }
      break;
    case 401:
      toast.error('Unauthorized')
      break;
    case 403:
      toast.error('Forbidden')
      break;
    case 404:
      router.navigate('/not-found')
      break;
    case 500:
      store.commonStore.setServerError(data);
      router.navigate('/server-error');
      break;
  }
  return Promise.reject(error);
})

const requests = {
  get: <T> (url: string) => axios.get<T>(url).then(responseBody),
  post: <T> (url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
  put: <T> (url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
  delete: <T> (url: string) => axios.delete<T>(url).then(responseBody)
}

const Users = {
  current: () => requests.get<User>('user'),
  login: (user: UserFormValues) => requests.post<User>('/user/login', user),
  register: (user: UserFormValues) => requests.post<User>('/user/register', user)
}

const Profiles = {
  get: (username: string) => requests.get<Profile>(`profile/${username}`),
  delete: (username: string) => requests.delete<void>(`profile/${username}`),
  update: (username: string, profile: ProfileFormValues) => requests.put(`profile/${username}`, profile),
  changePassword: (username: string, form: PasswordChangeFormValues) => requests.put(`profile/${username}/password`, form)
}

const Budget = {
  createIncome: (income: IncomeFormValues) => requests.post<void>('', income),
  createExpenditure: (expenditure: ExpenditureFormValues) => requests.post<void>('', expenditure),
  createSaving: (saving: SavingFormValues) => requests.post<void>('', saving),
  deleteIncome: (id: string) => requests.delete<void>(`/${id}`),
  deleteExpenditure: (id: string) => requests.delete<void>(`/${id}`),
  deleteSaving: (id: string) => requests.delete<void>(`/${id}`),
}

const SpendingPlan = {
  getFutureIncomes: (date: string) => requests.get<FutureIncome[]>(`/spendingplan/incomes/${date}`),
  getFutureExpenditures: (date: string) => requests.get<FutureExpenditure[]>(`/spendingplan/expenditures/${date}`),
  getFutureSavings: (date: string) => requests.get<FutureSaving[]>(`/spendingplan/savings/${date}`),  
  createFutureIncome: (futureIncome: FutureIncomeFormValues) => requests.post<void>('/spendingplan/incomes', futureIncome),
  createFutureExpenditure: (futureExpenditure: FutureExpenditureFormValues) => requests.post<void>('/spendingplan/expenditures', futureExpenditure),
  createFutureSaving: (futureSaving: FutureSavingFormValues) => requests.post<void>('/spendingplan/savings', futureSaving),  
  deleteFutureIncome: (id: string) => requests.delete<void>(`/spendingplan/incomes/${id}`),
  deleteFutureExpenditure: (id: string) => requests.delete<void>(`/spendingplan/expenditures/${id}`),
  deleteFutureSaving: (id: string) => requests.delete<void>(`/spendingplan/savings/${id}`),
}

const Extras = {
  getExpenditureCategories: () => requests.get<Category[]>('/extras/categories/expenditure'),
  getIncomeCategories: () => requests.get<Category[]>('/extras/categories/income'),
  getCheckingAccountNames: () => requests.get<AccountName[]>('/extras/accounts/checking'),
  getSavingAccountNames: () => requests.get<AccountName[]>('/extras/accounts/saving'),
  getGoalNames: () => requests.get<GoalName[]>('/extras/goals')
}

const agent = {
  Users,
  Profiles,
  Budget,
  SpendingPlan,
  Extras
}

export default agent;