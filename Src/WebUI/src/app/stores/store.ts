import { createContext, useContext } from "react";
import ModalStore from "./modalStore";
import CommonStore from "./commonStore";
import SpendingPlanStore from "./spendingPlanStore";

interface Store {
  commonStore: CommonStore
  modalStore: ModalStore
  spendingPlanStore: SpendingPlanStore
}

export const store: Store = {
  commonStore: new CommonStore(),
  modalStore: new ModalStore(),
  spendingPlanStore: new SpendingPlanStore()
}

export const StoreContext = createContext(store)

export function useStore() {
  return useContext(StoreContext)
}