import React from "react";
import { withRouter } from "react-router-dom";
import PropTypes from "prop-types";
import CitySelect from "./CitySelect";
import HamrahiSelect from "./HamrahiSelect";
import ActivityTypeSelect from "./ActivityTypeSelect";
import { ProductTypesEnum } from "../../../infrastructure/models";
import "react-modern-calendar-datepicker/lib/DatePicker.css";
import DatePicker from "react-modern-calendar-datepicker";
import { utils } from "react-modern-calendar-datepicker";

class FirstPageSearch extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      cities: props.cities,
      activityTypes: props.activityTypes,
      hamrahiTypes: props.hamrahiTypes,
      tab: ProductTypesEnum.Transfer,
      selectedCity: null,
      selectedActivityTypes: null,
      selectedHamrahiTypes: null,
      selectedDate: utils("fa").getToday(),
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
  dateChanged = val => {
    var persianDate = `${val.year}/${
      val.month < 10 ? "0" + val.month : val.month
    }/${val.day < 10 ? "0" + val.day : val.day}`;

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

    switch (this.state.tab) {
      case ProductTypesEnum.Transfer: {
        if (this.state.selectedHamrahiTypes)
          url += `comptype=${this.state.selectedHamrahiTypes}`;
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
      default: {
        break;
      }
    }
    if (url.endsWith("&")) url = url.slice(0, -1);

    // window.location = url;
    this.props.history.push(url);
  }
  render() {
    const { cities, activityTypes, hamrahiTypes, selectedDate } = this.state;
    if (cities && activityTypes && hamrahiTypes)
      return (
        <div>
          <ul role="tablist" className="nav nav-tabs search-bar-nav-tabs">
            <li className="nav-item ml-2">
              <button
                data-toggle="tab"
                role="tab"
                className="nav-link active"
                onClick={value =>
                  this.setState({ tab: ProductTypesEnum.Transfer })
                }
              >
                ترانسفر
              </button>
            </li>
            <li className="nav-item ml-2">
              <button
                data-toggle="tab"
                role="tab"
                className="nav-link"
                onClick={value =>
                  this.setState({ tab: ProductTypesEnum.Gasht })
                }
              >
                گشت و دربستی
              </button>
            </li>
            <li className="nav-item">
              <button
                data-toggle="tab"
                role="tab"
                className="nav-link  "
                onClick={value => this.setState({ tab: ProductTypesEnum.Tour })}
              >
                تور
              </button>
            </li>
          </ul>
          <div className="search-bar search-bar-with-tabs p-3 p-lg-4">
            <div className="tab-content">
              <div role="tabpanel" className="tab-pane fade show active">
                <div className="row">
                  <div className="col-md-6 col-lg-3 d-flex align-items-center form-group no-divider">
                    <DatePicker
                      value={selectedDate}
                      onChange={this.dateChanged}
                      shouldHighlightWeekends
                      minimumDate={this.state.minDay}
                      renderInput={this.renderCustomInput} // render a custom input
                      locale="fa" // add this
                    />
                  </div>
                  <div className="col-md-6 col-lg-3 d-flex align-items-center form-group no-divider">
                    <CitySelect
                      cities={cities}
                      onChange={this.cityChanged.bind(this)}
                    />
                  </div>
                  <div
                    className={`col-md-6 col-lg-3 d-flex align-items-center form-group no-divider ${
                      this.state.tab != ProductTypesEnum.Transfer
                        ? "d-none"
                        : ""
                    }`}
                  >
                    <HamrahiSelect
                      hamrahiTypes={hamrahiTypes}
                      onChange={this.hamrahiTypesChanged.bind(this)}
                    />
                  </div>
                  <div
                    className={`col-md-6 col-lg-3 d-flex align-items-center form-group no-divider ${
                      this.state.tab != ProductTypesEnum.Gasht ? "d-none" : ""
                    }`}
                  >
                    <ActivityTypeSelect
                      activityTypes={activityTypes}
                      onChange={this.activityTypesChanged.bind(this)}
                    />
                  </div>
                  <div className="col-md-6 col-lg-3 form-group mb-lg-0 mb-md-3 mr-auto">
                    <button
                      type="button"
                      className="btn btn-primary btn-block h-100"
                      onClick={this.search.bind(this)}
                    >
                      جستجو
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      );
    else return null;
  }
}

FirstPageSearch.propTypes = {};

export default withRouter(FirstPageSearch);
