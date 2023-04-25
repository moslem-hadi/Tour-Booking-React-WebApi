import React, { Fragment } from "react";
import ContentLoader from "react-content-loader";

//source Code: https://github.com/danilowoz/create-content-loader/blob/master/src/Gallery/insertYourLoaderHere/CatalogMagic.js
const ProductGridLoader = ({
  width = 1140,
  heading = { width: 0, height: 0 },
  row = 1,
  column = 3,
  padding = 30,
  borderRadius = 5,
  ...props
}) => {
  const list = [];
  const screenWidth = window.innerWidth;
  width =
    screenWidth >= 1200
      ? 1140
      : screenWidth >= 990
      ? 960
      : screenWidth >= 768
      ? 720
      : screenWidth >= 576
      ? 540
      : screenWidth;

  let height;

  for (let i = 1; i <= row; i++) {
    for (let j = 0; j < column; j++) {
      const itemWidth = (width - padding * (column + 1)) / column;

      const x = padding / 2 + j * (itemWidth + padding);

      const height1 = itemWidth / 2;

      const height2 = 10;

      const height3 = 10;

      const space =
        padding + height1 + (padding / 2 + height2) + height3 + padding * 4;

      const y1 = heading.height + space * (i - 1);

      const y2 = y1 + padding + height1 - 15;

      const y3 = y2 + padding / 2 + height2 - 5;

      list.push(
        <>
          <rect
            x={x}
            y={y1}
            rx={borderRadius}
            ry={borderRadius}
            width={itemWidth}
            height={height1}
          />
          <rect x={x} y={y2} rx={0} ry={0} width={itemWidth} height={height2} />
          <rect
            x={x}
            y={y3}
            rx={0}
            ry={0}
            width={itemWidth * 0.6}
            height={height3}
          />
        </>
      );

      if (i === row) {
        height = y3 + height3;
      }
    }
  }

  return (
    <ContentLoader
      title="در حال دریافت اطلاعات..."
      rtl
      viewBox={`0 0 ${width} ${height}`}
      width={width}
      height={height}
      speed={2}
      {...props}
    >
      {list.map((element, index) => {
        return <Fragment key={"mykey" + index}>{element}</Fragment>;
      })}
    </ContentLoader>
  );
};

export default ProductGridLoader;
