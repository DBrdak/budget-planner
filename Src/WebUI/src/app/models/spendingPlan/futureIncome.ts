import { Income } from "../income"

export interface FutureIncome {
  id: string
  category: string
  amount: number
  completedAmount: number
  date: Date | null
  accountName: string
  completedIncomes: Income[]
}

export class FutureIncome implements FutureIncome {
  constructor(init?: FutureIncomeFormValues) {
    Object.assign(this, init)
  }
}

export class FutureIncomeFormValues {
  id?: string = undefined
  category: string = ''
  amount: number = 0
  accountName: string = ''
  date: Date | null = null
}