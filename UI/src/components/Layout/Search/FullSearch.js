import React from "react";
import PropTypes from "prop-types";
import CitySelect from "./CitySelect";
import HamrahiSelect from "./HamrahiSelect";
import ActivityTypeSelect from "./ActivityTypeSelect";
import VehicleTypeSelect from "./VehicleTypeSelect";
import { ProductTypesEnum } from "../../../infrastructure/models";
import { SearchProducts } from "../../../actions/productActions";
import { withRouter } from "react-router-dom";
import { compose } from "redux";
import { connect } from "react-redux";
import "react-modern-calendar-datepicker/lib/DatePicker.css";
import DatePicker from "react-modern-calendar-datepicker";
import { utils } from "react-modern-calendar-datepicker";
import moment from "jalali-moment";
import PlaceSelect from "./PlaceSelect";

class FullSearch extends React.Component {
  render() {
    const {
      cities,
      activityTypes,
      hamrahiTypes,
      type,
      placeTypes,
      selectedDate,
      vehicleTypes
    } = this.state;
    if (cities && activityTypes && hamrahiTypes)
      return (
        <div>
          <div className=" pr-4 pl-4 pt-4 pb-5 mb-2">
            <div className="row">
              <div className="col-lg-3">
                <div className="form-group">
                  <label className="font-weight-bold">تاریخ:</label>

                  <DatePicker
                    value={selectedDate}
                    onChange={this.dateChanged}
                    shouldHighlightWeekends
                    minimumDate={this.state.minDay}
                    renderInput={this.renderCustomInput} // render a custom input
                    locale="fa" // add this
                  />
                </div>
              </div>
              <div className="col-lg-3">
                <div className="form-group">
                  <label className="font-weight-bold">شهر:</label>
                  <CitySelect
                    cities={cities}
                    onChange={this.cityChanged.bind(this)}
                  />
                </div>
              </div>

              <div
                className={`col-md-6 col-lg-3 ${
                  type != ProductTypesEnum.Gasht ? "d-none" : ""
                }`}
              >
                <div className="form-group">
                  <label className="font-weight-bold">نوع :</label>
                  <ActivityTypeSelect
                    activityTypes={activityTypes}
                    onChange={this.activityTypesChanged.bind(this)}
                  />
                </div>
              </div>
              <div
                className={`col-md-6 col-lg-3 ${
                  type != ProductTypesEnum.Transfer ? "d-none" : ""
                }`}
              >
                <div className="form-group">
                  <label className="font-weight-bold">نوع همراهی:</label>
                  <HamrahiSelect
                    hamrahiTypes={hamrahiTypes}
                    onChange={this.hamrahiTypesChanged.bind(this)}
                  />
                </div>
              </div>
              <div
                className={`col-md-6 col-lg-3 ${
                  type != ProductTypesEnum.Transfer ? "d-none" : ""
                }`}
              >
                <div className="form-group">
                  <label className="font-weight-bold">مکان:</label>
                  <PlaceSelect
                    placeTypes={placeTypes}
                    onChange={this.placeTypesChanged.bind(this)}
                  />
                </div>
              </div>
              <div
                className={`col-md-6 col-lg-3 ${
                  type != ProductTypesEnum.Transfer ? "d-none" : ""
                }`}
              >
                <div className="form-group">
                  <label className="font-weight-bold">وسیله نقلیه:</label>
                  <VehicleTypeSelect
                    vehicleTypes={vehicleTypes}
                    onChange={this.vehicleTypesChanged.bind(this)}
                  />
                </div>
              </div>
              <div className="col-12 mt-3 text-center">
                <button
                  type="button"
                  className="btn btn-primary pr-5 pl-5"
                  onClick={this.search.bind(this)}
                >
                  جستجو
                </button>
              </div>
            </div>
          </div>
        </div>
      );
    else return null;
  }

