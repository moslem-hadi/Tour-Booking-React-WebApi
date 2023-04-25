import { GET_FIRST_PAGE_DATA, LOG_ERROR, START_LOADING } from "./types";

import { getFirstPageData } from "../services/commonService";

export const GetFirstPageData = () => async dispatch => {
  StartLoading();

  try {
    var { data: result } = await getFirstPageData();
    if (result.success)
      dispatch({
        type: GET_FIRST_PAGE_DATA,
        payload: result.data
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
