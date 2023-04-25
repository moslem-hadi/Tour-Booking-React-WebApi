import React from "react";
import PropTypes from "prop-types";

const CitySelect = ({ cities, onChange }) => {
  var val = new URLSearchParams(window.location.search).get("city");
  return (
    <select
      name="city"
      title="شهر"
      data-style="btn-form-control"
      className="selectpicker1 form-control"
      onChange={onChange}
      defaultValue={val}
    >
      <option value="">شهر را انتخاب کنید</option>
      {cities.map(item => {
        return (
          <option value={item.value} key={item.value}>
            {item.title}
          </option>
        );
      })}
    </select>
  );
};

CitySelect.propTypes = {
  onChange: PropTypes.func.isRequired,
  cities: PropTypes.array.isRequired
};

export default CitySelect;
