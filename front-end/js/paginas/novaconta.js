app.page("novaconta", function () {
  return function (params) {
    $("#formNovaConta").trigger("reset");

    $("#formNovaConta").unbind().bind("submit", (function (e) {
      e.preventDefault();
      debugger;
      var nomeCompleto = $('#inputNomeCompleto').val();
      var email = $('#inputEmail').val();
      var senha = $('#inputSenha').val();
      var confirmarSenha = $('#inputConfirmarSenha').val();
      var telefone = $('#inputTelefone').val();

      if (senha != confirmarSenha) {
        swal({
          title: "Ops...",
          text: "A senha e a confirmação de  senha não conferem.",
          type: "warning",
          showCancelButton: false,
          confirmButtonClass: "btn-danger",
          confirmButtonText: "Fechar",
          closeOnConfirm: true
        });
      } else {
        var request = $.ajax({
          url: "http://localhost/ApiDevMyGame/usuarios",
          method: "POST",
          data: {
            'nome': nomeCompleto,
            'email': email,
            'senha': senha,
            'telefone': telefone
          },
          dataType: "json"
        });

        request.done(function (data) {
          Login(email, senha);
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
      }
    }));
  }
});