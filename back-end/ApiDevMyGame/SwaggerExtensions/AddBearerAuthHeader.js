$(document).ready(function () {
    $('#input_apiKey').attr("placeholder", "Token de Autorização");

    $('#input_apiKey').change(function () {
        var token = encodeURIComponent($('#input_apiKey')[0].value);

        if (token && token.trim() != "") {
            token = "Bearer " + token;

            var apiKeyAuth = new SwaggerClient.ApiKeyAuthorization("Authorization", token, "header");

            window.swaggerUi.api.clientAuthorizations.add("jsonWebToken", apiKeyAuth);
        }
    });
});