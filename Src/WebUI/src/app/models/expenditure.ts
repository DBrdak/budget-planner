export interface Expenditure {
  id: string
  amount: number
  title: string
  date: Date
  category: string
  accountName: string
}

export class Expenditure implements Expenditure {
  constructor(init?: ExpenditureFormValues) {
    Object.assign(this, init)
  }
}

export class ExpenditureFormValues {
  id?: string = undefined
  amount: number = 0
  title: string = ''
  date: Date | null = null
  category: string = ''
  accountName: string = ''
}