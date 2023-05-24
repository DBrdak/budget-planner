export interface Saving {
  id: string
  amount: number
  date: Date
  fromAccountName: string
  toAccountName: string
  goalName: string
}

export class Saving implements Saving {
  constructor(init?: SavingFormValues) {
    Object.assign(this, init)
  }
}

export class SavingFormValues {
  id?: string = undefined
  amount: number = 0
  date: Date | null = null
  fromAccountName: string = ''
  toAccountName: string = ''
  goalName: string = ''
}