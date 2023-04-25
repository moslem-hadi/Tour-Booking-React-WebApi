import {
  GET_FIRST_PAGE_DATA,
  LOG_ERROR,
  START_LOADING
} from "../actions/types";

const initialState = {
  firstPageData: {},
  isLoading: false,
  error: null
};

export default (state = initialState, action) => {
  switch (action.type) {
    case START_LOADING:
      return {
        ...state,
        isLoading: true
      };
    case GET_FIRST_PAGE_DATA:
      return {
        ...state,
        firstPageData: action.payload,
        isLoading: false
      };
    case LOG_ERROR:
      return {
        ...state,
        error: action.payload,
        isLoading: false
      };

    default:
      return state;
  }
};
