import { makeAutoObservable, runInAction } from "mobx";
import { User } from "../models/user";
import agent from "../api/agent";
import { store } from "./store";

export default class UserStore {
  user: User | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  getUser = async () => {
    try {
      const user = await agent.Users.current();
      store.commonStore.setToken(user.token);
      runInAction(() => this.user = user);
    } catch(error) {
      console.log(error);
    }
  }
}