import React from "react";
import ContentLoader from "react-content-loader";

const FirsPagePicturesLoader = () => {
  const screenWidth = window.innerWidth;
  const width =
    screenWidth >= 1200
      ? 1140
      : screenWidth >= 990
      ? 960
      : screenWidth >= 768
      ? 720
      : screenWidth >= 576
      ? 540
      : screenWidth >= 420
      ? 420
      : screenWidth;
  var w1 = (width / 3) * 2;
  var w2 = width - w1 - 10;
  var w3 = (width - 20) / 3;
  return (
    <ContentLoader
      rtl
      title="در حال دریافت اطلاعات..."
      speed={2}
      width={width}
      height={290}
      viewBox={`0 0 ${width} 290`}
      backgroundColor="#f8f9fa"
      foregroundColor="#e2e0e0"
    >
      <rect x="0" y="0" rx="10" ry="10" width={w1} height="140" />
      <rect x={w1 + 10} y="0" rx="10" ry="10" width={w2} height="140" />
      <rect x="0" y="150" rx="10" ry="10" width={w3} height="140" />
      <rect x={w3 + 10} y="150" rx="10" ry="10" width={w3} height="140" />
      <rect x={w3 * 2 + 20} y="150" rx="10" ry="10" width={w3} height="140" />
    </ContentLoader>
  );
};

export default FirsPagePicturesLoader;
