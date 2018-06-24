app.page("incluirjogo", function () {
  return function (params) {
    if (UsuarioAutenticado()) {
      $("#formIncluirJogo").trigger("reset");

      $("#formIncluirJogo").unbind().bind("submit", (function (e) {
        e.preventDefault();
        var nomeJogo = $('#inputNomeJogo').val();
        var codigoUsuarioDono = localStorage.getItem('codigoUsuarioLogado');

        var request = $.ajax({
          url: "http://localhost/ApiDevMyGame/jogos",
          method: "POST",
          data: {
            'nome': nomeJogo,
            'codigoUsuarioDono': codigoUsuarioDono
          },
          dataType: "json",
          beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
          }
        });

        request.done(function (data) {
          location.href = "index.html#jogos";
        });

        request.fail(function (jqXHR) {
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
      }));
    }
  }
});