import React, { Component } from "react";
import SignleArticleItem from "./SignleArticleItem";
import { Link } from "react-router-dom";

export default class FirstPageArticles extends Component {
  constructor() {
    super();
    var articles = [
      {
        id: 1,
        title: "استفاده از اندروید اتو و اپل کارپلی ، خطرناک‌تر از مصرف الکل",
        img:
          "https://www.digikala.com/mag/wp-content/uploads/2020/03/apple-carplay-hb20-2020_InPixio-60x60.jpg",
        slug: "خطر-اندروید-اتو-و-اپل-کارپلی",
        date: "۲۹ اسفند ۱۳۹۸"
      },
      {
        id: 2,
        title:
          "اپل سرانجام اعتراف کرد ایده مایکروسافت در مورد تبلت‌‌ها درست بود!",
        img:
          "https://www.digikala.com/mag/wp-content/uploads/2020/03/Untitled-2-27-60x60.jpg",
        slug: "شباهت-آیپد-پرو-به-تبلت-سرفیس-پرو",
        date: "۲ فروردین ۱۳۹۹"
      },
      {
        id: 3,
        title: "مایکروسافت تیزری از رابط کاربری جدید ویندوز ۱۰ منتشر کرد",
        img:
          "https://www.digikala.com/mag/wp-content/uploads/2020/03/Untitled-1-38-60x60.jpg",
        slug: "تیزر-رابط-کاربری-جدید-ویندوز-10",
        date: "۲۹ بهمن ۱۳۹۸"
      }
    ];

    this.state = { articles };
  }
  render() {
    const { articles } = this.state;
    if (articles == null || articles.length === 0) return null;
    return (
      <section className="bg-gray-100 pb-5">
        <div className="container">
          <div className="row mb-5">
            <div className="col-md-8">
              <h3>مطالب مفید</h3>
            </div>
            <div className="col-md-4 d-md-flex align-items-center justify-content-end">
              <Link className="text-muted text-sm" to="/blog">
                مشاهده وبلاگ <i className="fa fa-angle-left mr-2"></i>
              </Link>
            </div>
          </div>
          <div className="row">
            {articles == null
              ? ""
              : articles.map(a => {
                  return (
                    <div
                      key={a.id}
                      className="col-12 col-sm-12 col-md-6 col-lg-4"
                    >
                      <SignleArticleItem article={a} />
                    </div>
                  );
                })}
          </div>
        </div>
      </section>
    );
  }
}
