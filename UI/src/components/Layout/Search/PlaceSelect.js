import React from "react";
import PropTypes from "prop-types";

const PlaceSelect = ({ placeTypes, onChange }) => {
  var val = new URLSearchParams(window.location.search).get("place");
  return (
    <select
      name="comptype"
      title="وسیله نقلیه"
      data-style="btn-form-control"
      className="selectpicker1 form-control"
      onChange={onChange}
      defaultValue={val}
    >
      <option value="">مکان را انتخاب کنید</option>
      {placeTypes.map(item => {
        return (
          <option value={item.value} key={item.value}>
            {item.title}
          </option>
        );
      })}
    </select>
  );
};

PlaceSelect.propTypes = {
  onChange: PropTypes.func.isRequired,
  placeTypes: PropTypes.array.isRequired
};

export default PlaceSelect;
