import { get } from "../utils";

export const REQUEST_CYCLES = "REQUEST_CYCLES";
function requestCycles() {
    return {
        type: REQUEST_CYCLES
    };
}

export const RECEIVE_CYCLES = "RECEIVE_CYCLES";
function receiveCycles(cycles) {
    return {
        type: RECEIVE_CYCLES,
        cycles
    };
}

export function getCycles() {
    return function (dispatch) {
        dispatch(requestCycles());

        const url = "api/cycle-tracker/cycles";

        return get(url).then(response => {
            if (response) {
                dispatch(receiveCycles(response));
            }
        });
    };
}