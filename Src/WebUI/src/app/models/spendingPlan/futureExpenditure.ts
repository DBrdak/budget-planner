import { Expenditure } from "../expenditure"

export interface FutureExpenditure {
  id: string
  category: string
  amount: number
  completedAmount: number
  date: Date | null
  accountName: string
  completedExpenditures: Expenditure[]
}

export class FutureExpenditure implements FutureExpenditure {
  constructor(init?: FutureExpenditureFormValues) {
    Object.assign(this, init)
  }
}

export class FutureExpenditureFormValues {
  id?: string = undefined
  category: string = ''
  amount: number = 0
  date: Date | null = null
  accountName: string = ''
}