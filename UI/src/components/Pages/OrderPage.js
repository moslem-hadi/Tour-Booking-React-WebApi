import React, { Component } from "react";
import {
  GlobalValues,
  numberWithCommas,
  Notif
} from "../../infrastructure/utilities";
import { store } from "react-notifications-component";
import { OrderSummary } from "../Order/OrderSummary";
import DatePicker, { utils } from "react-modern-calendar-datepicker";
import TimeInput from "react-time-input";
import FormInput from "../Layout/Common/FormInput";
import { submitOrder } from "../../services/orderService";
import Cookies from "universal-cookie";
import { getUserByAuthToken } from "../../services/userService";

export class OrderPage extends Component {
  async componentDidMount() {
    const cookies = new Cookies();
    var authToken = cookies.get(GlobalValues.LoginTokenCookieName);
    if (authToken != null && authToken != undefined) {
      const {
        data: { data: user }
      } = await getUserByAuthToken(authToken);
      this.setState({
        loggedInUser: user,
        formData: {
          ...this.state.formData,
          registererTell: user.mobile,
          UserId: user.id
        }
      });
    }
  }
  constructor() {
    super();

    document.title = "ثبت سفارش";
    var products = localStorage.getItem(GlobalValues.OrderStorageKey);
    if (products == null || products == undefined) {
      window.location = "/";
      return;
    }
    var parsed = JSON.parse(products);
    var date = parsed.date;
    let objDate = {};
    if (date)
      objDate = {
        year: parseInt(date.split("-")[0]),
        month: parseInt(date.split("-")[1]),
        day: parseInt(date.split("-")[2])
      };

    this.state = {
      products: parsed.products,
      type: parsed.type,
      date: parsed.date,
      acttype: parsed.acttype,
      formData: {
        fullName: "",
        tell: "",
        registererTell: "",
        inDate: null,
        outDate: null,
        adultCount: 1,
        childCount: 0,
        hotel: "",
        hotelAddress: "",
        vehicle: "",
        vehicleNumber: "",
        departureTime: "",
        arriveTime: "",
        userId: null
      },
      minDay: objDate == null ? utils("fa").getToday() : objDate,
      isLoading: false,
      formErrors: {},
      loggedInUser: null
    };
  }

