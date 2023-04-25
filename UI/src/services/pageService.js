import http from "./httpService";
import { GlobalValues } from "../infrastructure/utilities";

const apiUrl = GlobalValues.ApiUrl;

http.setAuthorization();

export function getPage(slug) {
  return http.get(apiUrl + `/web/GetPage?slug=${slug}`);
}
