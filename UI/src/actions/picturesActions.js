import { GET_FIRS_PAGE_PICTURES, LOG_ERROR, START_LOADING } from "./types";
import { AdvertismentPlaceEnum } from "../infrastructure/models";
import { getPictures } from "../services/commonService";

export const GetFirstPagePictures = () => async dispatch => {
  try {
    //StartLoading();
    dispatch({
      type: START_LOADING
    });

    const { data: result } = await getPictures(
      AdvertismentPlaceEnum.FirstPage,
      0
    );
    if (result.success)
      dispatch({
        type: GET_FIRS_PAGE_PICTURES,
        payload: result.data
      });
    else
      dispatch({
        type: GET_FIRS_PAGE_PICTURES,
        payload: null
      });
  } catch (error) {
    dispatch({
      type: LOG_ERROR,
      payload: error
    });
  }
};

export const StartLoading = () => {
  return {
    type: START_LOADING
  };
};

// export const StartLoading = () => async dispatch => {
//   dispatch({
//     type: START_LOADING
//   });
// };
