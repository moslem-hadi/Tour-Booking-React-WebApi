import React, { Component } from "react";
import FooterMenu from "./FooterMenu";

export class Footer extends Component {
  render() {
    return (
      <div>
        <footer className="position-relative z-index-10 d-print-none">
          <div className="py-4  text-muted footer-bg">
            <div className="container">
              <div className="row">
                <div className="col-lg-4 mb-5 mb-lg-0">
                  <div className="font-weight-bold text-dark mb-3">
                    پرهان ترانسفر
                  </div>
                  <p className="text-justify">
                    پرهان ترانسفر از سال 1383 فعالیت خود را آغاز نموده که بصورت
                    کاملا تخصصی خدمات ترانسفر و گشت را ارائه مینماییم. تمامي
                    فعالیتهای مربوط به رزرو این سامانه، دارای مجوزهای لازم از
                    مراجع مربوطه می باشند و فعالیت های اين سايت تابع قوانین و
                    مقررات جمهوری اسلامی ايران است.
                  </p>
                </div>
                <div className="col-lg-3 col-md-6 mb-5 mb-lg-0">
                  <h6 className="text-uppercase text-dark mb-3">
                    لینک های مفید
                  </h6>
                  <FooterMenu />
                </div>
                <div className="col-lg-5 col-md-6 mb-5 mb-lg-0">
                  <h6 className="text-uppercase text-dark mb-3">
                    ارتباط با ما
                  </h6>
                  <p>
                    مشهد، بلوار هاشمیه، هاشمیه 23، ساختمان سامان، واحد 302
                    <br />
                    صاحب امتیاز: استیو جابز
                    <br />
                    info@parhantransfer.com
                  </p>
                </div>
              </div>
            </div>
          </div>
          <div className="py-4 font-weight-light bg-black text-gray-300">
            <div className="container">
              <div className="row align-items-center">
                <div className="col-md-6 text-right ">
                  <p className="text-sm text-muted mb-md-0">
                    پرهان ترانسفر &copy; {new Date().getFullYear()}
                  </p>
                </div>

                <div className="col-md-6 text-left ">
                  <p className="text-sm text-muted mb-md-0">
                    طراحی سایت:{" "}
                    <a
                      href="http://webtina.ir"
                      className="text-muted"
                      target="_blank"
                    >
                      وبتینا
                    </a>
                  </p>
                </div>
              </div>
            </div>
          </div>
        </footer>
      </div>
    );
  }
}

export default Footer;
