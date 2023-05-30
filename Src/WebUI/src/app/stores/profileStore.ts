import { makeAutoObservable, runInAction } from "mobx";
import { Profile, ProfileFormValues } from "../models/profile";
import agent from "../api/agent";
import { PasswordChangeFormValues } from "../models/user";

export default class ProfileStore {
  profile:Profile = null
  loading = false

  constructor() {
    makeAutoObservable(this)
  }

  setLoading(a: boolean) {
    this.loading = a
  }

  setProfile(prof: Profile) {
    this.profile = prof
  }

  loadProfile = async (username: string) => {
    this.setLoading(true)
    try {
      this.setProfile(null)
      const prof = await agent.Profiles.get(username)
      this.setProfile(prof)
      runInAction(() => {
        this.setLoading(false)
      })
    } catch (error) {
      console.log(error)
      runInAction(() => this.setLoading(false))
    }
  }

  updateProfile = async (username: string, newProfile: ProfileFormValues ) => {
    this.setLoading(true)
    try {
      await agent.Profiles.update(username, newProfile)
      await this.loadProfile(newProfile.username)
    } catch(error) {
      console.log(error)
    } finally {
      this.setLoading(false)
    }
  }

  deleteProfile = async (password: string) => {
    this.setLoading(true)
    try {
      await agent.Profiles.delete(password)
    } catch(error) {
      console.log(error)
    } finally {
      runInAction(() => this.setLoading(false))
    }
  }

  changePassword = async (username: string, passwordForm: PasswordChangeFormValues) => {
    this.setLoading(true)
    try {
      agent.Profiles.changePassword(username, passwordForm)
    } catch(error) {
      console.log(error)
    } finally {
      runInAction(() => this.setLoading(false))
    }
  }

  // usuwanie i zmiana hasła
}