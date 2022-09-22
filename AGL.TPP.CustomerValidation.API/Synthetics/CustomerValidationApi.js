/**
 * Feel free to explore, or check out the full documentation
 * https://docs.newrelic.com/docs/synthetics/new-relic-synthetics/scripting-monitors/writing-api-tests
 * for details.
 */

var assert = require('assert');
var env = "#{Env}";
var CLIENT_KEY = (env === 'prod' ? $secure.TPP_CUSTOMERVALIDATION_CLIENTKEY_PROD : $secure.TPP_CUSTOMERVALIDATION_CLIENTKEY_NONPROD);
var APIM_SUBSCRIPTION_KEY = (env === 'prod' ? $secure.SYNT_DISCOVERY_APIM_KEY_PROD : $secure.SYNT_DISCOVERY_APIM_KEY_NONPROD);
var API_VERSION = 'v1';
var CORRELATION_ID = 'NR-Synt-' + env;

var envStatusUri = (env === 'prod' ? 'https://api.platform.agl.com.au/partners/sales/customervalidation/status' : `https://api.platform.agl.com.au/${env}/partners/sales/customervalidation/status`);


var statusEndpoint = {
    //Define endpoint URI
    uri: envStatusUri,
    //Define query key and expected data type.
    headers: {
        'Status-Client-Key': CLIENT_KEY,
        'Api-Version': API_VERSION,
        'Ocp-Apim-Subscription-Key': APIM_SUBSCRIPTION_KEY,
        'Correlation-Id': CORRELATION_ID
    }
};


// Callback
function statusEndpointCallback(err, response, body) {
    console.log(response.request.uri.pathname);
    if (response != undefined) {
        // any statuscode other than 200, alert
        console.log('StatusCode:', response.statusCode);
        assert.equal(response.statusCode, 200, 'Customer Validation Search API - Expected a 200 OK response');

        var responseContent = JSON.parse(body)

        console.log('Response:', responseContent);
        assert.equal(responseContent.isHealthy, true, 'Expected is_healthy equals');
        assert.equal
    }
    else {
        assert.fail("Address search API seems be down.");
    }
}

$http.get(statusEndpoint, statusEndpointCallback);
