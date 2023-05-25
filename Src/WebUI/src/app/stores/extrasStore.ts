import { boolean } from "yup";
import { AccountName } from "../models/extras/accountName";
import { Category } from "../models/extras/category";
import agent from "../api/agent";
import { makeAutoObservable } from "mobx";

export default class ExtrasStore {
  checkingAccounts: AccountName[] = []
  savingAccounts: AccountName[] = []
  expenditureCategories: Category[] = []
  incomeCategories: Category[] = []
  loadingAcc: boolean = false
  loadingCat: boolean = false

  constructor(){
    makeAutoObservable(this)
  }

  setLoadingAcc(a: boolean){
    this.loadingAcc = a
  }
  setLoadingCat(a: boolean){
    this.loadingCat = a
  }

  loadCheckingAccounts = async () => {
    this.setLoadingAcc(true)
    try{
      const names = await agent.Extras.getCheckingAccountNames()
      this.checkingAccounts.push(...names)
      this.setLoadingAcc(false)
    } catch (error){
      console.log(error);
      this.setLoadingAcc(false);
    }
  }

  loadSavingAccounts = async () => {
    this.setLoadingAcc(true)
    try{
      const names = await agent.Extras.getSavingAccountNames()
      this.savingAccounts.push(...names)
      this.setLoadingAcc(false)
    } catch (error){
      console.log(error);
      this.setLoadingAcc(false);
    }
  }

  loadExpenditureCategories = async () => {
    this.setLoadingCat(true)
    try{
      const categories = await agent.Extras.getExpenditureCategories()
      this.expenditureCategories.push(...categories)
      this.setLoadingCat(false)
    } catch (error){
      console.log(error);
      this.setLoadingCat(false);
    }
  }

  loadIncomeCategories = async () => {
    this.setLoadingCat(true)
    try{
      const categories = await agent.Extras.getIncomeCategories()
      this.incomeCategories.push(...categories)
      this.setLoadingCat(false)
    } catch (error){
      console.log(error);
      this.setLoadingCat(false);
    }
  }
}