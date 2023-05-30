export interface Income {
  id: string
  amount: number
  title: string
  date: Date
  category: string
  accountName: string
}

export class Income implements Income {
  constructor(init?: IncomeFormValues) {
    Object.assign(this, init)
  }
}

export class IncomeFormValues {
  id?: string = undefined
  amount: number = 0
  title: string = ''
  date: Date | null = null
  category: string = ''
  accountName: string = ''
}