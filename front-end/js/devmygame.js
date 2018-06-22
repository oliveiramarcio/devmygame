$(document).ready(function () {
    $("#formLogin").submit(function () {
		$("#inputEmail").prop("disabled", true);
		$("#inputSenha").prop("disabled", true);
		$("#btnEntrar").prop("disabled", true);
		$("#btnNovaConta").prop("disabled", true);
		$("#recuperarSenha").prop("disabled", true);
		$("#logo").addClass('fa-spin');
		var grant_type = "password";
        var usuario = $('#inputEmail').val();
        var senha = $('#inputSenha').val();
		var dados = 'grant_type=password&username=' + usuario + '&password=' + senha;
		$.ajax({
  type: 'POST',
  url: 'http://localhost/ApiDevMyGame/oauth2/tokens',
  data: dados,
  dataType: 'json',
  success: function(data, status) {
	$("#logo").removeClass('fa-spin');
    console.log('The returned data', data);
  }
});
});
});