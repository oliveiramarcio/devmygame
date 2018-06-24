app.page("jogos", function () {
    return function (params) {
        if (UsuarioAutenticado()) {
            var $tabela = $('#tblJogos');
            var $btnIncluirJogo = $('#btnIncluirJogo');
            var $btnEmprestar = $('#btnEmprestar');
            var $formConfirmarEmprestimo = $('#formConfirmarEmprestimo');
            var $btnCancelarEmprestimo = $("#btnCancelarEmprestimo");
            var $btnDevolver = $('#btnDevolver');
            var $btnExcluir = $('#btnExcluir');

            preencherSelectUsuarioEmprestimo();

            $tabela.on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
                var possuiRegistros = (selecionados().length > 0);
                var possuiJogosParaEmprestar = (selecionadosParaEmprestimo().length > 0);
                var possuiJogosParaDevolver = (selecionadosParaDevolver().length > 0);

                $btnEmprestar.prop('disabled', ((!possuiRegistros) || (!possuiJogosParaEmprestar)));
                $btnDevolver.prop('disabled', ((!possuiRegistros) || (!possuiJogosParaDevolver)));
                $btnExcluir.prop('disabled', (!(possuiRegistros)));
            });

            $btnIncluirJogo.unbind().bind("click", (function () {
                location.href = "index.html#incluirjogo";
            }));

            $btnEmprestar.unbind().bind("click", (function () {
                $("#selectUsuarioEmprestimo").val("");

                $.blockUI({
                    message: $("#selecionarUsuarioEmprestimo"),
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#343A40',
                        '-webkit-border-radius': '10px',
                        '-moz-border-radius': '10px',
                        color: '#FFFFFF'
                    }
                });
            }));

            $formConfirmarEmprestimo.unbind().bind("submit", (function (e) {
                e.preventDefault();
                $.unblockUI();

                var codigoUsuarioEmprestimo = $("#selectUsuarioEmprestimo").val();
                var ids = selecionadosParaEmprestimo();

                var dados = {
                    'CodigoUsuarioEmprestimo': codigoUsuarioEmprestimo,
                    'CodigosJogos': ids
                };

                var requestEmprestimo = $.ajax({
                    url: "http://localhost/ApiDevMyGame/jogos/emprestar",
                    method: "POST",
                    data: dados,
                    dataType: "json",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
                    }
                });

                requestEmprestimo.done(function (data) {
                    ListarJogos();
                    desabilitarBotoes();
                });

                requestEmprestimo.fail(function (jqXHR) {
                    swal({
                        title: "Ops...",
                        text: (jqXHR.status == 500) ? jqXHR.responseJSON.exceptionMessage : jqXHR.responseJSON.error,
                        type: "warning",
                        showCancelButton: false,
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Fechar",
                        closeOnConfirm: true
                    });

                    desabilitarBotoes();
                });
            }));

            $btnCancelarEmprestimo.unbind().bind("click", (function () {
                $.unblockUI();
            }));

            $btnDevolver.unbind().bind("click", (function () {
                var ids = selecionadosParaDevolver();

                var requestDevolucao = $.ajax({
                    url: "http://localhost/ApiDevMyGame/jogos/devolver",
                    method: "POST",
                    data: {
                        '': ids
                    },
                    dataType: "json",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
                    }
                });

                requestDevolucao.done(function (data) {
                    ListarJogos();
                    desabilitarBotoes();
                });

                requestDevolucao.fail(function (jqXHR) {
                    swal({
                        title: "Ops...",
                        text: (jqXHR.status == 500) ? jqXHR.responseJSON.exceptionMessage : jqXHR.responseJSON.error,
                        type: "warning",
                        showCancelButton: false,
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Fechar",
                        closeOnConfirm: true
                    });

                    desabilitarBotoes();
                });
            }));

            $btnExcluir.unbind().bind("click", (function () {
                var ids = selecionados();

                var requestExclusao = $.ajax({
                    url: "http://localhost/ApiDevMyGame/jogos",
                    method: "DELETE",
                    data: {
                        '': ids
                    },
                    dataType: "json",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
                    }
                });

                requestExclusao.done(function (data) {
                    $tabela.bootstrapTable('remove', {
                        field: 'codigo',
                        values: ids
                    });

                    desabilitarBotoes();
                });

                requestExclusao.fail(function (jqXHR) {
                    swal({
                        title: "Ops...",
                        text: (jqXHR.status == 500) ? jqXHR.responseJSON.exceptionMessage : jqXHR.responseJSON.error,
                        type: "warning",
                        showCancelButton: false,
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Fechar",
                        closeOnConfirm: true
                    });

                    desabilitarBotoes();
                });
            }));

            function preencherSelectUsuarioEmprestimo() {
                var codigoUsuarioLogado = localStorage.getItem('codigoUsuarioLogado');

                var requestListarUsuarios = $.ajax({
                    url: "http://localhost/ApiDevMyGame/usuarios",
                    method: "GET",
                    data: {},
                    dataType: "json",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
                    }
                });

                requestListarUsuarios.done(function (data) {
                    $("#selectUsuarioEmprestimo").children("option:not(:first)").remove();

                    $.each(data, function (index, arr) {
                        if (arr.codigo != codigoUsuarioLogado) {
                            $("#selectUsuarioEmprestimo")
                                .append($("<option></option>")
                                    .attr("value", arr.codigo)
                                    .text(arr.nome));
                        }
                    });

                    ListarJogos();
                });

                requestListarUsuarios.fail(function (jqXHR) {
                    swal({
                        title: "Ops...",
                        text: (jqXHR.status == 500) ? jqXHR.responseJSON.exceptionMessage : jqXHR.responseJSON.error,
                        type: "warning",
                        showCancelButton: false,
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Fechar",
                        closeOnConfirm: true
                    });
                });
            }

            function ListarJogos() {
                var codigoUsuarioLogado = localStorage.getItem('codigoUsuarioLogado');

                var request = $.ajax({
                    url: "http://localhost/ApiDevMyGame/jogos",
                    method: "GET",
                    data: {
                        "codigoUsuarioDono": codigoUsuarioLogado
                    },
                    dataType: "json",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
                    }
                });

                request.done(function (data) {
                    $tabela.bootstrapTable({
                        columns: [{
                            field: 'selecao',
                            checkbox: true,
                            align: 'center',
                            valign: 'middle'
                        }, {
                            field: 'codigo',
                            title: 'C&oacute;digo'
                        }, {
                            field: 'nome',
                            title: 'Nome'
                        }, {
                            field: 'dataCadastro',
                            title: 'Data Cadastro'
                        }, {
                            field: 'nomeUsuarioEmpresitmo',
                            title: 'Usu&aacute;rio Empr&eacute;stimo'
                        }, {
                            field: 'dataEmprestimo',
                            title: 'Data Empr&eacute;stimo'
                        }, {
                            field: 'quantidadeDiasEmprestimo',
                            title: 'Dias Empr&eacute;stimo'
                        }]
                    }).bootstrapTable('load', data);

                    $tabela.bootstrapTable('uncheckAll');

                    $("#info").text("");
                });

                request.fail(function (jqXHR) {
                    if (jqXHR.status == 404) {
                        $tabela.bootstrapTable('removeAll');
                        $("#info").text("Nenhum jogo cadastrado.");
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

            function selecionados() {
                return $.map($tabela.bootstrapTable('getSelections'), function (row) {
                    return row.codigo
                });
            }

            function selecionadosParaDevolver() {
                var registros = $tabela.bootstrapTable('getSelections').filter(function (obj) {
                    return obj.dataEmprestimo != null
                });

                return $.map(registros, function (row) {
                    return row.codigo
                });
            }

            function selecionadosParaEmprestimo() {
                var registros = $tabela.bootstrapTable('getSelections').filter(function (obj) {
                    return obj.dataEmprestimo == null
                });

                return $.map(registros, function (row) {
                    return row.codigo
                });
            }

            function desabilitarBotoes() {
                $btnEmprestar.prop('disabled', true);
                $btnDevolver.prop('disabled', true);
                $btnExcluir.prop('disabled', true);
            }
        }
    }
});