export interface User {
  displayName: string;
  username: string;
  token: string;
  budgetName: string;
}

export interface UserFormValues{
  email: string,
  password: string,
  confirmPassword?: string
  displayName?: string,
  username?: string,
  budgetName?: string
}

export interface PasswordChangeFormValues {
  oldPassword: string,
  newPassword: string
}