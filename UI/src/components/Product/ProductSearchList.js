import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { SearchProducts } from "../../actions/productActions";
import { GlobalValues, numberWithCommas } from "../../infrastructure/utilities";
import { Redirect, Link } from "react-router-dom";
import { utils } from "react-modern-calendar-datepicker";
import { ProductTypesEnum } from "../../infrastructure/models";

const ProductSearchList = (
  { products: { search_Result, isLoading }, SearchProducts, type },
  props
) => {
  const [selectedItems, setSelectedItem] = useState([]);
  const [startOrder, setstartOrder] = useState(false);
  const [acttype, setActtype] = useState(null);
  //var selectedItems = [];

  const updateSelectedItems = (e, product, justUpdateCount) => {
    var selected = selectedItems.splice(0); //CLONE!
    if (justUpdateCount) {
      let index = selected.findIndex(x => x.product.id === product.id);
      if (index > -1) selected[index].count = parseInt(e.target.value);
    } else {
      let checked = e.target.checked;
      let index = selected.findIndex(x => x.product.id === product.id);
      if (!checked && index > -1) selected.splice(index, 1);
      if (checked)
        selected.push({
          product: product,
          count: parseInt(document.getElementById(`select_${product.id}`).value)
        });
    }

    setSelectedItem(selected);
  };
  useEffect(() => {
    var date = new URLSearchParams(window.location.search).get("date");
    var comptype = new URLSearchParams(window.location.search).get("comptype");
    var city = new URLSearchParams(window.location.search).get("city");
    var acttype = new URLSearchParams(window.location.search).get("acttype");
    var vehicle = new URLSearchParams(window.location.search).get("vehicle");
    var place = new URLSearchParams(window.location.search).get("place");

    SearchProducts(type, date, comptype, city, vehicle, acttype, place);
    setActtype(acttype);
    setSelectedItem([]);
    localStorage.removeItem(GlobalValues.OrderStorageKey);

    //eslint-disable-next-line
  }, [type]);

  const goToOrder = () => {
    var date = utils("fa").getToday();
    var defaultDate = `${date.year}-${
      date.month < 10 ? "0" + date.month : date.month
    }-${date.day < 10 ? "0" + date.day : date.day}`;

    var acttype = new URLSearchParams(window.location.search).get("acttype");

    localStorage.setItem(
      GlobalValues.OrderStorageKey,
      JSON.stringify({
        date:
          new URLSearchParams(window.location.search).get("date") ||
          defaultDate,
        products: selectedItems,
        type,
        acttype
      })
    );
    setstartOrder(true);
  };
  return (
    <div>
      {startOrder ? <Redirect to="/order" /> : null}

      {isLoading || search_Result === null ? (
        <p className="pt-5 pb-5 mt-5 mb-5 text-center">
          <img src="/assets/img/loader01.gif" alt="loading" />
        </p>
      ) : !isLoading && search_Result.length === 0 ? (
        <div className="pt-5 pb-7 text-center font-weight-bold text-muted">
          جستجوی شما نتیجه ای نداشت...
        </div>
      ) : (
        search_Result.map(product => (
          //   <ProductListItem
          //     product={tour}
          //     key={tour.id}
          //     updateSelectedItems={updateSelectedItems}
          //   />

          <div
            className="list-group-item list-group-item-action p-4 search-list"
            key={product.id}
          >
            <div className="row">
              <div className="col-lg-4 align-self-center mb-0 mb-lg-0">
                <div className="d-flex align-items-center mb-3">
                  <img
                    src={`${GlobalValues.ContentFolderUrl}productpic/${product.pic}`}
                    alt={product.title}
                    className="avatar avatar-sm avatar-border-white ml-3 d-none d-md-block"
                  />
                  <div>
                    <span className="text-sm text-muted">
                      {product.cityTitle}
                    </span>
                    <h6 className="h6 mb-0">
                      <Link className="text-dark" to={`/view/${product.slug}`}>
                        {product.title}
                      </Link>
                    </h6>
                  </div>
                </div>

                <div className="custom-control custom-checkbox">
                  <input
                    type="checkbox"
                    id={`select${product.id}`}
                    className="custom-control-input"
                    onClick={e => updateSelectedItems(e, product, false)}
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
                  <div className="col-12 col-md-8 col-lg-8 py-3 text-justify">
                    {product.description}
                  </div>
                  <div className="col-8 col-lg-4 col-md-4 align-self-center justify-content-center text-right text-md-center">
                    <div className="h5">
                      <span className="badge badge-pill p-2 badge-light mb-3 text-muted">
                        قیمت: {numberWithCommas(product.price)} تومان
                      </span>
                    </div>
                    <div className="d-flex align-items-center justify-content-center">
                      <span className="text-nowrap ml-3">
                        {type === ProductTypesEnum.Transfer ||
                        (type === ProductTypesEnum.Gasht && acttype === 1)
                          ? "تعداد خودرو"
                          : "تعداد نفرات"}
                      </span>

                      <select
                        id={`select_${product.id}`}
                        className="form-control"
                        defaultValue={product.selectedCount}
                        onChange={e => updateSelectedItems(e, product, true)}
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
        ))
      )}

      <div
        className={`${
          selectedItems.length == 0 ? "d-none" : ""
        } list-group-item list-group-item-action p-4 clearfix position-sticky bottom-0 bg-light-blue z-index-20`}
      >
        <span className="pt-3 d-inline-block">
          مبلغ کل:
          <b className="ml-2 mr-2">
            {numberWithCommas(
              selectedItems.reduce(
                (prev, next) => prev + next.count * next.product.price,
                0
              )
            )}
          </b>
          <small>تومان</small>
        </span>

        <button
          type="button"
          className="btn btn-primary btn-lg float-left pl-5 pr-5"
          onClick={goToOrder}
        >
          ادامه سفارش
        </button>
      </div>
    </div>
  );
};

ProductSearchList.propTypes = {
  products: PropTypes.object.isRequired,
  SearchProducts: PropTypes.func.isRequired
};

const mapStatesToProps = state => ({
  products: state.product
});

export default connect(mapStatesToProps, { SearchProducts })(ProductSearchList);
