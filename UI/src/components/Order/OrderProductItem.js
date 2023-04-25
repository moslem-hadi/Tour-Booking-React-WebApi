import React from "react";
import { GlobalValues } from "../../infrastructure/utilities";
import { Link } from "react-router-dom";
import { ProductTypesEnum } from "../../infrastructure/models";

export const OrderProductItem = ({ item, type, acttype }) => {
  return (
    <div className="media align-items-center">
      <div className="media-body">
        <h6>
          <Link
            to={`/view/${item.product.slug}`}
            target="_blank"
            className="text-reset"
          >
            {item.product.title}
          </Link>
        </h6>
        <ul className="list-unstyled mb-0 mt-3 list-inline">
          <li className="mb-3 list-inline-item">
            <i className="fa fa-users  text-muted ml-2"></i>
            {item.count}
            &nbsp;
            {type === ProductTypesEnum.Transfer ||
            (type === ProductTypesEnum.Gasht && acttype === 1)
              ? "خودرو"
              : "نفر"}
          </li>
          <li className="mb-0 list-inline-item mr-4">
            <i className="fa fa-map-marker  text-muted ml-2"></i>
            {item.product.cityTitle}
          </li>
        </ul>
      </div>
      <Link to={`/view/${item.product.slug}`} target="_blank">
        <img
          src={`${GlobalValues.ContentFolderUrl}productpic/${item.product.pic}`}
          alt={item.product.title}
          width="100"
          className="mr-3 rounded"
        />
      </Link>
    </div>
  );
};
