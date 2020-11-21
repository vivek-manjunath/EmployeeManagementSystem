import React, { useContext, useEffect, Fragment } from "react";
import "./App.scss";
import Routes from "./Components/Routes";
import { ToastContainer } from "react-toastify";
import ModalContainer from "./Components/Common/ModalContainer";
import { modalStoreContext } from "./Stores/ModalStore";
import { observer } from "mobx-react";
import { UserStoreContext } from "./Stores/UserStore";
import { Container } from "semantic-ui-react";

const App = () => {
  const modalStore = useContext(modalStoreContext);
  const userStore = useContext(UserStoreContext);
  const {
    modal: { open, content },
  } = modalStore;

  const { getCurrentUser } = userStore;

  useEffect(() => {
    getCurrentUser();
  }, [getCurrentUser]);

  return (
    <Container style={{ marginTop: "5rem" }}>
      <ModalContainer
        open={open}
        closeHandler={() => modalStore.closeModal()}
        body={content}
      ></ModalContainer>
      <ToastContainer position="bottom-right"></ToastContainer>
      <Routes></Routes>
    </Container>
  );
};

export default observer(App);