  componentDidUpdate(prevProps) {
    if (prevProps.type !== this.props.type) {
      this.setState({ ...this.state, type: this.props.type });
    }
  }
  constructor(props) {
    super(props);
    var date = new URLSearchParams(window.location.search).get("date");
    let objDate = {};
    if (date)
      objDate = {
        year: parseInt(date.split("-")[0]),
        month: parseInt(date.split("-")[1]),
        day: parseInt(date.split("-")[2])
      };
    this.state = {
      cities: props.cities,
      activityTypes: props.activityTypes,
      hamrahiTypes: props.hamrahiTypes,
      vehicleTypes: props.vehicleTypes,
      placeTypes: props.placeTypes,
      type: props.type,
      selectedCity: null,
      selectedActivityTypes: null,
      selectedHamrahiTypes: null,
      selectedvehicle: null,
      selectedPlace: null,
      selectedDate: date == null ? utils("fa").getToday() : objDate,
      SearchProducts: props.SearchProducts,
      minDay: utils("fa").getToday()
    };
  }
  renderCustomInput = ({ ref }) => (
    <input
      ref={ref} // necessary
      placeholder="تاریخ را انتخاب کنید"
      readOnly
      value={
        this.state.selectedDate
          ? `${this.state.selectedDate.year}/${this.state.selectedDate.month}/${this.state.selectedDate.day}`
          : ""
      }
      className="form-control ltr not-readOnly" // a styling class
    />
  );
  cityChanged(e) {
    this.setState({ selectedCity: e.target.value });
  }
  activityTypesChanged(e) {
    this.setState({ selectedActivityTypes: e.target.value });
  }
  hamrahiTypesChanged(e) {
    this.setState({ selectedHamrahiTypes: e.target.value });
  }
  vehicleTypesChanged(e) {
    this.setState({ selectedvehicle: e.target.value });
  }
  placeTypesChanged(e) {
    this.setState({ selectedPlace: e.target.value });
  }
  dateChanged = val => {
    var persianDate = `${val.year}/${
      val.month < 10 ? "0" + val.month : val.month
    }/${val.day < 10 ? "0" + val.day : val.day}`;

    var dt = moment
      .from(persianDate, "fa", "YYYY/MM/DD")
      .locale("en")
      .format("YYYY/MM/DD");

    this.setState({ selectedDate: val });
  };
  search() {
    let url = "";
    let { selectedDate } = this.state;
    if (this.state.selectedCity) url = `city=${this.state.selectedCity}&`;
    if (selectedDate) {
      var persianDate = `${selectedDate.year}-${
        selectedDate.month < 10 ? "0" + selectedDate.month : selectedDate.month
      }-${selectedDate.day < 10 ? "0" + selectedDate.day : selectedDate.day}`;

      url += `date=${persianDate}&`;
    }
    switch (this.state.type) {
      case ProductTypesEnum.Transfer: {
        if (this.state.selectedHamrahiTypes)
          url += `comptype=${this.state.selectedHamrahiTypes}`;
        if (this.state.selectedvehicle)
          url += `vehicle=${this.state.selectedvehicle}`;
        if (this.state.selectedPlace)
          url += `place=${this.state.selectedPlace}`;
        url = `Transfer?${url}`;
        break;
      }
      case ProductTypesEnum.Gasht: {
        if (this.state.selectedActivityTypes)
          url += `acttype=${this.state.selectedActivityTypes}`;
        url = `Gasht?${url}`;
        break;
      }
      case ProductTypesEnum.Tour: {
        url = `Tour?${url}`;
        break;
      }
      default:
        break;
    }
    if (url.endsWith("&")) url = url.slice(0, -1);

    // window.location = url;
    this.props.history.push(url);
    //فکر کنم نیازی نیست
    var date = new URLSearchParams(window.location.search).get("date");
    var comptype = new URLSearchParams(window.location.search).get("comptype");
    var city = new URLSearchParams(window.location.search).get("city");
    var acttype = new URLSearchParams(window.location.search).get("acttype");
    var vehicle = new URLSearchParams(window.location.search).get("vehicle");
    var place = new URLSearchParams(window.location.search).get("place");

    this.state.SearchProducts(
      this.state.type,
      date,
      comptype,
      city,
      vehicle,
      acttype,
      place
    );
  }
}

FullSearch.propTypes = {
  SearchProducts: PropTypes.func.isRequired
};

//export default withRouter(FullSearch);

const mapStatesToProps = state => ({
  products: state.product
});
export default compose(
  withRouter,
  connect(mapStatesToProps, { SearchProducts })
)(FullSearch);
