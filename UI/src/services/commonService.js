import http from "./httpService";
import { GlobalValues } from "../infrastructure/utilities";

const apiUrl = GlobalValues.ApiUrl;

http.setAuthorization();

export function getFirstPageData() {
  return http.get(apiUrl + `/web/GetFirstPageData`);
}

export function getPictures(place, count) {
  //set count to 0 if you want all of the data
  return http.post(apiUrl + `/web/getPictures`, {
    Place: place,
    Count: count
  });
}

export function getMenuLinks(position) {
  return http.get(apiUrl + `/web/GetMenuLinks?position=${position}`);
}

export function submitContactUs(name, mobile, message) {
  return http.post(apiUrl + `/web/SubmitContactUs`, {
    name,
    mobile,
    message
  });
}
