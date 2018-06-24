$(document).ajaxStart(function () {
    $.blockUI({
        message: '<div class=\"row\"><div class=\"col-md-12 col-centered text-center\"><span class\".align-middle\"><strong><i class="fas fa-spinner fa-spin"></i>&nbsp;&nbsp;Carregando...</strong></span></div></div>',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#343A40',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            color: '#FFFFFF'
        }
    });
});

$(document).ajaxStop(function () {
    $.unblockUI();
});

UsuarioAutenticado();

function UsuarioAutenticado() {
    if ((localStorage.token) && (!tokenExpirado())) {
        $('.nav-item').show();
        var token = parseJwt(localStorage.getItem('token'));
        $("#nomeusuario").text(token.unique_name);

        var link = location.href.split("#").slice(-1).toString();
        if ((link == "login") || (link == "novaconta")) {
            location.href = "index.html#jogos";
        }

        return true;
    } else {
        Logout();
        return false;
    }
}

function Login(usuario, senha) {
    var request = $.ajax({
        url: "http://localhost/ApiDevMyGame/oauth2/autenticar",
        method: "POST",
        data: {
            'grant_type': 'password',
            'username': usuario,
            'password': senha
        },
        dataType: "json",
        traditional: true
    });

    request.done(function (data) {
        localStorage.setItem('token', data.access_token);

        var token = parseJwt(data.access_token);
        $("#nomeusuario").text(token.unique_name);

        var requestUsuario = $.ajax({
            url: "http://localhost/ApiDevMyGame/usuarios/recuperar-por-email",
            method: "GET",
            data: {
                "email": token.unique_name
            },
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
            }
        });

        requestUsuario.done(function (retorno) {
            localStorage.setItem('codigoUsuarioLogado', retorno.codigo);
            $('.nav-item').show();
            location.href = "index.html#jogos";
        });

        requestUsuario.fail(function (jXHR) {
            swal({
                title: "Falha no Login",
                text: (jXHR.status == 500) ? jqXHR.responseJSON.exceptionMessage : jqXHR.responseJSON.error,
                type: "warning",
                showCancelButton: false,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Fechar",
                closeOnConfirm: true
            });

            Logout();
        });
    });

    request.fail(function (jqXHR) {
        var msgErro = (jqXHR.status == 500)
            ? jqXHR.responseJSON.exceptionMessage
            : (jqXHR.responseJSON.error.toString().toLowerCase() == "invalid_grant")
                ? "Credenciais de login inválidas."
                : jqXHR.responseJSON.error;

        swal({
            title: "Falha no Login",
            text: msgErro,
            type: "warning",
            showCancelButton: false,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Fechar",
            closeOnConfirm: true
        });
    });
}

function Logout() {
    $('.nav-item').hide();
    localStorage.removeItem('token');
    localStorage.removeItem('codigoUsuarioLogado');
    $("#nomeusuario").text('');

    var link = location.href.split("#").slice(-1).toString();
    if ((location.href.indexOf("#") != -1) && (link != "home") && (link != "login") && (link != "novaconta")) {
        location.href = "index.html#home";
    }
    return;
};

function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace('-', '+').replace('_', '/');
    return JSON.parse(window.atob(base64));
};

function tokenExpirado() {
    if (!localStorage.token) {
        return false;
    }

    var current_time = Date.now() / 1000;
    var token = parseJwt(localStorage.getItem('token'));
    return (token.exp < current_time);
}