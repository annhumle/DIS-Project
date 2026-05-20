export const REPORT_ERROR = "REPORT_ERROR";
export function reportError(error) {
    return {
        type: REPORT_ERROR,
        id: Date.now(),
        error
    };
}

export const DISMISS_ERROR = "DISMISS_ERROR";
export function dismissError(id) {
    return {
        type: DISMISS_ERROR,
        id
    };
}