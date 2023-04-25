import React, { Component } from "react";
import { Link, Redirect } from "react-router-dom";
import { GlobalValues, numberWithCommas } from "../../infrastructure/utilities";
import { Loader } from "../Layout/Common/Loader";
import { ProductTypesEnum } from "../../infrastructure/models";
import { getProduct } from "../../services/productService";

export default class ViewPage extends Component {
  state = { pageInfo: null, isLoaded: false, error: null };

  async componentDidMount() {
    let slug = this.props.match.params.slug;
    document.title = "پرهان ترانسفر";

    try {
      const { data: result } = await getProduct(slug);
      this.setState({
        isLoaded: true,
        pageInfo: result.data
      });
      if (result.data && result.data.seoTitle)
        document.title = result.data.seoTitle;
    } catch (error) {
      this.setState({
        isLoaded: true,
        error
      });
    }
  }
  render() {
    let { isLoaded, pageInfo } = this.state;
    return !isLoaded ? (
      <section className="py-6">
        <div className="container">
          <Loader />
        </div>
      </section>
    ) : pageInfo == null ? (
      <Redirect to="/404" />
    ) : (
      <div>
        <section
          style={{
            backgroundImage: `url('${GlobalValues.ContentFolderUrl}productpic/${pageInfo.pic}')`
          }}
          className="pt-7 pb-5 d-flex align-items-end dark-overlay bg-cover"
        >
          <div className="container overlay-content">
            <div className="d-flex justify-content-between align-items-start flex-column flex-lg-row align-items-lg-end">
              <div className="text-white mb-4 mb-lg-0">
                <div className="badge badge-pill badge-transparent px-3 py-2 mb-4">
                  {pageInfo.type == ProductTypesEnum.Tour
                    ? "تور"
                    : pageInfo.type == ProductTypesEnum.Gasht
                    ? "گشت و دربستی"
                    : "ترانسفر"}
                </div>
                <h1 className="text-shadow verified">{pageInfo.title}</h1>
                <p>
                  <i className="fa-map-marker fa ml-2"></i> {pageInfo.cityTitle}
                </p>
              </div>
            </div>
          </div>
        </section>
        <section className="py-6">
          <div className="container">
            <div className="row">
              <div className="col-lg-8">
                <div className="text-block">
                  <h3 className="mb-3">توضیحات</h3>

                  <p
                    className="text-muted"
                    dangerouslySetInnerHTML={{
                      __html: pageInfo.text.replace(
                        /(<? *script)/gi,
                        "illegalscript"
                      )
                    }}
                  ></p>
                </div>

                {/* <div className="text-block">
                  <h3 className="mb-4">Gallery</h3>
                  <div className="row gallery ml-n1 mr-n1">
                    <div className="col-lg-4 col-6 px-1 mb-2">
                      <a href="img/photo/restaurant-1515164783716-8e6920f3e77c.jpg">
                        <img
                          src="../../../https@d19m59y37dris4.cloudfront.net/directory/1-4/img/photo/restaurant-1515164783716-8e6920f3e77c.jpg"
                          alt="..."
                          className="img-fluid"
                        />
                      </a>
                    </div>
                 </div>
                </div> */}
              </div>
              <div className="col-lg-4">
                <div
                  style={{ top: "100px" }}
                  className="shadow ml-lg-4 rounded sticky-top"
                >
                  <img
                    src={`${GlobalValues.ContentFolderUrl}productpic/${pageInfo.pic}`}
                    className="mw-100 product-img"
                    alt={pageInfo.title}
                  />
                  <div className="p-4">
                    <p className="text-muted">
                      قیمت:&nbsp;&nbsp;
                      {pageInfo.minPrice == pageInfo.maxPrice ? (
                        <span className="text-primary h5">
                          {numberWithCommas(pageInfo.minPrice)}
                        </span>
                      ) : (
                        <>
                          از&nbsp;
                          <span className="text-primary h5">
                            {numberWithCommas(pageInfo.minPrice)}
                          </span>
                          &nbsp;تا&nbsp;
                          <span className="text-primary h5">
                            {numberWithCommas(pageInfo.maxPrice)}
                          </span>
                        </>
                      )}
                      &nbsp;تومان
                    </p>
                    <hr className="my-4" />
                    <Link
                      to={
                        pageInfo.type == ProductTypesEnum.Tour
                          ? "/Tour"
                          : pageInfo.type == ProductTypesEnum.Gasht
                          ? "/Gasht"
                          : "/Transfer"
                      }
                      className="btn btn-primary btn-block"
                    >
                      رزرو&nbsp;
                      {pageInfo.type == ProductTypesEnum.Tour
                        ? "تور"
                        : pageInfo.type == ProductTypesEnum.Gasht
                        ? "گشت و دربستی"
                        : "ترانسفر"}
                    </Link>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </section>
      </div>
    );
  }
}
