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
    [GroupSwagger("Jogos")]
    public class JogoController : ApiController
    {
        private readonly IJogoAppService jogoAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jogoAppService"></param>
        public JogoController(IJogoAppService jogoAppService)
        {
            this.jogoAppService = jogoAppService;
        }

        /// <summary>
        /// Lista os jogos cadastrados
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("jogos")]
        [ResponseType(typeof(IEnumerable<JogoResponse>))]
        public IHttpActionResult Listar([FromUri] ListarJogoRequest request)
        {
            var response = this.jogoAppService.Listar(request);

            if ((response == null) || (response.Count() == 0))
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Recupera um jogo
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("jogos/{codigo:int}")]
        [ResponseType(typeof(JogoResponse))]
        public IHttpActionResult Recuperar([FromUri] int codigo)
        {
            var response = this.jogoAppService.Recuperar(codigo);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Insere um novo jogo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("jogos")]
        [ResponseType(typeof(JogoResponse))]
        public IHttpActionResult Inserir([FromBody] InserirJogoRequest request)
        {
            var response = this.jogoAppService.Inserir(request);

            return Created(new Uri(Request.RequestUri + "/" + response.Codigo.ToString()), response);
        }

        /// <summary>
        /// Atualiza um jogo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("jogos")]
        [ResponseType(typeof(JogoResponse))]
        public IHttpActionResult Atualizar([FromBody] AtualizarJogoRequest request)
        {
            var response = this.jogoAppService.Atualizar(request);

            return Ok(response);
        }

        /// <summary>
        /// Exclui um ou mais jogos
        /// </summary>
        /// <param name="codigos"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("jogos")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Excluir([FromBody] int[] codigos)
        {
            return Ok(this.jogoAppService.Excluir(codigos));
        }

        /// <summary>
        /// Devolve um ou mais jogos emprestados
        /// </summary>
        /// <param name="codigosJogos"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("jogos/devolver")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Devolver([FromBody] int[] codigosJogos)
        {
            return Ok(this.jogoAppService.Devolver(codigosJogos));
        }

        /// <summary>
        /// Empresta um ou mais jogos para um usuário
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("jogos/emprestar")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Emprestar([FromBody] EmprestarJogoRequest request)
        {
            return Ok(this.jogoAppService.Emprestar(request));
        }
    }
}