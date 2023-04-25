import {
  GET_LATEST_TOURS,
  START_LOADING,
  LOG_ERROR,
  SEARCH_RESULT
} from "../actions/types";

const initialState = {
  latest_Tours: [],
  isLoading: false,
  search_Result: null,
  error: null
};

export default (state = initialState, action) => {
  switch (action.type) {
    case START_LOADING:
      return {
        ...state,
        isLoading: true
      };
    case GET_LATEST_TOURS:
      return {
        ...state,
        latest_Tours: action.payload,
        isLoading: false
      };
    case SEARCH_RESULT:
      return {
        ...state,
        search_Result: action.payload,
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
