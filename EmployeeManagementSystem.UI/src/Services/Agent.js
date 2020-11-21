import axios from "axios";

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

const requests = {
  get: (url) => axios.get(url).then(responseBody),
  post: (url, body) => axios.post(url, body).then(responseBody),
  put: (url, body) => axios.put(url, body).then(responseBody),
  del: (url) => axios.delete(url).then(responseBody),
};

const responseBody = (response) => response.data;

export default { requests };