import { get } from "../utils";

export const REQUEST_PHYSICAL_SYMPTOMS = "REQUEST_PHYSICAL_SYMPTOMS";
function requestPhysicalSymptoms() {
    return {
        type: REQUEST_PHYSICAL_SYMPTOMS
    };
}

export const RECEIVE_PHYSICAL_SYMPTOMS = "RECEIVE_PHYSICAL_SYMPTOMS";
function receivePhysicalSymptoms(physicalSymptoms) {
    return {
        type: RECEIVE_PHYSICAL_SYMPTOMS,
        physicalSymptoms
    };
}

export function getPhysicalSymptoms() {
    return function (dispatch) {
        dispatch(requestPhysicalSymptoms());

        const url = "api/cycle-tracker/physical-symptoms";

        return get(url).then(response => {
            if (response) {
                dispatch(receivePhysicalSymptoms(response));
            }
        });
    };
}
