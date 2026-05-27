const initialState = {
    results: [],
    loading: false,
    pattern: ""
};

const symptomSearchReducer = (state = initialState, action) => {
    switch (action.type) {

        case "REQUEST_SYMPTOM_SEARCH_RESULTS":
            return {
                ...state,
                loading: true,
                pattern: action.pattern
            };

        case "RECEIVE_SYMPTOM_SEARCH_RESULTS":
            return {
                ...state,
                loading: false,
                results: action.results,
                pattern: action.pattern
            };

        case "CLEAR_SYMPTOM_SEARCH_RESULTS":
            return {
                ...initialState
            };

        default:
            return state;
    }
};

export default symptomSearchReducer;