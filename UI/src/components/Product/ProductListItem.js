import React from "react";
import PropTypes from "prop-types";
import { GlobalValues, numberWithCommas } from "../../infrastructure/utilities";

const ProductListItem = ({ product, updateSelectedItems }) => {
  return (
    <div className="list-group-item list-group-item-action p-4 search-list">
      <div className="row">
        <div className="col-lg-4 align-self-center mb-4 mb-lg-0">
          <div className="d-flex align-items-center mb-3">
            <img
              src={`${GlobalValues.ContentFolderUrl}productpic/${product.pic}`}
              alt={product.title}
              className="avatar avatar-sm avatar-border-white ml-3"
            />
            <div>
              <span className="text-sm text-muted">{product.cityTitle}</span>
              <h6 className="h5 mb-0">{product.title}</h6>
            </div>
          </div>

          <div className="custom-control custom-checkbox">
            <input
              type="checkbox"
              id={`select${product.id}`}
              className="custom-control-input"
              // onChange={e => updateSelectedItems(e, product.id, itemCounts)}
              value={product.isSelected}
              onChange={e => {
                product.isSelected = e.target.checked;
              }}
            />
            <label
              htmlFor={`select${product.id}`}
              className="custom-control-label text-black font-weight-bold"
            >
              انتخاب
            </label>
          </div>
        </div>
        <div className="col-lg-8">
          <div className="row">
            <div className="col-6 col-md-8 col-lg-8 py-3 text-justify">
              {product.description}
            </div>
            <div className="col-6 col-lg-4 col-md-4 align-self-center justify-content-center text-center">
              <div className="h5">
                <span className="badge badge-pill p-2 badge-light mb-3 text-muted">
                  قیمت: {numberWithCommas(product.price)} تومان
                </span>
                <h1>x:{product.selectedCount}</h1>
              </div>
              <div className="d-flex align-items-center justify-content-center">
                <span className="text-nowrap ml-3">تعداد نفرات</span>

                <select
                  className="form-control"
                  onChange={e => {
                    product.selectedCount = parseInt(e.target.value);
                  }}
                  defaultValue={product.selectedCount}
                >
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
                </select>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

ProductListItem.propTypes = {
  product: PropTypes.object.isRequired
};

export default ProductListItem;
