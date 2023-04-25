import React, { Component, useEffect } from "react";
import LatestTours from "../Product/LatestTours";
import FirstPagePictures from "../Layout/Pictures/FirstPagePictures";
import FirstPageSearch from "../Layout/Search/FirstPageSearch";
import FirstPageSlider from "../Layout/Slider/FirstPageSlider";
import FirstPageArticles from "../Layout/Article/FirstPageArticles";

import { Loader } from "../Layout/Common/Loader";
import { getFirstPageData } from "../../services/commonService";

class HomePage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      data: {},
      isLoaded: false
    };
  }

  async componentDidMount() {
    document.title = "پرهان ترانسفر";

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
    const { error, isLoaded, data } = this.state;
    if (isLoaded) {
      if (error) {
        return (
          <div className="container py-7 text-center">
            <img
              src="/assets/img/erro-loading-data.png"
              className="mb-4"
              alt="loading..."
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
          sliderPic,
          sliderTitle,
          sliderSubTitle,
          sliderText,
          cities,
          hamrahiTypes,
          placeTypes,
          vehicleTypes,
          activityTypes,
          firstPageProductCount
        } = data;
        document.title = websiteTitle;

        return (
          <div>
            <FirstPageSlider
              sliderTitle={sliderTitle}
              sliderSubTitle={sliderSubTitle}
              sliderText={sliderText}
              sliderPic={sliderPic}
            />
            <section className="bg-gray-100">
              <div className="container position-relative mt-n6 z-index-20 ">
                <FirstPageSearch
                  cities={cities}
                  hamrahiTypes={hamrahiTypes}
                  placeTypes={placeTypes}
                  vehicleTypes={vehicleTypes}
                  activityTypes={activityTypes}
                />
              </div>
            </section>
            <FirstPagePictures />
            <LatestTours firstPageProductCount={firstPageProductCount} />
            <FirstPageArticles />
          </div>
        );
      }
    } else {
      return <Loader />;
    }
  }
}

export default HomePage;
