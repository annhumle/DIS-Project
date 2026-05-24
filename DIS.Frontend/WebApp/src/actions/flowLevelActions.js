import { get } from "../utils";

export const REQUEST_FLOW_LEVELS = "REQUEST_FLOW_LEVELS";
function requestFlowLevels() {
    return {
        type: REQUEST_FLOW_LEVELS
    };
}

export const RECEIVE_FLOW_LEVELS = "RECEIVE_FLOW_LEVELS";
function receiveFlowLevels(flowLevels) {
    return {
        type: RECEIVE_FLOW_LEVELS,
        flowLevels
    };
}

export function getFlowLevels() {
    return function (dispatch) {
        dispatch(requestFlowLevels());

        const url = "api/cycle-tracker/flow-levels";

        return get(url).then(response => {
            if (response) {
                dispatch(receiveFlowLevels(response));
            }
        });
    };
}
