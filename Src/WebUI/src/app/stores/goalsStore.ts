import { makeAutoObservable } from "mobx";
import { Goal } from "../models/goal";
import { Income } from "../models/income";

export default class GoalsStore {
  loading = false

  constructor() {
    makeAutoObservable(this)
  }

  setLoading(a: boolean) {
    this.loading = a
  }

  //createGoal = async(Goal)
  
  goal: Goal | null = null
}