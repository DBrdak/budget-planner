import { createContext, useContext } from "react";
import ModalStore from "./modalStore";
import CommonStore from "./commonStore";
import SpendingPlanStore from "./spendingPlanStore";
import ExtrasStore from "./extrasStore";
import BudgetStore from "./budgetStore";
import ProfileStore from "./profileStore";
import UserStore from "./userStore";

interface Store {
  commonStore: CommonStore
  modalStore: ModalStore
  spendingPlanStore: SpendingPlanStore
  extrasStore: ExtrasStore
  budgetStore: BudgetStore
  profileStore: ProfileStore
  userStore: UserStore
}

export const store: Store = {
  commonStore: new CommonStore(),
  modalStore: new ModalStore(),
  spendingPlanStore: new SpendingPlanStore(),
  extrasStore: new ExtrasStore(),
  budgetStore: new BudgetStore(),
  profileStore: new ProfileStore(),
  userStore: new UserStore()
}

export const StoreContext = createContext(store)

export function useStore() {
  return useContext(StoreContext)
}