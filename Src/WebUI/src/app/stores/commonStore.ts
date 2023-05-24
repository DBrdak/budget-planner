import { makeAutoObservable, reaction } from "mobx";
import { ServerError } from "../models/serverError";

export default class CommonStore {
  error: ServerError | null = null
  token: string | null = localStorage.getItem('jwt') || null
  budgetName: string | null = localStorage.getItem('budgetName') || null
  appLoaded = false

  constructor() {
    makeAutoObservable(this)

    reaction(
      () => this.token,
      token => {
        if(token){
          localStorage.setItem('jwt', token)

        } else {
          localStorage.removeItem('jwt')
        }
      }
    )

    reaction(
      () => this.budgetName,
      (budgetName) => {
        if (budgetName) {
          localStorage.setItem('budgetName', budgetName);
        } else {
          localStorage.removeItem('budgetName');
        }
      }
    );
  }

  setServerError(error: ServerError) {
    this.error = error
  }

  setToken = (token: string | null) => {
    this.token = token
  }

  setBudgetName = (budgetName: string | null) => {
    this.budgetName = budgetName
  }

  setAppLoaded = () => {
    this.appLoaded = true
  }
}