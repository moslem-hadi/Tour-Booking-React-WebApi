import React, { useEffect } from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { GetLatestTours } from "../../actions/productActions";
import ProductGridItem from "./ProductGridItem";
import ProductGridLoader from "../Layout/Common/ProductGridLoader";
import { Link } from "react-router-dom";

const LatestTours = ({
  firstPageProductCount,
  products: { latest_Tours, isLoading },
  GetLatestTours
}) => {
  useEffect(() => {
    //GetProducts
    GetLatestTours(firstPageProductCount);
    //eslint-disable-next-line
  }, []);

  return (
    <section className="pb-6 pt-4 bg-gray-100">
      <div className="container overflow-hidden">
        <div className="row mb-5">
          <div className="col-md-8">
            <p className="subtitle text-secondary">یه تور لذت بخش</p>
            <h2>آخرین تورها</h2>
          </div>
          <div className="col-md-4 d-md-flex align-items-center justify-content-end">
            <Link to="/Tour" className="text-muted text-sm">
              مشاهده همه <i className="fa fa-angle-left mr-2"></i>
            </Link>
          </div>
        </div>

        {isLoading || latest_Tours === null ? (
          <ProductGridLoader />
        ) : (
          <div className="row">
            {!isLoading && latest_Tours.length === 0 ? (
              <div className="col-12 mb-4">No product</div>
            ) : (
              latest_Tours.map(tour => (
                <ProductGridItem product={tour} key={tour.id} />
              ))
            )}
          </div>
        )}
      </div>
    </section>
  );
};

LatestTours.propTypes = {
  firstPageProductCount: PropTypes.number.isRequired,
  products: PropTypes.object.isRequired,
  GetLatestTours: PropTypes.func.isRequired
};

const mapStatesToProps = state => ({
  products: state.product
});

export default connect(mapStatesToProps, { GetLatestTours })(LatestTours);
