import React from "react";
import PropTypes from "prop-types";
import { GlobalValues } from "../../infrastructure/utilities";
import { Link } from "react-router-dom";

const ProductGridItem = ({ product }) => {
  return (
    <div className="col-lg-4 col-sm-6 mb-4 hover-animate">
      <div className="card shadow border-0 h-100">
        <Link to={`/view/${product.slug}`}>
          <img
            src={`${GlobalValues.ContentFolderUrl}productpic/${product.pic}`}
            alt={product.title}
            className="img-fluid card-img-top"
          />
        </Link>
        <div className="card-body">
          <span className="text-muted text-sm">{product.cityTitle}</span>
          <h5 className="my-2">
            <Link to={`/view/${product.slug}`} className="text-dark">
              {product.title}
            </Link>
          </h5>
          <p className="my-2 text-muted text-sm">{product.description}</p>
          <Link to={`/view/${product.slug}`} className="btn btn-link pl-0">
            مشاهده
            <i className="fa fa-long-arrow-alt-right ml-2"></i>
          </Link>
        </div>
      </div>
    </div>
  );
};

ProductGridItem.propTypes = {
  product: PropTypes.object.isRequired
};

export default ProductGridItem;
