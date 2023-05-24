import { Saving } from "../saving"


export interface FutureSaving {
  id: string
  amount: number
  completedAmount: number
  date: Date | null
  fromAccountName: string
  toAccountName: string
  goalName: string
  completedSavings: Saving[]
}

export class FutureSaving implements FutureSaving {
  constructor(init?: FutureSavingFormValues) {
    Object.assign(this, init)
  }
}

export class FutureSavingFormValues {
  id?: string = undefined
  realAmount: number = 0
  budgetedAmount: number = 0
  date: Date | null = null
  fromAccountName: string = ''
  toAccountName: string = ''
  goalName: string = ''
}