import { get, post, put } from "../utils";

export const REQUEST_DAILY_LOGS = "REQUEST_DAILY_LOGS";
function requestDailyLogs() {
    return {
        type: REQUEST_DAILY_LOGS
    };
}

export const RECEIVE_DAILY_LOGS = "RECEIVE_DAILY_LOGS";
function receiveDailyLogs(dailyLogs) {
    return {
        type: RECEIVE_DAILY_LOGS,
        dailyLogs
    };
}

export function getLogsByCycleId(cycleId) {
    return function (dispatch) {
        dispatch(requestDailyLogs());

        const url = `api/cycle-tracker/cycles/${cycleId}/logs`;

        return get(url).then(response => {
            if (response) {
                dispatch(receiveDailyLogs(response));
            }
        });
    };
}

export function getAllLogs(cycles) {
    return function (dispatch) {
        if (!cycles || cycles.length === 0) return Promise.resolve();

        dispatch(requestDailyLogs());

        const requests = cycles.map(cycle =>
            get(`api/cycle-tracker/cycles/${cycle.cycleId}/logs`).catch(() => [])
        );

        return Promise.all(requests).then(results => {
            const allLogs = results.flat();
            dispatch(receiveDailyLogs(allLogs));
        });
    };
}

export function createDailyLog(dto) {
    return function () {
        const url = "api/cycle-tracker/dailylogs";

        return post(url, dto);
    };
}

export function updateDailyLog(dailyLogId, dto) {
    return function () {
        const url = `api/cycle-tracker/dailylogs/${dailyLogId}`;

        return put(url, dto);
    };
}
