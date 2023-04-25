import {
  GET_FIRS_PAGE_PICTURES,
  LOG_ERROR,
  START_LOADING
} from "../actions/types";

const initialState = {
  picturesList: [],
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
    case GET_FIRS_PAGE_PICTURES:
      return {
        ...state,
        picturesList: action.payload,
        isLoading: false
      };
    case LOG_ERROR:
      return {
        ...state,
        error: action.payload
      };

    default:
      return state;
  }
};