  render() {
    let { formData, loggedInUser: user } = this.state;
    return (
      <section className="py-5">
        <div className="container">
          <div className="row">
            <div className="col-lg-7">
              <h1 className="h2 mb-5">ثبت سفارش</h1>
              <div className="text-block mb-5 pb-4">
                لطفا اطلاعات زیر را به دقت وارد نمایید.
                {user == null ? (
                  ""
                ) : (
                  <span className="pr-1 text-dark">
                    شما با حساب کاربری
                    <b className="text-success"> {user.fullName}</b> وارد شده
                    اید.
                  </span>
                )}
              </div>
              <div className="row">
                <FormInput
                  label="نام و نام خانوادگی مسافر"
                  name="fullName"
                  onChange={this.handleInputChange}
                  required={true}
                  value={formData.fullName}
                />

                <FormInput
                  label="شماره تماس مسافر"
                  name="tell"
                  inputClass="ltr"
                  onChange={this.handleInputChange}
                  required={true}
                  value={formData.tell}
                />
                <FormInput
                  label="شماره تماس سفارش دهنده"
                  name="registererTell"
                  inputClass="ltr"
                  onChange={this.handleInputChange}
                  value={formData.registererTell}
                />

                <div className="col-12 col-md-6">
                  <div className="row">
                    <div className="col-6">
                      <label className="form-label">تعداد بزرگسال</label>

                      <div className="d-flex align-items-center justify-content-center">
                        <select
                          className="form-control"
                          value={formData.adultCount}
                          onChange={this.setAdultCount}
                        >
                          <option value="0">0</option>
                          <option value="1">1</option>
                          <option value="2">2</option>
                          <option value="3">3</option>
                          <option value="4">4</option>
                          <option value="5">5</option>
                          <option value="6">6</option>
                          <option value="7">7</option>
                          <option value="8">8</option>
                          <option value="9">9</option>
                          <option value="10">10</option>
                          <option value="11">11</option>
                          <option value="12">12</option>
                          <option value="13">13</option>
                          <option value="14">14</option>
                          <option value="15">15</option>
                        </select>
                      </div>
                    </div>

                    <div className="col-6">
                      <label className="form-label">تعداد کودک</label>

                      <div className="d-flex align-items-center justify-content-center">
                        <select
                          className="form-control"
                          value={formData.childCount}
                          onChange={this.setChildCount}
                        >
                          {" "}
                          <option value="0">0</option>
                          <option value="1">1</option>
                          <option value="2">2</option>
                          <option value="3">3</option>
                          <option value="4">4</option>
                          <option value="5">5</option>
                          <option value="6">6</option>
                          <option value="7">7</option>
                          <option value="8">8</option>
                          <option value="9">9</option>
                          <option value="10">10</option>
                          <option value="11">11</option>
                          <option value="12">12</option>
                          <option value="13">13</option>
                          <option value="14">14</option>
                          <option value="15">15</option>
                        </select>
                      </div>
                    </div>
                  </div>
                </div>

                <div className="clearfix col-12"></div>

                <div className="col-12 col-md-4">
                  <div className="form-group">
                    <label className="form-label">تاریخ ورود</label>

                    <DatePicker
                      value={formData.inDate}
                      onChange={this.setInDate}
                      shouldHighlightWeekends
                      minimumDate={this.state.minDay}
                      renderInput={this.renderCustomInputInDate} // render a custom input
                      locale="fa" // add this
                    />
                  </div>
                </div>

                <div className="col-12 col-md-4">
                  <div className="form-group">
                    <label className="form-label">تاریخ خروج</label>

                    <DatePicker
                      value={formData.outDate}
                      onChange={this.setOutDate}
                      shouldHighlightWeekends
                      minimumDate={this.state.minDay}
                      renderInput={this.renderCustomInputOutDate} // render a custom input
                      locale="fa" // add this
                    />
                  </div>
                </div>
                <div className="clearfix col-12"></div>
                <FormInput
                  label="نام هتل محل اقامت"
                  name="hotel"
                  onChange={this.handleInputChange}
                  value={formData.hotel}
                />

                <FormInput
                  label="آدرس هتل"
                  name="hotelAddress"
                  onChange={this.handleInputChange}
                  value={formData.hotelAddress}
                />

                <div className="col-12 col-md-6">
                  <div className="form-group">
                    <label className="form-label">وسیله نقلیه</label>
                    <select
                      name="payment"
                      id="form_payment"
                      data-style="btn-selectpicker"
                      className="form-control mb-3"
                      value={formData.vehicle}
                      onChange={this.setVehicle}
                    >
                      <option value="">انتخاب کنید</option>
                      <option value="1">اتوبوس</option>
                      <option value="2">قطار</option>
                      <option value="3">هواپیما</option>
                    </select>
                  </div>
                </div>
                {formData.vehicle === "" ? null : (
                  <div className="col-12 col-md-6">
                    <div className="form-group">
                      <label className="form-label">
                        شماره &nbsp;
                        {formData.vehicle === "1"
                          ? "اتوبوس"
                          : formData.vehicle === "2"
                          ? "قطار"
                          : "هواپیما"}
                      </label>
                      <input
                        type="text"
                        className="form-control"
                        value={formData.vehicleNumber}
                        onChange={this.setvehicleNumber}
                      ></input>
                    </div>
                  </div>
                )}

                <div className="clearfix col-12"></div>

                <div className="col-12 col-md-6">
                  <div className="form-group">
                    <label className="form-label">ساعت حرکت</label>

                    <TimeInput
                      ref="TimeInputWrapper"
                      className="form-control ltr"
                      mountFocus={false}
                      value={formData.departureTime}
                      onTimeChange={this.setDepartureTime}
                    />
                  </div>
                </div>

                <div className="col-12 col-md-6">
                  <div className="form-group">
                    <label className="form-label">ساعت رسیدن</label>

                    <TimeInput
                      ref="TimeInputWrapper"
                      className="form-control ltr"
                      mountFocus={false}
                      value={formData.arriveTime}
                      onTimeChange={this.setArriveTime}
                    />
                  </div>
                </div>
                <div className="col-12 text-center mt-4">
                  <button
                    type="button"
                    className="btn btn-primary px-3 btn-lg"
                    onClick={this.submit}
                    disabled={this.state.isLoading}
                  >
                    {this.state.isLoading ? (
                      <span>
                        لطفا صبر کنید
                        <i className="fa fa-circle-notch fa-spin mr-2"></i>
                      </span>
                    ) : (
                      <span>
                        ثبت سفارش <i className="fa-chevron-left fa mr-2"></i>
                      </span>
                    )}
                  </button>
                </div>
              </div>
            </div>
            <div className="col-lg-5 pr-xl-5">
              <OrderSummary
                products={this.state.products}
                date={this.state.date}
                type={this.state.type}
                acttype={this.state.acttype}
              />
            </div>
          </div>
        </div>
      </section>
    );
  }

