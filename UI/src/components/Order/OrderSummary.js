import React from "react";
import { OrderProductItem } from "../Order/OrderProductItem";
import { numberWithCommas } from "../../infrastructure/utilities";

export const OrderSummary = ({ products, date, type, acttype }) => {
  return (
    <div className="card border-0 shadow">
      <div className="card-header bg-light py-3 border-0">
        <span className="h6 m-0 text-dark d-block text-center">
          خلاصه سفارش شما
        </span>
      </div>

      <div className="card-body pt-4 pr-4 pl-4 pb-0">
        <div className="pb-3">
          {products.map(item => (
            <OrderProductItem
              item={item}
              key={item.product.id}
              type={type}
              acttype={acttype}
            />
          ))}
        </div>
      </div>
      <div className="card-footer pr-4 pb-4 pl-4 pt-0">
        <div className="pt-3 pb-0 d-flex align-items-center justify-content-between">
          <span>تاریخ</span>
          <b>{date}</b>
        </div>
        <div className="pt-3 pb-0 d-flex align-items-center justify-content-between">
          <b>مبلغ کل</b>
          <b>
            {numberWithCommas(
              products.reduce(
                (prev, next) => prev + next.count * next.product.price,
                0
              )
            )}
            <small className="mr-1">تومان</small>
          </b>
        </div>
      </div>
    </div>
  );
};
