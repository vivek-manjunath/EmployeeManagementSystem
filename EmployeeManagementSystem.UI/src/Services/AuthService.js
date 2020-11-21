import axios from "axios";

const API_URL = process.env.REACT_APP_API_AUTH_URL;

class AuthService {
  login(user) {
    return axios.post(API_URL + "login", user);
  }

  logout() {
    localStorage.removeItem("user");
  }

  register(registrationDto) {
    return axios
      .post(API_URL + "register", registrationDto)
      .then((response) => {
        localStorage.setItem("user", JSON.stringify(response.data));
      });
  }

  getCurrentUser() {
    return localStorage.getItem("user");
  }

  getAuthHeader() {
    const user = JSON.parse(localStorage.getItem("user"));

    if (user) {
      return { Authorization: "Bearer " + user.accessToken };
    }
    return {};
  }

  getAuthToken() {
    const user = JSON.parse(localStorage.getItem("user"));

    if (user) {
      return "Bearer " + user.accessToken;
    }
    return "";
  }
}

export default new AuthService();
