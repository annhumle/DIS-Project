const initialState = {
    physicalSymptoms: [],
    loading: false
};

const physicalSymptomReducer = (state = initialState, action) => {
    switch (action.type) {

        case "REQUEST_PHYSICAL_SYMPTOMS":
            return {
                ...state,
                loading: true
            };

        case "RECEIVE_PHYSICAL_SYMPTOMS":
            return {
                ...state,
                loading: false,
                physicalSymptoms: action.physicalSymptoms
            };

        default:
            return state;
    }
};

export default physicalSymptomReducer;
