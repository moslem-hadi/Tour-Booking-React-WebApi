import { combineReducers } from "redux";
import productReducer from "./productReducer";
import picturesReducer from "./picturesReducer";
import commonReducer from "./commonReducer";

export default combineReducers({
  //List all reducers
  product: productReducer,
  pictures: picturesReducer,
  common: commonReducer
});
