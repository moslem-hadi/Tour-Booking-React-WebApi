import http from "./httpService";
import { GlobalValues } from "../infrastructure/utilities";

const apiUrl = GlobalValues.ApiUrl;

http.setAuthorization();

export function submitOrder(orderInfo, orderDetail, date) {
  return http.post(apiUrl + `/order/SubmitOrder`, {
    orderInfo,
    orderDetail,
    date
  });
}
