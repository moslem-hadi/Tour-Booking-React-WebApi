import http from "./httpService";
import { GlobalValues } from "../infrastructure/utilities";

const apiUrl = GlobalValues.ApiUrl;

//http.setAuthorization();

function init() { }

function log(error) {
  var model = {
    Exception: JSON.stringify(error),
    Message: error?.response?.statusText,
    Type: "logService.js",
    Url: error?.config?.url
  };
  //loop
  //http.post(apiUrl + "/web/Log", model);
}

export default {
  init,
  log
};
