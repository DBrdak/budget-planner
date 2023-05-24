import { makeAutoObservable, reaction, runInAction } from "mobx";
import { FutureExpenditure, FutureExpenditureFormValues } from "../models/spendingPlan/futureExpenditure";
import { FutureIncome, FutureIncomeFormValues } from "../models/spendingPlan/futureIncome";
import { FutureSaving, FutureSavingFormValues } from "../models/spendingPlan/futureSaving";
import agent from "../api/agent";

export default class SpendingPlanStore {
  incomeRegistry = new Map<string, FutureIncome>()
  expenditureRegistry = new Map<string, FutureExpenditure>()
  savingRegistry = new Map<string, FutureSaving>()
  loadingInitial: boolean = false
  loading: boolean = false

  constructor() {
    makeAutoObservable(this)
  }

  get expenditures() {
    return Array.from(this.expenditureRegistry.values())
  }

  get incomes() {
    return Array.from(this.incomeRegistry.values())
  }

  get savings() {
    return Array.from(this.savingRegistry.values())
  }

  setLoadingInitial(a: boolean){
    runInAction(() => this.loadingInitial = a)
  }

  setLoading(a: boolean) {
    runInAction(() => this.loading = a)
  }

  loadIncomes = async (date: Date) => {
    this.setLoadingInitial(true)
    try{
      const formattedDate = date.toISOString();
      const incomes = await agent.SpendingPlan.getFutureIncomes(formattedDate)
      incomes.forEach(income => {
        this.incomeRegistry.set(income.id, income)
      })
      this.setLoadingInitial(false)
    } catch (error){
      console.log(error);
      this.setLoadingInitial(false);
    }
  }

  loadExpenditures = async (date: Date) => {
    this.setLoadingInitial(true)
    try{
      const formattedDate = date.toISOString();
      const expenditures = await agent.SpendingPlan.getFutureExpenditures(formattedDate)
      expenditures.forEach(expenditure => {
        this.expenditureRegistry.set(expenditure.id, expenditure)
      })
      this.setLoadingInitial(false)
    } catch (error){
      console.log(error);
      this.setLoadingInitial(false);
    }
  }

  loadSavings = async (date: Date) => {
    this.setLoadingInitial(true)
    try{
      const formattedDate = date.toISOString();
      const savings = await agent.SpendingPlan.getFutureSavings(formattedDate)
      savings.forEach(saving => {
        this.savingRegistry.set(saving.id, saving)
      })
      this.setLoadingInitial(false)
    } catch (error){
      console.log(error);
      this.setLoadingInitial(false)
    }
  }

  deleteIncome = async (income: FutureIncome) => {
    this.setLoading(true)
    try {
      await agent.SpendingPlan.deleteFutureIncome(income.id)
      runInAction(() => {
        this.incomeRegistry.delete(income.id)
        this.setLoading(false)
      })
    } catch(error) {
      console.log(error)
      this.setLoading(false)
    }
  }

  deleteExpenditure = async (expenditure: FutureExpenditure) => {
    this.setLoading(true)
    try {
      await agent.SpendingPlan.deleteFutureExpenditure(expenditure.id)
      runInAction(() => {
        this.expenditureRegistry.delete(expenditure.id)
        this.setLoading(false)
      })
    } catch(error) {
      console.log(error)
      this.setLoading(false)
    }
  }

  deleteSaving = async (saving: FutureSaving) => {
    this.setLoading(true)
    try {
      await agent.SpendingPlan.deleteFutureSaving(saving.id)
      runInAction(() => {
        this.savingRegistry.delete(saving.id)
        this.setLoading(false)
      })
    } catch(error) {
      console.log(error)
      this.setLoading(false)
    }
  }

  createIncome = async (income: FutureIncomeFormValues) => {
    this.setLoading(true)
    try {
      await agent.SpendingPlan.createFutureIncome(income)
      const newIncome = new FutureIncome(income)
      newIncome.completedIncomes = []
      this.incomeRegistry.set(newIncome.id, newIncome)
      this.setLoading(false)
    } catch(error) {
      console.log(error)
      this.setLoading(false)
    }
  }

  createExpenditure = async (expenditure: FutureExpenditureFormValues) => {
    this.setLoading(true)
    try {
      await agent.SpendingPlan.createFutureExpenditure(expenditure)
      const newExpenditure = new FutureExpenditure(expenditure)
      newExpenditure.completedAmount = 0
      newExpenditure.completedExpenditures = []
      this.expenditureRegistry.set(newExpenditure.id, newExpenditure)
      this.setLoading(false)
    } catch(error) {
      console.log(error)
      this.setLoading(false)
    }
  }

  createSaving = async (saving: FutureSavingFormValues) => {
    this.setLoading(true)
    try {
      await agent.SpendingPlan.createFutureSaving(saving)
      const newSaving = new FutureSaving(saving)
      newSaving.completedSavings = []
      this.savingRegistry.set(newSaving.id, newSaving)
      this.setLoading(false)
    } catch(error) {
      console.log(error)
      this.setLoading(false)
    }
  }

  clearAll = () => {
    this.expenditureRegistry.clear()
    this.incomeRegistry.clear()
    this.savingRegistry.clear()
  }
}