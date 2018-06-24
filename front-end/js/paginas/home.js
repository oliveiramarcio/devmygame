app.page("home", function () {
    return function (params) {
        if ((localStorage.token) && (!tokenExpirado())) {
            $("#btnEntrar").hide();
            $("#btnNovaConta").hide();
            $("#info").text("Utilize o menu do topo para acessar o D3vMyGame!");
        } else {
            $("#btnEntrar").show();
            $("#btnNovaConta").show();
            $("#info").text("");
        }

        $("#btnEntrar").unbind().bind("click", (function (e) {
            location.href = "index.html#login";
        }));

        $("#btnNovaConta").unbind().bind("click", (function () {
            location.href = "index.html#novaconta";
        }));
    }
});