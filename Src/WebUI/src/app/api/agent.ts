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
import path from "path"
import { Goal, GoalFormValues } from "../models/goal"

const sleep = (delay: number) =>{
  return new Promise((resolve) => {
    setTimeout(resolve, delay)
  })
}

let isBudgetRequired = true

axios.defaults.baseURL = process.env.REACT_APP_API_URL

const responseBody = <T> (response: AxiosResponse<T>) => response.data

axios.interceptors.request.use(config => {
  const token = store.commonStore.token
  const budgetName = store.commonStore.budgetName

  if(budgetName && config.baseURL && isBudgetRequired){
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
      //router.navigate('/not-found')
      toast.error('Not found')
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
  current: () => {
    isBudgetRequired = false
    return requests.get<User>('/user')
  },
  login: (user: UserFormValues) => {
    isBudgetRequired = false
    return requests.post<User>('/user/login', user)
  },
  register: (user: UserFormValues) => {
    isBudgetRequired = false
    return requests.post<User>('/user/register', user)
  }
}

const Profiles = {
  get: (username: string) => {
    isBudgetRequired = true
    return requests.get<Profile>(`/profile/${username}`)
  },
  delete: (password: string) => {
    isBudgetRequired = true
    return requests.put<void>(`/profile`, password)
  },
  update: (username: string, profile: ProfileFormValues) => {
    isBudgetRequired = true
    return requests.put(`/profile/${username}`, profile)
  },
  changePassword: (username: string, form: PasswordChangeFormValues) => {
    isBudgetRequired = true
    return requests.put(`/profile/${username}/password`, form)
  }
}

const Budget = {
  createIncome: (income: IncomeFormValues) => {
    isBudgetRequired = true
    return requests.post<void>('/incomes', income)
  },
  createExpenditure: (expenditure: ExpenditureFormValues) => {
    isBudgetRequired = true
    return requests.post<void>('/expenditures', expenditure)
  },
  createSaving: (saving: SavingFormValues) => {
    isBudgetRequired = true
    return requests.post<void>('/savings', saving)
  },
  deleteIncome: (id: string) => {
    isBudgetRequired = true
    return requests.delete<void>(`/incomes/${id}`)
  },
  deleteExpenditure: (id: string) => {
    isBudgetRequired = true
    return requests.delete<void>(`/expenditures/${id}`)
  },
  deleteSaving: (id: string) => {
    isBudgetRequired = true
    return requests.delete<void>(`/savings/${id}`)
  },
}

const SpendingPlan = {
  getFutureIncomes: (date: string) => {
    isBudgetRequired = true
    return requests.get<FutureIncome[]>(`/spendingplan/incomes/${date}`)
  },
  getFutureExpenditures: (date: string) => {
    isBudgetRequired = true
    return requests.get<FutureExpenditure[]>(`/spendingplan/expenditures/${date}`)
  },
  getFutureSavings: (date: string) => {
    isBudgetRequired = true
    return requests.get<FutureSaving[]>(`/spendingplan/savings/${date}`)
  },  
  createFutureIncome: (futureIncome: FutureIncomeFormValues) => {
    isBudgetRequired = true
    return requests.post<void>('/spendingplan/incomes', futureIncome)
  },
  createFutureExpenditure: (futureExpenditure: FutureExpenditureFormValues) => {
    isBudgetRequired = true
    return requests.post<void>('/spendingplan/expenditures', futureExpenditure)
  },
  createFutureSaving: (futureSaving: FutureSavingFormValues) => {
    isBudgetRequired = true
    return requests.post<void>('/spendingplan/savings', futureSaving)
  },  
  deleteFutureIncome: (id: string) => {
    isBudgetRequired = true
    return requests.delete<void>(`/spendingplan/incomes/${id}`)
  },
  deleteFutureExpenditure: (id: string) => {
    isBudgetRequired = true
    return requests.delete<void>(`/spendingplan/expenditures/${id}`)
  },
  deleteFutureSaving: (id: string) => {
    isBudgetRequired = true
    return requests.delete<void>(`/spendingplan/savings/${id}`)
  },
}

const Extras = {
  getExpenditureCategories: () => {
    isBudgetRequired = true
    return requests.get<Category[]>('/extras/categories/expenditure')
    },
  getIncomeCategories: () => {
    isBudgetRequired = true
    return requests.get<Category[]>('/extras/categories/income')
  },
  getCheckingAccountNames: () => {
    isBudgetRequired = true
    return requests.get<AccountName[]>('/extras/accounts/checking')
  },
  getSavingAccountNames: () => {
    isBudgetRequired = true
    return requests.get<AccountName[]>('/extras/accounts/saving')},
  getGoalNames: () => {
    isBudgetRequired = true
    return requests.get<GoalName[]>('/extras/goals')
  }
}

const Goals = {
  getGoals: () => {
    isBudgetRequired = true
    return requests.get<Goal[]>('/goals')
  },
  deleteGoal: (id: string) => {
    isBudgetRequired = true
    return requests.delete<void>(`/goals/${id}`)
  },
  updateGoal: (updatedGoal: GoalFormValues) => {
    isBudgetRequired = true
    return requests.put<void>(`/goals/${updatedGoal.id}`, updatedGoal)
  },
  createGoal: (newGoal: GoalFormValues) => {
    isBudgetRequired = true
    return requests.post<void>(`/goals`, newGoal)
  },
}

const agent = {
  Users,
  Profiles,
  Budget,
  SpendingPlan,
  Extras,
  Goals,
}

export default agent;