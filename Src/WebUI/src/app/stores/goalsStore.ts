import { makeAutoObservable, runInAction } from "mobx";
import { Goal, GoalFormValues } from "../models/goal";
import agent from "../api/agent";
import { id } from "date-fns/locale";

export default class GoalsStore {
  loading = false
  goals: Goal[] = []

  constructor() {
    makeAutoObservable(this)
  }

  setLoading(a: boolean) {
    this.loading = a
  }

  getGoals = async () => {
    this.setLoading(true)
    try {
      const response = await agent.Goals.getGoals()
      this.goals = response
    } catch (error) {
      console.log(error)
    } finally {
      runInAction( () => this.setLoading(false))
    }
  }

  clearGoals = () => {
    this.goals = []
  }

  deleteGoal = async (id: string) => {
    this.setLoading(true)
    try {
      await agent.Goals.deleteGoal(id)
    } catch (error) {
      console.log(error)
    } finally {
      runInAction( () => this.setLoading(false))
    }
  }

  updateGoal = async(updatedGoal: GoalFormValues) => {
    this.setLoading(true)
    try {
      await agent.Goals.updateGoal(updatedGoal)
    } catch (error) {
      console.log(error)
    } finally {
      runInAction( () => this.setLoading(false))
    }
  }
  
  createGoal = async(newGoal: GoalFormValues) => {
    this.setLoading(true)
    try {
      await agent.Goals.createGoal(newGoal)
    } catch (error) {
      console.log(error)
    } finally {
      runInAction( () => this.setLoading(false))
    }
  }
}