const initialState = {
    dailyLogs: [],
    loading: false
};

const dailyLogReducer = (state = initialState, action) => {
    switch (action.type) {

        case "REQUEST_DAILY_LOGS":
            return {
                ...state,
                loading: true
            };

        case "RECEIVE_DAILY_LOGS":
            return {
                ...state,
                loading: false,
                dailyLogs: action.dailyLogs
            };

        default:
            return state;
    }
};

export default dailyLogReducer;
