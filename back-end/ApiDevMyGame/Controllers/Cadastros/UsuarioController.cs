using ApiDevMyGame.SwaggerExtensions;
using Aplicacao.Interfaces.Cadastros;
using DataTransfer.Requests.Cadastros;
using DataTransfer.Responses.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace ApiDevMyGame.Controllers.Cadastros
{
    /// <summary>
    /// 
    /// </summary>
    [GroupSwagger("Usuários")]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioAppService usuarioAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioAppService"></param>
        public UsuarioController(IUsuarioAppService usuarioAppService)
        {
            this.usuarioAppService = usuarioAppService;
        }

        /// <summary>
        /// Lista os usuários cadastrados.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("usuarios")]
        [ResponseType(typeof(IEnumerable<UsuarioResponse>))]
        public IHttpActionResult Listar([FromUri] ListarUsuarioRequest request)
        {
            var response = this.usuarioAppService.Listar(request);

            if ((response == null) || (response.Count() == 0))
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Recupera um usuário
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("usuarios/{codigo:int}")]
        [ResponseType(typeof(UsuarioResponse))]
        public IHttpActionResult Recuperar([FromUri] int codigo)
        {
            var response = this.usuarioAppService.Recuperar(codigo);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Insere um novo usuário (não requer autenticação)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("usuarios")]
        [ResponseType(typeof(UsuarioResponse))]
        public IHttpActionResult Inserir([FromBody] InserirUsuarioRequest request)
        {
            var response = this.usuarioAppService.Inserir(request);

            return Created(new Uri(Request.RequestUri + "/" + response.Codigo.ToString()), response);
        }

        /// <summary>
        /// Atualiza um usuário
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("usuarios")]
        [ResponseType(typeof(UsuarioResponse))]
        public IHttpActionResult Atualizar([FromBody] AtualizarUsuarioRequest request)
        {
            var response = this.usuarioAppService.Atualizar(request);

            return Ok(response);
        }

        /// <summary>
        /// Exclui um usuário
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("usuarios/{codigo:int}")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Excluir([FromUri] int codigo)
        {
            return Ok(this.usuarioAppService.Excluir(codigo));
        }
    }
}