  submit = () => {
    let { formData, products, date } = this.state;
    if (formData.fullName.length === 0 || formData.tell.length === 0) {
      Notif("لطفا تمام فیلدهای ستاره دار را پر کنید.", "danger", "توجه!");
      return;
    }

    if (formData.childCount === 0 && formData.adultCount === 0) {
      Notif("لطفا تعداد بزرگسال و کودک را مشخص نمایید.", "danger", "توجه!");
      return;
    }

    var orderDetail = [];
    products.map(a => {
      orderDetail.push({
        productId: a.product.id,
        count: a.count,
        productPrice: a.product.price
      });
    });
    let formDataCopy = { ...formData };

    formDataCopy.vehicle =
      formDataCopy.vehicle === "" ? null : parseInt(formDataCopy.vehicle);

    formDataCopy.inDate =
      formDataCopy.inDate == null || formDataCopy.inDate === ""
        ? null
        : `${formDataCopy.inDate.year}/${
            formDataCopy.inDate.month < 10
              ? "0" + formDataCopy.inDate.month
              : formDataCopy.inDate.month
          }/${
            formDataCopy.inDate.day < 10
              ? "0" + formDataCopy.inDate.day
              : formDataCopy.inDate.day
          }`;

    formDataCopy.outDate =
      formDataCopy.outDate == null || formDataCopy.outDate === ""
        ? null
        : `${formDataCopy.outDate.year}/${
            formDataCopy.outDate.month < 10
              ? "0" + formDataCopy.outDate.month
              : formDataCopy.outDate.month
          }/${
            formDataCopy.outDate.day < 10
              ? "0" + formDataCopy.outDate.day
              : formDataCopy.outDate.day
          }`;

    this.setState({ isLoading: true });
    (async () => {
      try {
        const { data: result } = await submitOrder(
          formDataCopy,
          orderDetail,
          date
        );

        if (result.success) {
          //این خط بعدا حذف
          localStorage.setItem("formData", JSON.stringify(formData));
          this.props.history.push(`/orderdetail/${result.data.code}`);
        } else {
          Notif(result.message, "danger", "توجه!");
        }
        this.setState({ isLoading: false });
      } catch (error) {
        Notif(
          "خطا در درخواست از سرور. لطفا بعدا مجدد امتحان کنید.",
          "danger",
          "توجه!"
        );
        this.setState({ isLoading: false });
      }
    })();
  };

  renderCustomInputInDate = ({ ref }) => (
    <input
      ref={ref} // necessary
      readOnly
      value={
        this.state.formData.inDate
          ? `${this.state.formData.inDate.year}/${this.state.formData.inDate.month}/${this.state.formData.inDate.day}`
          : ""
      }
      className="form-control ltr not-readOnly" // a styling class
    />
  );
  renderCustomInputOutDate = ({ ref }) => (
    <input
      ref={ref} // necessary
      readOnly
      value={
        this.state.formData.outDate
          ? `${this.state.formData.outDate.year}/${this.state.formData.outDate.month}/${this.state.formData.outDate.day}`
          : ""
      }
      className="form-control ltr not-readOnly" // a styling class
    />
  );
  handleInputChange = ({ currentTarget: input }) => {
    const formData = { ...this.state.formData };
    formData[input.name] = input.value;
    this.setState({ formData });
  };

  setInDate = val => {
    this.setState({
      formData: { ...this.state.formData, inDate: val }
    });
  };
  setOutDate = val => {
    this.setState({
      formData: { ...this.state.formData, outDate: val }
    });
  };
  setVehicle = val => {
    this.setState({
      formData: { ...this.state.formData, vehicle: val.target.value }
    });
  };
  setvehicleNumber = val => {
    this.setState({
      formData: { ...this.state.formData, vehicleNumber: val.target.value }
    });
  };

  setDepartureTime = val => {
    this.setState({
      formData: { ...this.state.formData, departureTime: val }
    });
  };

  setAdultCount = val => {
    this.setState({
      formData: { ...this.state.formData, adultCount: val.target.value }
    });
  };
  setChildCount = val => {
    this.setState({
      formData: { ...this.state.formData, childCount: val.target.value }
    });
  };
  setArriveTime = val => {
    this.setState({
      formData: { ...this.state.formData, arriveTime: val }
    });
  };
}

export default OrderPage;
