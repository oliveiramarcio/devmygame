using Aplicacao.Interfaces.Cadastros;
using AutoMapper;
using Biblioteca.Excecoes;
using Biblioteca.Interfaces;
using DataTransfer.Requests.Cadastros;
using DataTransfer.Responses.Cadastros;
using Dominio.Entidades.Cadastros;
using Infra.Interfaces.Cadastros;
using System.Collections.Generic;
using System.Linq;

namespace Aplicacao.Servicos.Cadastros
{
    public class UsuarioAppService : IAppServiceBase, IUsuarioAppService
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private MapperConfiguration configuracoesDeMapeamento;

        public UsuarioAppService(IUsuarioRepositorio usuarioRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;

            this.ConfigurarMapeamento();
        }

        public void ConfigurarMapeamento()
        {
            this.configuracoesDeMapeamento = new MapperConfiguration(cfg => {
                cfg.CreateMap<Usuario, UsuarioResponse>()
                    .ForMember(a => a.DataCadastro, b => b.MapFrom(c => c.DataCadastro.ToString("dd/MM/yyyy hh:mm:ss")));
            });

            this.configuracoesDeMapeamento.AssertConfigurationIsValid();
        }

        public IEnumerable<UsuarioResponse> Listar(ListarUsuarioRequest request)
        {
            if (request == null)
            {
                request = new ListarUsuarioRequest();
            }

            if ((request != null) && (request.Codigo != null) && (request.Codigo <= 0))
            {
                throw new ParametroInvalidoExcecao("Favor informar um código de usuário válido");
            }
            
            var listaUsuarios = this.usuarioRepositorio.Listar(request.Codigo, request.Email, null);

            return this.configuracoesDeMapeamento.CreateMapper().Map<IEnumerable<Usuario>, IEnumerable<UsuarioResponse>>(listaUsuarios);
        }

        public UsuarioResponse RecuperarPorCodigo(int codigo)
        {
            if (codigo <= 0)
            {
                throw new ParametroInvalidoExcecao("Favor informar um código de usuário válido");
            }

            Usuario usuario = this.usuarioRepositorio.RecuperarPorCodigo(codigo);

            return this.configuracoesDeMapeamento.CreateMapper().Map<Usuario, UsuarioResponse>(usuario);
        }

        public UsuarioResponse RecuperarPorEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ParametroInvalidoExcecao("Favor informar um email válido");
            }

            Usuario usuario = this.usuarioRepositorio.RecuperarPorEmail(email);

            return this.configuracoesDeMapeamento.CreateMapper().Map<Usuario, UsuarioResponse>(usuario);
        }

        public UsuarioResponse Inserir(InserirUsuarioRequest request)
        {
            if (request == null)
            {
                throw new RequestInvalidoExcecao();
            }

            if (string.IsNullOrWhiteSpace(request.Nome))
            {
                throw new ParametroInvalidoExcecao("Favor informar um nome");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ParametroInvalidoExcecao("Favor informar um e-mail");
            }

            if (string.IsNullOrWhiteSpace(request.Senha))
            {
                throw new ParametroInvalidoExcecao("Favor informar uma senha");
            }

            if (string.IsNullOrWhiteSpace(request.Telefone))
            {
                throw new ParametroInvalidoExcecao("Favor informar um telefone");
            }

            if (this.usuarioRepositorio.RecuperarPorEmail(request.Email) != null)
            {
                throw new RegraDeNegocioExcecao("O e-mail informado já está cadastrado no D3vMyGame!");
            }

            Usuario novoUsuario = this.usuarioRepositorio.Inserir(request.Nome, request.Email, request.Senha, request.Telefone);

            return this.configuracoesDeMapeamento.CreateMapper().Map<Usuario, UsuarioResponse>(novoUsuario);
        }

        public UsuarioResponse Atualizar(AtualizarUsuarioRequest request)
        {
            if (request == null)
            {
                throw new RequestInvalidoExcecao();
            }

            if (string.IsNullOrWhiteSpace(request.Nome))
            {
                throw new ParametroInvalidoExcecao("Favor informar um nome");
            }

            if (string.IsNullOrWhiteSpace(request.Senha))
            {
                throw new ParametroInvalidoExcecao("Favor informar uma senha");
            }

            if (string.IsNullOrWhiteSpace(request.Telefone))
            {
                throw new ParametroInvalidoExcecao("Favor informar um telefone");
            }

            Usuario usuario = this.usuarioRepositorio.RecuperarPorCodigo(request.Codigo);

            if (usuario != null)
            {
                Usuario usuarioAtualizado = this.usuarioRepositorio.Atualizar(request.Codigo, request.Nome, request.Senha, request.Telefone);

                return this.configuracoesDeMapeamento.CreateMapper().Map<Usuario, UsuarioResponse>(usuarioAtualizado);
            }
            else
            {
                throw new ParametroInvalidoExcecao("O usuário informado não existe.");
            }
        }

        public bool Excluir(int codigo)
        {
            if (codigo <= 0)
            {
                throw new ParametroInvalidoExcecao("Favor informar um código de usuário válido");
            }

            Usuario usuario = this.usuarioRepositorio.RecuperarPorCodigo(codigo);

            bool retorno = false;

            if (usuario != null)
            {
                retorno = this.usuarioRepositorio.Excluir(codigo);
            }

            return retorno;
        }

        public bool AutenticarUsuario(string email, string senha)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ParametroInvalidoExcecao("Favor informar um e-mail");
            }

            if (string.IsNullOrEmpty(senha))
            {
                throw new ParametroInvalidoExcecao("Favor informar uma senha");
            }

            return this.usuarioRepositorio.AutenticarUsuario(email, senha);
        }
    }
}