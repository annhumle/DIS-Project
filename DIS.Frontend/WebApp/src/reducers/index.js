import { combineReducers } from "redux";
import cycleReducer from "./cycleReducer";
import flowLevelReducer from "./flowLevelReducer";
import physicalSymptomReducer from "./physicalSymptomReducer";
import dailyLogReducer from "./dailyLogReducer";
import symptomSearchReducer from "./symptomSearchReducer";

const reducers = combineReducers({
    cycleState: cycleReducer,
    flowLevelState: flowLevelReducer,
    physicalSymptomState: physicalSymptomReducer,
    dailyLogState: dailyLogReducer,
    symptomSearchState: symptomSearchReducer
});

export default reducers;
