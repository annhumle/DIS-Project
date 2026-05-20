import store from "./store";
import { reportError } from "./actions/errors";

const BASE_URL = "http://localhost:5221";

export function post(url, data) {
    const options = {
        method: "POST",
        headers: {
            "Content-Type": "application/json; charset=utf-8",
        },
        credentials: "include",
        body: JSON.stringify(data || {}),
    };

    return fetch(BASE_URL + "/" + url, options)
        .then(parseFetchResponse)
        .then(handleResponse)
        .catch(handleError);
}

export function postForm(url, data) {
    const options = {
        method: "POST",
        mode: "cors",
        credentials: "include",
        body: data,
    };

    return fetch(BASE_URL + "/" + url, options)
        .then(parseFetchResponse)
        .then(handleResponse)
        .catch(handleError);
}

export function get(url) {
    const options = {
        method: "GET",
        headers: {
            "Content-Type": "application/json; charset=utf-8",
        },
        credentials: "include",
    };

    return fetch(BASE_URL + "/" + url, options)
        .then(parseFetchResponse)
        .then(handleResponse)
        .catch(handleError);
}

function parseFetchResponse(response) {
    if (response.status === 204) return { json: {}, meta: response };
    
    const contentLength = response.headers.get("Content-Length");
    if (contentLength === "0") return { json: {}, meta: response };

    return response.json().then(text => ({
        json: text,
        meta: response,
    }));
}

function handleResponse({ json, meta }) {
    if (meta.ok) {
        return json;
    }

    if (meta.status === 404) {
        throw new Error("NotFound");
    }

    if (meta.status == 401) {
        location.reload();
    }

    var message = "";
    if (typeof json === 'object') {
        for (var key in json) {
            if (json.hasOwnProperty(key)) {
                message += key + ": " + json[key] + " ";
            }
        }
        throw new Error(message || "Der er sket en fejl");
    } else {
        throw new Error(json.Messages || "Der er sket en fejl");
    }
}

function handleError(e) {
    store.dispatch(reportError(e));
    return Promise.reject(false);
}

export function escapeId(id) {
    if (!id) return "";
    return id.replace(/\//g, "_");
}

export function debounce(func, wait, immediate) {
    var timeout;

    return function executedFunction() {
        var context = this;
        var args = arguments;

        var later = function() {
            timeout = null;
            if (!immediate) func.apply(context, args);
        };

        var callNow = immediate && !timeout;

        clearTimeout(timeout);

        timeout = setTimeout(later, wait);

        if (callNow) func.apply(context, args);
    };
}