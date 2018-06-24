app.page("login", function () {
    return function (params) {
        $("#formLogin").trigger("reset");

        $("#formLogin").unbind().bind("submit", (function (e) {
            e.preventDefault();
            var usuario = $('#inputEmail').val();
            var senha = $('#inputSenha').val();

            Login(usuario, senha);
        }));

        $("#btnCriarConta").unbind().bind("click", (function () {
            location.href = "index.html#novaconta";
        }));
    }
});