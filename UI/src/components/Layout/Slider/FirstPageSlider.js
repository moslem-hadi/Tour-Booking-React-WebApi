import React from "react";
import PropTypes from "prop-types";

const FirstPageSlider = ({
  sliderPic,
  sliderText,
  sliderTitle,
  sliderSubTitle
}) => {
  return (
    <section
      style={{ backgroundImage: `url(${sliderPic})` }}
      className="d-flex align-items-center dark-overlay bg-cover"
    >
      <div className="container py-6 py-lg-7 text-white overlay-content">
        <div className="row">
          <div className="col-xl-8">
            <h1
              className="h4 font-weight-bold text-shadow"
              dangerouslySetInnerHTML={{ __html: sliderTitle }}
            />
            <p
              className="text-lg text-shadow mb-6"
              dangerouslySetInnerHTML={{ __html: sliderText }}
            />
          </div>
        </div>
      </div>
    </section>
  );
};

FirstPageSlider.propTypes = {
  sliderSubTitle: PropTypes.string.isRequired,
  sliderTitle: PropTypes.string.isRequired,
  sliderText: PropTypes.string.isRequired,
  sliderPic: PropTypes.string.isRequired
};

export default FirstPageSlider;
