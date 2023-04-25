import React from "react";
import PropTypes from "prop-types";

const HamrahiSelect = ({ hamrahiTypes, onChange }) => {
  var val = new URLSearchParams(window.location.search).get("comptype");
  return (
    <select
      name="comptype"
      title="نوع همراهی"
      data-style="btn-form-control"
      className="selectpicker1 form-control"
      onChange={onChange}
      defaultValue={val}
    >
      <option value="">نوع همراهی را انتخاب کنید</option>
      {hamrahiTypes.map(item => {
        return (
          <option value={item.value} key={item.value}>
            {item.title}
          </option>
        );
      })}
    </select>
  );
};

HamrahiSelect.propTypes = {
  onChange: PropTypes.func.isRequired,
  hamrahiTypes: PropTypes.array.isRequired
};

export default HamrahiSelect;
