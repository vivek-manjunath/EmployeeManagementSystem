import axios from "axios";
import AuthService from "./AuthService";

class BaseService {
  api(options) {
    options.headers = AuthService.getAuthHeader();
    return axios(options);
  }
}

export default new BaseService();
