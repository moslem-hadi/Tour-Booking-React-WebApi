import {
  GET_LATEST_TOURS,
  START_LOADING,
  LOG_ERROR,
  SEARCH_RESULT
} from "./types";
import { ProductTypesEnum } from "../infrastructure/models";
import { GlobalValues } from "../infrastructure/utilities";
import { getProducts, searchProducts } from "../services/productService";
export const GetLatestTours = count => async dispatch => {
  try {
    //StartLoading();
    const { data: result } = await getProducts(ProductTypesEnum.Tour, count);

    if (result.success)
      dispatch({
        type: GET_LATEST_TOURS,
        payload: result.data
      });
    else
      dispatch({
        type: GET_LATEST_TOURS,
        payload: null
      });
  } catch (error) {
    dispatch({
      type: LOG_ERROR,
      payload: error
    });
  }
};

export const SearchProducts = (
  type,
  date,
  comptype,
  city,
  vehicle,
  acttype,
  place
) => async dispatch => {
  try {
    dispatch({
      type: START_LOADING
    });
    //StartLoading();
    const { data: result } = await searchProducts(
      type,
      date,
      comptype, //همراهی
      city, //شهر
      vehicle, //نقلیه
      acttype, //گروهی یا دربستی
      place //فروگاه، راه آهن و..
    );

    if (result.success)
      dispatch({
        type: SEARCH_RESULT,
        payload: result.data
      });
    else
      dispatch({
        type: SEARCH_RESULT,
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
