import { observable, action, decorate } from "mobx";
import { createContext } from "react";

class ModalStore {
  modal = {
    open: false,
    content: "",
  };

  showModal = (content) => {
    this.modal.open = true;
    this.modal.content = content;
  };

  closeModal = (content) => {
    this.modal.open = false;
    this.modal.content = "";
  };
}

decorate(ModalStore, {
  modal: observable.shallow,
  showModal: action,
  closeModal: action,
});

export const modalStoreContext = createContext(new ModalStore());
