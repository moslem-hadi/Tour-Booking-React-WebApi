import { store } from "react-notifications-component";

export const GlobalValues = {
  ContentFolderUrl:
    process.env.NODE_ENV === "development"
      ? "http://localhost:14143/content/"
      : "http://panel.parhantransfer.ir/content/",
  ApiUrl:
    process.env.NODE_ENV === "development"
      ? "http://localhost:1945/v1"
      : "http://api.parhantransfer.ir/v1",
  AppUrl:
    process.env.NODE_ENV === "development"
      ? "http://localhost:3000"
      : "http://parhantransfer.ir",
  DashboardUrl:
    process.env.NODE_ENV === "development"
      ? "http://localhost:14143"
      : "http://panel.parhantransfer.ir",

  ApiToken: "4134170d5ae2c0e77ed6f84f48a156d47a0c",
  OrderStorageKey: "Order",
  MenuLocalStorageKey: "websiteMenu",
  FooterMenuLocalStorageKey: "websiteFooterMenu",
  LoginTokenCookieName: "AuthToken",
  RequestHeaders: ["Access-Control-Allow-Origin", ""]
};

export function numberWithCommas(x) {
  var parts = x.toString().split(".");
  parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
  return parts.join(".");
}

export function Notif(message, type = "danger", title = null) {
  store.addNotification({
    title: title,
    message: message,
    type: type,
    insert: "top",
    container: "top-right",
    animationIn: ["animated", "fadeIn"],
    animationOut: ["animated", "fadeOut"],
    dismiss: {
      duration: 5000
    }
  });
}
