import React, { useEffect } from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { GetFirstPagePictures } from "../../../actions/picturesActions";
import { GlobalValues } from "../../../infrastructure/utilities";

import FirsPagePicturesLoader from "../Common/FirsPagePicturesLoader";

const FirstPagePictures = ({
  pictures: { picturesList, isLoading },
  GetFirstPagePictures
}) => {
  useEffect(() => {
    GetFirstPagePictures();
    //eslint-disable-next-line
  }, []);
  if (isLoading) {
    return (
      <section className="py-6 bg-gray-100">
        <div className="container overflow-hidden">
          <FirsPagePicturesLoader />
        </div>
      </section>
    );
  }

  if (!isLoading && (picturesList == null || picturesList.length === 0)) {
    return null;
  }

  return (
    <section className="pt-5 pb-2 bg-gray-100">
      <div className="container">
        <div className="row">
          {picturesList.map(pic => (
            <div
              className={`d-flex align-items-lg-stretch mb-4 col-lg-${pic.columnSize}`}
              key={pic.id}
            >
              <div
                style={{
                  backgroundImage: `url(${GlobalValues.ContentFolderUrl}pictures/${pic.pic})`,
                  backgroundSize: "cover"
                }}
                className="card shadow-lg border-0 w-100 border-0 hover-animate"
              >
                <a href={pic.link} className="tile-link"></a>
                <div className="d-flex align-items-center h-100 text-white justify-content-center py-6 py-lg-7">
                  <h3 className="text-shadow text-uppercase mb-0">
                    {pic.title}
                  </h3>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
};

FirstPagePictures.propTypes = {
  pictures: PropTypes.object.isRequired,
  GetFirstPagePictures: PropTypes.func.isRequired
};

const mapStatesToProps = state => ({
  pictures: state.pictures
});

export default connect(mapStatesToProps, { GetFirstPagePictures })(FirstPagePictures);
