import React from "react";
import PropTypes from "prop-types";

const VehicleTypeSelect = ({ vehicleTypes, onChange }) => {
  var val = new URLSearchParams(window.location.search).get("vehicle");
  return (
    <select
      name="comptype"
      title="وسیله نقلیه"
      data-style="btn-form-control"
      className="selectpicker1 form-control"
      onChange={onChange}
      defaultValue={val}
    >
      <option value="">وسیله نقلیه را انتخاب کنید</option>
      {vehicleTypes.map(item => {
        return (
          <option value={item.value} key={item.value}>
            {item.title}
          </option>
        );
      })}
    </select>
  );
};

VehicleTypeSelect.propTypes = {
  onChange: PropTypes.func.isRequired,
  vehicleTypes: PropTypes.array.isRequired
};

export default VehicleTypeSelect;
