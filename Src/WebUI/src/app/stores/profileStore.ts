import { makeAutoObservable, runInAction } from "mobx";
import { Profile, ProfileFormValues } from "../models/profile";
import agent from "../api/agent";
import { tr } from "date-fns/locale";

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
      const prof = await agent.Profiles.get(username)
      console.log(prof)
      runInAction(() => {
        this.setProfile(prof)
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

  // usuwanie i zmiana hasła
}