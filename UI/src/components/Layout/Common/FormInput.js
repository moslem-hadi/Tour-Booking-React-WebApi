import React from "react";
const FormInput = ({
  name,
  value,
  type,
  required,
  label,
  onChange,
  colClass,
  inputClass,
  error
}) => {
  return (
    <div className={colClass}>
      <div className="form-group">
        <label className="form-label" htmlFor={name}>
          {label} {required ? <span className="required">*</span> : ""}
        </label>
        <input
          type={type}
          className={`form-control ${inputClass}`}
          name={name}
          value={value}
          onChange={onChange}
        ></input>
        {error && <div class="text-danger validate">{error}</div>}
      </div>
    </div>
  );
};
FormInput.defaultProps = {
  type: "text",
  required: false,
  error: null,
  colClass: "col-12 col-md-6"
};
export default FormInput;
