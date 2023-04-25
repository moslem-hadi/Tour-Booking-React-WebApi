import axios from "axios";
import logger from "./logService";
import { Notif, GlobalValues } from "../infrastructure/utilities";

axios.interceptors.response.use(null, error => {
  const expectedError =
    error.response &&
    error.response.status >= 400 &&
    error.response.status < 500;

  if (!expectedError) {
    logger.log(error);
    Notif("خطای ناشناخته ای رخ داد.", "danger", "خطا");
  }

  return Promise.reject(error);
});

function setAuthorization() {
  axios.defaults.headers.common["Authorization"] =
    "Bearer " + GlobalValues.ApiToken;
}

export default {
  get: axios.get,
  post: axios.post,
  put: axios.put,
  delete: axios.delete,
  setAuthorization
};
