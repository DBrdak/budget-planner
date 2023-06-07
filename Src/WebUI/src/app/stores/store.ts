import { createContext, useContext } from "react";
import ModalStore from "./modalStore";
import CommonStore from "./commonStore";
import SpendingPlanStore from "./spendingPlanStore";
import ExtrasStore from "./extrasStore";
import BudgetStore from "./budgetStore";
import ProfileStore from "./profileStore";
import UserStore from "./userStore";
import GoalsStore from "./goalsStore";

interface Store {
  commonStore: CommonStore
  modalStore: ModalStore
  spendingPlanStore: SpendingPlanStore
  extrasStore: ExtrasStore
  budgetStore: BudgetStore
  profileStore: ProfileStore
  userStore: UserStore
  goalsStore: GoalsStore
}

export const store: Store = {
  commonStore: new CommonStore(),
  modalStore: new ModalStore(),
  spendingPlanStore: new SpendingPlanStore(),
  extrasStore: new ExtrasStore(),
  budgetStore: new BudgetStore(),
  profileStore: new ProfileStore(),
  userStore: new UserStore(),
  goalsStore: new GoalsStore()
}

export const StoreContext = createContext(store)

export function useStore() {
  return useContext(StoreContext)
}