import { get } from "../utils";

export const REQUEST_SYMPTOM_SEARCH_RESULTS = "REQUEST_SYMPTOM_SEARCH_RESULTS";
function requestSymptomSearchResults(pattern) {
    return {
        type: REQUEST_SYMPTOM_SEARCH_RESULTS,
        pattern
    };
}

export const RECEIVE_SYMPTOM_SEARCH_RESULTS = "RECEIVE_SYMPTOM_SEARCH_RESULTS";
function receiveSymptomSearchResults(results, pattern) {
    return {
        type: RECEIVE_SYMPTOM_SEARCH_RESULTS,
        results,
        pattern
    };
}

export const CLEAR_SYMPTOM_SEARCH_RESULTS = "CLEAR_SYMPTOM_SEARCH_RESULTS";
export function clearSymptomSearchResults() {
    return {
        type: CLEAR_SYMPTOM_SEARCH_RESULTS
    };
}

export function searchDailyLogsBySymptomPattern(pattern) {
    return function (dispatch) {
        const trimmedPattern = pattern.trim();

        if (!trimmedPattern) {
            dispatch(clearSymptomSearchResults());
            return Promise.resolve();
        }

        dispatch(requestSymptomSearchResults(trimmedPattern));

        const url = `api/cycle-tracker/dailylogs/search?pattern=${encodeURIComponent(trimmedPattern)}`;

        return get(url).then(response => {
            if (response) {
                dispatch(receiveSymptomSearchResults(response, trimmedPattern));
            }
        });
    };
}