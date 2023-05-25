import { set } from "date-fns"
import { IncomeFormValues } from "../models/income"
import agent from "../api/agent"
import { ExpenditureFormValues } from "../models/expenditure"
import { SavingFormValues } from "../models/saving"
import { makeAutoObservable } from "mobx"

export default class BudgetStore {
  loading = false

  constructor() {
    makeAutoObservable(this)
  }

  setLoading(a: boolean) {
    this.loading = a
  }

  createIncome = async (income: IncomeFormValues) => {
    this.setLoading(true)
    try {
      await agent.Budget.createIncome(income)
    } catch (error) {
      console.log(error)
    } finally {
      this.setLoading(false)
    }
  }

  createExpenditure = async (expenditure: ExpenditureFormValues) => {
    this.setLoading(true)
    try {
      await agent.Budget.createExpenditure(expenditure)
    } catch (error) {
      console.log(error)
    } finally {
      this.setLoading(false)
    }
  }

  createSaving = async (saving: SavingFormValues) => {
    this.setLoading(true)
    try {
      await agent.Budget.createSaving(saving)
    } catch (error) {
      console.log(error)
    } finally {
      this.setLoading(false)
    }
  }

  deleteIncome = async (id: string) => {
    this.setLoading(true)
    try {
      await agent.Budget.deleteIncome(id)
    } catch (error) {
      console.log(error)
    } finally {
      this.setLoading(false)
    }
  }

  deleteExpenditure = async (id: string) => {
    this.setLoading(true)
    try {
      await agent.Budget.deleteExpenditure(id)
    } catch (error) {
      console.log(error)
    } finally {
      this.setLoading(false)
    }
  }

  deleteSaving = async (id: string) => {
    this.setLoading(true)
    try {
      await agent.Budget.deleteSaving(id)
    } catch (error) {
      console.log(error)
    } finally {
      this.setLoading(false)
    }
  }
}