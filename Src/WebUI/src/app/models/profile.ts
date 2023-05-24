export interface Profile {
  username: string
  displayName: string
  email: string
  budgetName: string
}

export class Profile implements Profile {
  constructor(init?: ProfileFormValues) {
    Object.assign(this, init)
  }
}

export class ProfileFormValues {
  username: string = ''
  displayName: string = ''
  email: string = ''
  budgetName: string = ''

  constructor(profile?: Profile) {
    if(profile){
      this.username = profile.username
      this.displayName = profile.displayName
      this.email = profile.email
      this.budgetName = profile.budgetName
    }
  }
}