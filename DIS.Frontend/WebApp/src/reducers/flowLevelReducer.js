const initialState = {
    flowLevels: [],
    loading: false
};

const flowLevelReducer = (state = initialState, action) => {
    switch (action.type) {

        case "REQUEST_FLOW_LEVELS":
            return {
                ...state,
                loading: true
            };

        case "RECEIVE_FLOW_LEVELS":
            return {
                ...state,
                loading: false,
                flowLevels: action.flowLevels
            };

        default:
            return state;
    }
};

export default flowLevelReducer;
