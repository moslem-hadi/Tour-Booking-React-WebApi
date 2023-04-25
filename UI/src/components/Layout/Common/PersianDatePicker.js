import React from "react";
import moment from "moment-jalaali";
import DatePicker from "react-datepicker2";

export default class PersianDatePicker extends React.Component {
  constructor(props) {
    super(props);
    this.state = { value: moment() };
  }
  render() {
    return (
      <DatePicker
        onChange={value => this.setState({ value })}
        value={this.state.value}
      />
    );
  }
}
