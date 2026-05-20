const initialState = {
    cycles: [],
    loading: false
};

const cycleReducer = (state = initialState, action) => {
    switch (action.type) {

        case "REQUEST_CYCLES":
            return {
                ...state,
                loading: true
            };

        case "RECEIVE_CYCLES":
            return {
                ...state,
                loading: false,
                cycles: action.cycles
            };

        default:
            return state;
    }
};

export default cycleReducer;