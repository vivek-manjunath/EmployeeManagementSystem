import React, { useContext } from "react";
import { Menu, Container, Button } from "semantic-ui-react";
import { NavLink } from "react-router-dom";
import { modalStoreContext } from "../../../Stores/ModalStore";
import { UserStoreContext } from "../../../Stores/UserStore";
import Login from "../../User/Login";
import Register from "../../User/Register";
import { observer } from "mobx-react";

const NavBar = () => {
  const modalStore = useContext(modalStoreContext);
  const userStore = useContext(UserStoreContext);

  const { showModal } = modalStore;
  const { isUserLoggedin, logout } = userStore;

  return (
    <Menu inverted fixed="top">
      <Container>
        <Menu.Item header as={NavLink} exact to="/">
          EMS
        </Menu.Item>
        <Menu.Item as={NavLink} exact to="/employees" name="Employees" />
        <Menu.Item as={NavLink} exact to="/departments" name="Departments" />
        <Menu.Menu position="right">
          {isUserLoggedin === true && (
            <Menu.Item
              as={Button}
              name="login"
              onClick={() => showModal(<Login />)}
            ></Menu.Item>
          )}
          {!isUserLoggedin && (
            <Menu.Item
              as={Button}
              name="logout"
              onClick={() => logout()}
            ></Menu.Item>
          )}
          {isUserLoggedin && (
            <Menu.Item
              as={Button}
              name="register"
              onClick={() => showModal(<Register />)}
            ></Menu.Item>
          )}
        </Menu.Menu>
      </Container>
    </Menu>
  );
};

export default observer(NavBar);
