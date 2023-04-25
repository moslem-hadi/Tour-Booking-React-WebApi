import React from "react";
import PropTypes from "prop-types";
import { ProductTypesEnum } from "../../infrastructure/models";
import FullSearch from "../Layout/Search/FullSearch";
import { GlobalValues } from "../../infrastructure/utilities";
import ProductSearchList from "../Product/ProductSearchList";
import { getFirstPageData } from "../../services/commonService";

class SearchPage extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      type: props.type,
      data: {},
      isLoaded: false
    };
  }
  componentDidUpdate(prevProps) {
    if (prevProps.type !== this.props.type) {
      this.setState({ ...this.state, type: this.props.type });
    }
  }
  componentDidMount() {
    this.firstPageData();
  }
  async firstPageData() {
    try {
      var { data: result } = await getFirstPageData();
      this.setState({
        isLoaded: true,
        data: result.data
      });
    } catch (error) {
      this.setState({
        isLoaded: true,
        error
      });
    }
  }
  render() {
    const { error, isLoaded, data, type } = this.state;
    if (isLoaded) {
      if (error) {
        return (
          <div className="container py-7 text-center">
            <img
              src="/assets/img/erro-loading-data.png"
              className="mb-4"
              alt="loading ..."
            />

            <h4>خطا در دریافت اطلاعات</h4>
            <p className="text-muted">
              دریافت اطلاعات با خطا مواجه شد. لطفا بعدا مجددا تلاش کنید.
            </p>
          </div>
        );
      } else {
        var {
          websiteTitle,
          cities,
          hamrahiTypes,
          placeTypes,
          vehicleTypes,
          activityTypes
        } = data;
        document.title = websiteTitle;
        return (
          <div>
            <section
              style={{
                backgroundImage: `url('/assets/img/slider1.jpg')`
              }}
              className="pt-5 pb-5 d-flex align-items-end dark-overlay bg-cover"
            >
              <div className="container overlay-content">
                <div className="d-flex justify-content-between align-items-start flex-column flex-lg-row align-items-lg-end">
                  <div className="text-white mb-4 mb-lg-0">
                    <h2 className="text-shadow verified">
                      رزرو &nbsp;
                      {type == ProductTypesEnum.Gasht
                        ? "گشت و دربستی"
                        : type == ProductTypesEnum.Transfer
                        ? "ترانسفر"
                        : type == ProductTypesEnum.Tour
                        ? "تور"
                        : ""}
                    </h2>
                    <p>جستجو و سفارش رزرو</p>
                  </div>
                </div>
              </div>
            </section>
            <section className="">
              <div className="container">
                <div className="list-group shadow mb-5 mt-n5 position-relative z-index-20 bg-white">
                  <FullSearch
                    cities={cities}
                    hamrahiTypes={hamrahiTypes}
                    placeTypes={placeTypes}
                    vehicleTypes={vehicleTypes}
                    activityTypes={activityTypes}
                    type={type}
                  />

                  <ProductSearchList type={type} />
                </div>
              </div>
            </section>
          </div>
        );
      }
    } else {
      return (
        <p className="pt-5 pb-5 mt-5 mb-5 text-center">
          <img src="/assets/img/loader01.gif" />
        </p>
      );
    }
  }
}

SearchPage.propTypes = {};

export default SearchPage;
