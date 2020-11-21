import axios from "axios";
import { toast } from "react-toastify";
import AuthService from "./AuthService";

axios.interceptors.request.use(
  (config) => {
    config.headers["Authorization"] = AuthService.getAuthToken();
    return config;
  },
  (error) => {
    console.log("Unable to complete request. Error: " + error);
    return Promise.reject(error);
  }
);

axios.interceptors.response.use(undefined, (error) => {
  var msg = "";
  console.log(error.message);
  if (error.message === "Network Error" && !error.response) {
    toast.error("Error connecting with server");
  }
  if (error.response) {
    switch (error.response.status) {
      case 401:
        localStorage.removeItem('user');
        msg = "User not authorized";
        toast.error(msg);
        break;
      case 404:
        toast.error("Entity not found");
        break;
      default:
        throw error;
    }
  }
  return Promise.reject(error);
});