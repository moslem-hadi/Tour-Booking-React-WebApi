import React from "react";

export const NotFoundPage = () => {
  return (
    <section className="py-6">
      <div className="container">
        <div className="py-3 text-center">
          <img src="/assets/img/404-not-found.png" alt="not found" />
          <h4 className="font-size-10 mt-5">404</h4>
          <h4 className="m">صفحه مورد نظر شما پیدا نشد. </h4>
          <p className="text-muted mt-4">
            ممکن است آدرس را اشتباه وارد کرده‌اید، یا صفحه حذف شده باشد.
          </p>
        </div>
      </div>
    </section>
  );
};
