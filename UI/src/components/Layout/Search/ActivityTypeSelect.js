import React from "react";
import PropTypes from "prop-types";

const ActivityTypeSelect = ({ activityTypes, onChange }) => {
  var val = new URLSearchParams(window.location.search).get("acttype");

  return (
    <select
      name="actType"
      title="نوع فعالیت"
      data-style="btn-form-control"
      className="selectpicker1 form-control"
      onChange={onChange}
      defaultValue={val}
    >
      <option value="">نوع فعالیت را انتخاب کنید</option>
      {activityTypes.map(item => {
        return (
          <option value={item.value} key={item.value}>
            {item.title}
          </option>
        );
      })}
    </select>
  );
};

ActivityTypeSelect.propTypes = {
  onChange: PropTypes.func.isRequired,
  activityTypes: PropTypes.array.isRequired
};

export default ActivityTypeSelect;
