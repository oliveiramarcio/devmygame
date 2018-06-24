using Aplicacao.Interfaces.Cadastros;
using AutoMapper;
using Biblioteca.Excecoes;
using Biblioteca.Interfaces;
using DataTransfer.Requests.Cadastros;
using DataTransfer.Responses.Cadastros;
using Dominio.Entidades.Cadastros;
using Infra.Interfaces.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aplicacao.Servicos.Cadastros
{
    public class JogoAppService : IAppServiceBase, IJogoAppService
    {
        private readonly IJogoRepositorio jogoRepositorio;
        private MapperConfiguration configuracoesDeMapeamento;

        public JogoAppService(IJogoRepositorio jogoRepositorio)
        {
            this.jogoRepositorio = jogoRepositorio;

            this.ConfigurarMapeamento();
        }

        public void ConfigurarMapeamento()
        {
            this.configuracoesDeMapeamento = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Jogo, JogoResponse>()
                    .ForMember(a => a.DataCadastro, b => b.MapFrom(c => c.DataCadastro.ToString("dd/MM/yyyy hh:mm:ss")))
                    .ForMember(a => a.DataEmprestimo, b => b.MapFrom(c => (c.DataEmprestimo == null) ? null : ((DateTime)c.DataEmprestimo).ToString("dd/MM/yyyy hh:mm:ss")));
            });

            this.configuracoesDeMapeamento.AssertConfigurationIsValid();
        }

        public IEnumerable<JogoResponse> Listar(ListarJogoRequest request)
        {
            if (request == null)
            {
                request = new ListarJogoRequest();
            }

            if ((request != null) && (request.Codigo != null) && (request.Codigo <= 0))
            {
                throw new ParametroInvalidoExcecao("Favor informar um código de jogo válido");
            }

            var listaJogos = this.jogoRepositorio.Listar(request.Codigo, request.Nome, request.CodigoUsuarioDono);

            return this.configuracoesDeMapeamento.CreateMapper().Map<IEnumerable<Jogo>, IEnumerable<JogoResponse>>(listaJogos);
        }

        public JogoResponse Recuperar(int codigo)
        {
            if (codigo <= 0)
            {
                throw new ParametroInvalidoExcecao("Favor informar um código de jogo válido");
            }

            Jogo jogo = this.jogoRepositorio.Recuperar(codigo);

            return this.configuracoesDeMapeamento.CreateMapper().Map<Jogo, JogoResponse>(jogo);
        }

        public JogoResponse Inserir(InserirJogoRequest request)
        {
            if (request == null)
            {
                throw new RequestInvalidoExcecao();
            }

            if (string.IsNullOrWhiteSpace(request.Nome))
            {
                throw new ParametroInvalidoExcecao("Favor informar um nome");
            }

            if (request.CodigoUsuarioDono <= 0)
            {
                throw new ParametroInvalidoExcecao("Favor informar um código de usuário dono");
            }

            if (this.jogoRepositorio.Listar(null, request.Nome, request.CodigoUsuarioDono).FirstOrDefault() != null)
            {
                throw new RegraDeNegocioExcecao("Você já possui um jogo cadastrado com o nome informado.");
            }

            Jogo novoJogo = this.jogoRepositorio.Inserir(request.Nome, request.CodigoUsuarioDono);

            return this.configuracoesDeMapeamento.CreateMapper().Map<Jogo, JogoResponse>(novoJogo);
        }

        public JogoResponse Atualizar(AtualizarJogoRequest request)
        {
            if (request == null)
            {
                throw new RequestInvalidoExcecao();
            }

            if (request.Codigo <= 0)
            {
                throw new ParametroInvalidoExcecao("Favor informar um código de jogo válido");
            }

            if (string.IsNullOrWhiteSpace(request.Nome))
            {
                throw new ParametroInvalidoExcecao("Favor informar um nome");
            }

            Jogo jogo = this.jogoRepositorio.Recuperar(request.Codigo);

            if (jogo != null)
            {
                Jogo jogoAtualizado = this.jogoRepositorio.Atualizar(request.Codigo, request.Nome);

                return this.configuracoesDeMapeamento.CreateMapper().Map<Jogo, JogoResponse>(jogoAtualizado);
            }
            else
            {
                throw new ParametroInvalidoExcecao("O jogo informado não existe.");
            }
        }

        public bool Excluir(int[] codigos)
        {
            if (codigos.Length == 0)
            {
                throw new ParametroInvalidoExcecao("Favor informar um código de jogo válido");
            }

            foreach (int codigo in codigos)
            {
                Jogo jogo = this.jogoRepositorio.Recuperar(codigo);

                if (jogo != null)
                {
                    this.jogoRepositorio.Excluir(codigo);
                }
            }

            return true;
        }

        public bool Devolver(int[] codigosJogos)
        {
            if (codigosJogos.Length == 0)
            {
                throw new ParametroInvalidoExcecao("Favor informar um código de jogo válido");
            }

            foreach (int codigoJogo in codigosJogos)
            {
                Jogo jogo = this.jogoRepositorio.Recuperar(codigoJogo);

                if ((jogo != null) && (jogo.DataEmprestimo != null))
                {
                    this.jogoRepositorio.Devolver(codigoJogo);
                }
            }

            return true;
        }

        public bool Emprestar(EmprestarJogoRequest request)
        {
            if (request == null)
            {
                throw new RequestInvalidoExcecao();
            }

            if (request.CodigoUsuarioEmprestimo <= 0)
            {
                throw new ParametroInvalidoExcecao("Favor informar um código de usuário para empréstimo válido");
            }

            if (request.CodigosJogos.Length == 0)
            {
                throw new ParametroInvalidoExcecao("Favor informar um código de jogo válido");
            }

            foreach (int codigoJogo in request.CodigosJogos)
            {
                Jogo jogo = this.jogoRepositorio.Recuperar(codigoJogo);

                if ((jogo != null) && (jogo.DataEmprestimo == null))
                {
                    this.jogoRepositorio.Emprestar(request.CodigoUsuarioEmprestimo, codigoJogo);
                }
            }

            return true;
        }
    }
}