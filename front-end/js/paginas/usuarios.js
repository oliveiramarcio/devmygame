app.page("usuarios", function () {
    return function (params) {
        if (UsuarioAutenticado()) {
            var request = $.ajax({
                url: "http://localhost/ApiDevMyGame/usuarios",
                method: "GET",
                data: {},
                dataType: "json",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
                }
            });

            request.done(function (data) {
                $('#tblUsuarios').bootstrapTable({
                    columns: [{
                        field: 'codigo',
                        title: 'C&oacute;digo'
                    }, {
                        field: 'nome',
                        title: 'Nome'
                    }, {
                        field: 'email',
                        title: 'E-mail'
                    }, {
                        field: 'telefone',
                        title: 'Telefone'
                    }, {
                        field: 'dataCadastro',
                        title: 'Data Cadastro'
                    }]
                }).bootstrapTable('load', data);

                $("#info").text("");
            });

            request.fail(function (jqXHR) {
                if (jqXHR.status == 404) {
                    $('#tblUsuarios').bootstrapTable('removeAll');
                    $("#info").text("Nenhum usuário cadastrado.");
                } else {
                    swal({
                        title: "Ops...",
                        text: (jqXHR.status == 500) ? jqXHR.responseJSON.exceptionMessage : jqXHR.responseJSON.error,
                        type: "warning",
                        showCancelButton: false,
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Fechar",
                        closeOnConfirm: true
                    });
                }
            });
        }
    }
});