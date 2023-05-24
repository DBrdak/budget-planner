import { makeAutoObservable, reaction } from "mobx";
import { FutureExpenditure } from "../models/spendingPlan/futureExpenditure";
import { FutureIncome } from "../models/spendingPlan/futureIncome";
import { FutureSaving } from "../models/spendingPlan/futureSaving";
import agent from "../api/agent";

export default class SpendingPlanStore {
  incomeRegistry = new Map<string, FutureIncome>()
  expenditureRegistry = new Map<string, FutureExpenditure>()
  savingRegistry = new Map<string, FutureSaving>()
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

  loadIncomes = async (date: Date) => {
    this.loading = true
    try{
      const formattedDate = date.toISOString();
      const incomes = await agent.SpendingPlan.getFutureIncomes(formattedDate)
      incomes.forEach(income => {
        this.incomeRegistry.set(income.id, income)
      })
      this.loading = false
    } catch (error){
      console.log(error);
      this.loading = false;
    }
  }

  loadExpenditures = async (date: Date) => {
    this.loading = true
    try{
      const formattedDate = date.toISOString();
      const expenditures = await agent.SpendingPlan.getFutureExpenditures(formattedDate)
      expenditures.forEach(expenditure => {
        this.expenditureRegistry.set(expenditure.id, expenditure)
      })
      this.loading = false
    } catch (error){
      console.log(error);
      this.loading = false;
    }
  }

  loadSavings = async (date: Date) => {
    this.loading = true
    try{
      const formattedDate = date.toISOString();
      const savings = await agent.SpendingPlan.getFutureSavings(formattedDate)
      savings.forEach(saving => {
        this.savingRegistry.set(saving.id, saving)
      })
      this.loading = false
    } catch (error){
      console.log(error);
      this.loading = false;
    }
  }
}