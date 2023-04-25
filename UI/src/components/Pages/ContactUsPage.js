import React, { useState, useEffect } from "react";
import { Notif } from "../../infrastructure/utilities";
import { getPage } from "../../services/pageService";
import { submitContactUs } from "../../services/commonService";

const ContactUsPage = () => {
  document.title = "ارتباط با پرهان ترانسفر";
  const [name, setname] = useState("");
  const [mobile, setmobile] = useState("");
  const [message, setmessage] = useState("");
  const [isLoading, setisLoading] = useState(false);
  const [messageSent, setmessageSent] = useState(false);
  const [pageInfo, setpageInfo] = useState({});

  useEffect(() => {
    async function getData() {
      try {
        const { data: result } = await getPage("contactus");
        setpageInfo(result.data);
        if (result.data && result.data.title)
          document.title = result.data.title;
      } catch {
        setpageInfo(null);
      }
    }
    getData();
  }, []);

  const submitForm = e => {
    e.preventDefault();

    setisLoading(true);
    (async () => {
      try {
        const { data: result } = submitContactUs(name, mobile, message);

        setisLoading(false);
        if (result.success) {
          setmessageSent(true);
        } else {
          Notif(
            "خطا در ثبت اطلاعات. لطفا بعدا مجددا امتحان کنید.",
            "danger",
            "توجه!"
          );
        }
      } catch (error) {
        Notif(
          "خطا در درخواست از سرور. لطفا بعدا مجدد امتحان کنید.",
          "danger",
          "توجه!"
        );
        setisLoading(false);
      }
    })();
  };
  return (
    <div>
      <section className="hero py-4 py-lg-5 text-white dark-overlay">
        <img
          src="/assets/img/slider1.jpg"
          alt="تماس با پرهان ترانسفر"
          className="bg-image"
        />
        <div className="container overlay-content">
          <ol className="breadcrumb text-white justify-content-center no-border mb-0">
            <li className="breadcrumb-item">
              <a href="index.html">صفحه نخست</a>
            </li>
            <li className="breadcrumb-item active">تماس با ما </li>
          </ol>
          <h1 className="hero-heading">
            {pageInfo == null || pageInfo.title == undefined
              ? ""
              : pageInfo.title}
          </h1>
        </div>
      </section>
      <section className="py-6">
        <div className="container">
          <div className="row">
            <div className="col-md-5">
              <div
                dangerouslySetInnerHTML={{
                  __html:
                    pageInfo == null || pageInfo.text == undefined
                      ? ""
                      : pageInfo.text.replace(/(<? *script)/gi, "illegalscript")
                }}
              ></div>
            </div>

            <div className="col-md-7 mb-5 ">
              <h2 className="h5 mb-4">ارسال پیام</h2>

              {messageSent ? (
                <div>
                  <div class="f-modal-alert">
                    <div class="f-modal-icon f-modal-success animate">
                      <span class="f-modal-line f-modal-tip animateSuccessTip"></span>
                      <span class="f-modal-line f-modal-long animateSuccessLong"></span>
                      <div class="f-modal-placeholder"></div>
                      <div class="f-modal-fix"></div>
                    </div>
                  </div>

                  <h4 class="text-success text-center">
                    با تشکر. پیام شما ارسال شد
                  </h4>
                </div>
              ) : (
                <form onSubmit={submitForm} id="contact-form" className="form">
                  <div className="controls">
                    <div className="row">
                      <div className="col-sm-6">
                        <div className="form-group">
                          <label htmlFor="name" className="form-label">
                            نام و نام خانوادگی
                          </label>
                          <input
                            type="text"
                            name="name"
                            id="name"
                            defaultValue={name}
                            onChange={e => setname(e.target.value)}
                            required="required"
                            className="form-control"
                          />
                        </div>
                      </div>
                      <div className="col-sm-6">
                        <div className="form-group">
                          <label htmlFor="mobile" className="form-label">
                            شماره تماس
                          </label>
                          <input
                            type="text"
                            name="mobile"
                            id="mobile"
                            defaultValue={mobile}
                            onChange={e => setmobile(e.target.value)}
                            required="required"
                            className="form-control ltr"
                          />
                        </div>
                      </div>
                    </div>
                    <div className="form-group">
                      <label htmlFor="message" className="form-label">
                        پیام شما
                      </label>
                      <textarea
                        rows="4"
                        name="message"
                        id="message"
                        defaultValue={message}
                        onChange={e => setmessage(e.target.value)}
                        placeholder="متن پیام خود را اینجا وارد کنید"
                        required="required"
                        className="form-control"
                      ></textarea>
                    </div>
                    <div className="text-center">
                      <button type="submit" className="btn btn-outline-primary">
                        {isLoading ? (
                          <span>
                            لطفا صبر کنید
                            <i className="fa fa-circle-notch fa-spin mr-2"></i>
                          </span>
                        ) : (
                          <span>
                            ارسال پیام{" "}
                            <i className="fa-chevron-left fa mr-2"></i>
                          </span>
                        )}
                      </button>
                    </div>
                  </div>
                </form>
              )}
            </div>
          </div>
        </div>
      </section>
    </div>
  );
};

export default ContactUsPage;
