
/**
 * Send request to server
 * @param {string} SendMethod
 * @param {string} uri
 * @param {any} body
 * @returns
 */
function XHRDownloader(SendMethod, uri, body = null) {
    var xhrResponse = {};

    var xhr = new XMLHttpRequest();

    xhr.open(SendMethod, uri, false);
    xhr.onload = function () {
        if (xhr.status === 200) {
            xhrResponse = xhr.response;
        }
    }
    if (body == null || body == "") xhr.send(null);
    else xhr.send(JSON.stringify(body));
    return xhrResponse;
}

function ConvertJSON(object) {
    return JSON.parse(object);
}