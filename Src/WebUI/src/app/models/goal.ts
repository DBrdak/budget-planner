export interface Goal {
  id: string
  name: string
  description: string | null
  endDate : Date
  currentAmount : number
  requiredAmount : number
}

export class Goal implements Goal {
  constructor(init?: GoalFormValues) {
    Object.assign(this, init)
  }
}

export class GoalFormValues {
  id?: string = undefined
  name: string = ''
  description: string | null = null
  endDate : Date | null = null
  currentAmount : number = 0
  requiredAmount : number = 0
}