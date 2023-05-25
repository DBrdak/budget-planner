import { createContext, useContext } from "react";
import ModalStore from "./modalStore";
import CommonStore from "./commonStore";
import SpendingPlanStore from "./spendingPlanStore";
import ExtrasStore from "./extrasStore";

interface Store {
  commonStore: CommonStore
  modalStore: ModalStore
  spendingPlanStore: SpendingPlanStore
  extrasStore: ExtrasStore
}

export const store: Store = {
  commonStore: new CommonStore(),
  modalStore: new ModalStore(),
  spendingPlanStore: new SpendingPlanStore(),
  extrasStore: new ExtrasStore()
}

export const StoreContext = createContext(store)

export function useStore() {
  return useContext(StoreContext)
}