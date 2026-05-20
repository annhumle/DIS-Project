import { combineReducers } from "redux";
import cycleReducer from "./cycleReducer";

const reducers = combineReducers({
    cycleState: cycleReducer
});

export default reducers;