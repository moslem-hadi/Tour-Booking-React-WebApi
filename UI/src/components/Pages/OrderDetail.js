import React, { Component } from "react";
import { GlobalValues, numberWithCommas } from "../../infrastructure/utilities";
import { OrderSummary } from "../Order/OrderSummary";
import { OrderProductItem } from "../Order/OrderProductItem";
import Cookies from "universal-cookie";
import { getUserByAuthToken } from "../../services/userService";
import { Link } from "react-router-dom";

export default class OrderDetail extends Component {
  async componentDidMount() {
    const cookies = new Cookies();
    var authToken = cookies.get(GlobalValues.LoginTokenCookieName);
    if (authToken != null && authToken != undefined) {
      const {
        data: { data: user }
      } = await getUserByAuthToken(authToken);
      this.setState({
        loggedInUser: user
      });
    }
  }
  constructor() {
    super();
    document.title = "اطلاعات سفارش";
    var products = localStorage.getItem(GlobalValues.OrderStorageKey);
    if (products == null || products == undefined) {
      window.location = "/";
      return;
    }
    var formData = JSON.parse(localStorage.getItem("formData"));

    formData.outDate =
      formData.outDate == null || formData.outDate === ""
        ? null
        : `${formData.outDate.year}/${
            formData.outDate.month < 10
              ? "0" + formData.outDate.month
              : formData.outDate.month
          }/${
            formData.outDate.day < 10
              ? "0" + formData.outDate.day
              : formData.outDate.day
          }`;

    formData.inDate =
      formData.inDate == null || formData.inDate === ""
        ? null
        : `${formData.inDate.year}/${
            formData.inDate.month < 10
              ? "0" + formData.inDate.month
              : formData.inDate.month
          }/${
            formData.inDate.day < 10
              ? "0" + formData.inDate.day
              : formData.inDate.day
          }`;
    var parsed = JSON.parse(products);
    this.state = {
      products: parsed.products,
      date: parsed.date,
      formData: formData,
      online: false
    };
  }
  render() {
    let { formData, products, date } = this.state;
    let discountPrice = products.reduce(
      (prev, next) =>
        prev + next.count * (next.product.price - 0.1 * next.product.price),
      0
    );
    let price = products.reduce(
      (prev, next) => prev + next.count * next.product.price,
      0
    );
    return (
      <section className="py-6">
        <div className="container">
          <div className="row">
            {/* <div className="col-12 text-center">
              <h1 className="h2">اطلاعات سفارش</h1>
            </div> */}
            <div className="col-lg-6 col-sm-8 col-12 m-auto">
              <div className="card bg-gray-100 w-100 position-relative shadow">
                <div className="ribbon ribbon-danger">پرداخت نشده</div>
                <div className="ribbon ribbon-success d-none">پرداخت شده</div>

                <div className="card-body">
                  {/* <i className="fa fa-bookmark text-muted h1 ml-3 mb-0"></i> */}

                  <h5 className="mb-4">اطلاعات سفارش</h5>

                  <p className="d-flex justify-content-between align-items-center">
                    <span>
                      <i className="fa fa-barcode"></i> شناسه سفارش
                    </span>
                    <b>3254</b>
                  </p>

                  <p className="d-flex justify-content-between align-items-center">
                    <span>
                      <i className="fa fa-calendar"></i> تاریخ ثبت سفارش
                    </span>
                    <b>1398/12/22</b>
                  </p>
                  <p className="d-flex justify-content-between align-items-center">
                    <span>
                      <i className="fa fa-user"></i> نام و نام خانوادگی مسافر
                    </span>
                    <b>مسلم هادی</b>
                  </p>

                  <p className="d-flex justify-content-between align-items-center">
                    <span>
                      <i className="fa fa-mobile"></i> شماره تماس
                    </span>
                    <b>09150650700</b>
                  </p>
                  <p className="d-flex justify-content-between align-items-center">
                    <span>
                      <i className="fa fa-check"></i> تاریخ رزرو
                    </span>
                    <b>1399/01/08</b>
                  </p>

                  <hr
                    className="mt-3"
                    style={{ marginRight: "-1.25rem", marginLeft: "-1.25rem" }}
                  />
                  <h6 className="text-muted">محصولات این سفارش</h6>
                  {products.map((item, index) => (
                    <div
                      key={index}
                      className="d-flex align-items-center justify-content-start mt-4 "
                    >
                      <div className="font-weight-bold">{index + 1})</div>
                      <div className="media-body  mr-4 ">
                        <h6>
                          <Link
                            to={`/view/${item.product.slug}`}
                            target="_blank"
                            className="text-reset"
                          >
                            {item.product.title}
                          </Link>
                        </h6>

                        <div className="d-flex align-items-center justify-content-between">
                          <div>
                            {item.count}
                            <span className=" text-muted ml-2 mr-2">×</span>
                            {numberWithCommas(item.product.price)}&nbsp;
                            <small>تومان</small>
                          </div>
                          <div className="text-left">
                            مجموع:&nbsp;
                            <b>
                              {numberWithCommas(
                                item.count * item.product.price
                              )}
                              &nbsp;
                              <small>تومان</small>
                            </b>
                          </div>
                        </div>
                      </div>
                    </div>
                  ))}
                  <div className="text-left mt-4 font-weight-bold text-primary">
                    مجموع سفارش: {numberWithCommas(price)}
                    &nbsp;<small>تومان</small>
                  </div>
                </div>

                <div className="card-footer">
                  <div>
                    <div className="form-group">
                      <label className="font-weight-bold h5">نحوه پرداخت</label>
                      <div className="pr-4">
                        <div className="custom-control custom-radio">
                          <input
                            type="radio"
                            id="guests_0"
                            name="guests"
                            checked={!this.state.online}
                            className="custom-control-input"
                            value={!this.state.online}
                            onChange={() => this.setState({ online: false })}
                          />
                          <label
                            htmlFor="guests_0"
                            className="custom-control-label"
                          >
                            پرداخت اعتباری
                          </label>
                        </div>
                        <div className="custom-control custom-radio">
                          <input
                            type="radio"
                            id="guests_1"
                            name="guests"
                            className="custom-control-input"
                            value={this.state.online}
                            onChange={() => this.setState({ online: true })}
                          />
                          <label
                            htmlFor="guests_1"
                            className="custom-control-label"
                          >
                            پرداخت آنلاین از طریق درگاه سایت (شامل 10 درصد
                            تخفیف)
                          </label>
                        </div>
                      </div>
                    </div>

                    <div className="text-center">
                      <span className="pl-4">مبلغ قابل پرداخت:</span>
                      <b className="h5">
                        {numberWithCommas(
                          this.state.online ? discountPrice : price
                        )}
                        <small className="mr-1 ml-4">تومان</small>

                        {this.state.online ? (
                          <span className="text-danger small">
                            <br />
                            {numberWithCommas(price - discountPrice)}{" "}
                            <span>تومان</span> تخفیف
                          </span>
                        ) : (
                          ""
                        )}
                      </b>
                      <br />
                      <button
                        type="button"
                        className="btn btn-success btn-lg pl-5 pr-5 mb-3 mt-3"
                      >
                        اتصال به درگاه
                      </button>
                      <p className="text-muted small">
                        پرداخت از طریق تمام کارت های بانکی با رمز دوم فعال
                        امکانپذیر است.
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    );
  }
}
