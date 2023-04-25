import http from "./httpService";
import { GlobalValues } from "../infrastructure/utilities";

const apiUrl = GlobalValues.ApiUrl;

http.setAuthorization();

export function getUserByAuthToken(slug) {
  return http.get(apiUrl + `/web/getUserByAuthToken?token=${slug}`);
}
