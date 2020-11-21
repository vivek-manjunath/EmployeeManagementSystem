import { observable, computed, decorate, action } from "mobx";
import { createContext } from "react";
import AuthService from "../Services/AuthService";

class UserStore {
  user;

  get isUserLoggedin() {
    return !this.user;
  }

  setUser = (user) => {
    this.user = user;
  };

  login = (user) => {
    return AuthService.login(user).then(
      (response) => {
        this.user = JSON.stringify(response.data);
        localStorage.setItem("user", JSON.stringify(response.data));
      },
      (error) => console.log(error)
    );
  };

  logout = () => {
    localStorage.removeItem("user");
    this.user = null;
  };

  getCurrentUser = () => {
    this.user = localStorage.getItem("user");
  };
}

decorate(UserStore, {
  user: observable,
  isUserLoggedin: computed,
  logout: action,
});

export const UserStoreContext = createContext(new UserStore());
