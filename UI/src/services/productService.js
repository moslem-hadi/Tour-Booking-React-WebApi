import http from "./httpService";
import { GlobalValues } from "../infrastructure/utilities";

const apiUrl = GlobalValues.ApiUrl;

http.setAuthorization();

export function getProducts(productType, count) {
  //set count to 0 if you want all of the data
  return http.post(apiUrl + `/product/GetProducts`, {
    productType,
    count
  });
}

export function getProduct(slug) {
  return http.get(apiUrl + `/product/GetProduct?slug=${slug}`);
}
export function searchProducts(
  ProductType,
  Date,
  CompType, //همراهی
  CityId, //شهر
  Vehicle, //نقلیه
  ActType, //گروهی یا دربستی
  Place //فروگاه، راه آهن و..
) {
  //set count to 0 if you want all of the data
  return http.post(apiUrl + `/product/SearchProducts`, {
    ProductType,
    Date,
    CompType,
    CityId,
    Vehicle,
    ActType,
    Place
  });
}
