using RenderApi.Model;
using RenderApi.Control;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RenderApi.Control
{
    public class Controle: DbContext
    {
        Repository dbRepository = new Repository();

        /*
         * Controle geral
         */

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public void construtor_formulario(int quest, int render, string usuario)
        {
            int id_quest = quest;
            int id_render = render;
            string user = usuario;

            dbRepository.rodaProcedure(id_quest, id_render, user);
        }

        public void atualizarDados()
        {
            dbRepository.salvarAlteracoes();
        }

        /*
         * Controle Questionario
         */

        public void salvarQuestionario(Questionarios questionario)
        {
            dbRepository.salvarNovoQuestionario(questionario);
        }

        public void excluirQuestionario(Questionarios questionario)
        {
            dbRepository.excluirDadosQuestionario(questionario);
        }

        public List<Questionarios> pesquisaGeralQuestionario()
        {         
            return dbRepository.pesquisaGeralQuestionario();
        }

        public Questionarios pesquisaQuestionarioNome(string valor)
        {
            string pesquisa = valor;

            return dbRepository.pesquisaQuestionarioPorNome(pesquisa);
        }

        public Questionarios pesquisaQuestionarioId(int ID)
        {
            int pesquisa = ID;

            return dbRepository.pesquisaQuestionarioPorId(pesquisa);
        }

        public int pesquisaIntQuestionario()
        {
            return dbRepository.pesquisaIdQuestionario();
        }

        /*
        * Controle Perguntas
        */

        public void salvarPergunta(Perguntas pergunta)
        {
            dbRepository.salvarNovaPergunta(pergunta);
        }

        public void excluirPergunta(Perguntas pergunta)
        {
            dbRepository.excluirDadosPergunta(pergunta);
        }

        public List<Perguntas> pesquisaGeralPergunta(string valor)
        {
            string pesquisa = valor;

            return dbRepository.pesquisaGeralPergunta(pesquisa);
        }

        public List<Perguntas> pesquisaPerguntaQuestionario(int valor)
        {
            int pesquisa = valor;

            return dbRepository.pesquisaPerguntaPorQuestionario(pesquisa);
        }               

        public Perguntas pesquisaPerguntaNome(string valor)
        {
            string pesquisa = valor;

            return dbRepository.pesquisaPerguntaPorNome(pesquisa);
        }

        public Perguntas pesquisaPerguntaId(int ID)
        {
            int pesquisa = ID;

            return dbRepository.pesquisaPerguntaPorId(pesquisa);
        }

        /*
        * Controle Respostas
        */

        public void salvarResposta(Respostas resposta)
        {
            dbRepository.salvarNovaResposta(resposta);
        }

        public void excluirResposta(Respostas resposta)
        {
            dbRepository.excluirDadosResposta(resposta);
        }

        public List<Respostas> pesquisaGeralResposta(string valor)
        {
            string pesquisa = valor;

            return dbRepository.pesquisaGeralRespostas(pesquisa);
        }

        public List<Respostas> pesquisaRespostaQuestão(int valor)
        {
            int pesquisa = valor;

            return dbRepository.pesquisaRespostasPorPergunta(pesquisa);
        }

        public Respostas pesquisaRespostaNome(string valor)
        {
            string pesquisa = valor;

            return dbRepository.pesquisaRespostaPorNome(pesquisa);
        }

        public Respostas pesquisaRespostaId(int ID)
        {
            int pesquisa = ID;

            return dbRepository.pesquisaRespostaPorId(pesquisa);
        }

        /*
         * Pesquisa de tipos
         */
        public List<Tipos> pesquisaTipos()
        {
            return dbRepository.pesquisaTodosTipos();
        }

        public Tipos pesquisaTiposNome(string valor)
        {
            string pesquisa = valor;

            return dbRepository.pesquisaValorTipo(pesquisa);
        }

        /*
        * Pesquisa de Usuários
        */
        public List<Usuarios> pesquisaUser()
        {
            return dbRepository.pesquisaTodosUsers();
        }

        public Usuarios pesquisaUsuarioReg(int valor)
        {
            int pesquisa = valor;

            return dbRepository.pesquisaUsuarioReg(pesquisa);
        }

        /*
        * Pesquisa de Renderização
        */

        public List<Renderizar> pesquisaRenderizarReg(int valor)
        {
            int pesquisa = valor;

            return dbRepository.pesquisaRenderizarReg(pesquisa);
        }

        public Renderizar pesquisaRenderizarIdUser(int valor, int user)
        {
            int pesquisa = valor;
            int pesquisaUser = user;

            return dbRepository.pesquisaRenderizarId_User(pesquisa, pesquisaUser);
        }

        public void salvarPreenchimento(Prenchimentos preenchimento)
        {
            dbRepository.salvarNovoPreenchimento(preenchimento);
        }

        public Prenchimentos pesquisaPreenchimentoId(int valor)
        {
            int pesquisa = valor;

            return dbRepository.pesquisaPrenchimentoId(pesquisa);
        }

        public List<Prenchimentos> pesquisaPreenchimento_render_user( int id, string text)
        {
            int render = id;
            string user = text;
            return dbRepository.pesquisaPreenchimentoRenderUser(render, user);
        }

        public Prenchimentos pesquisaPreenchimento_perg_render(int pergunta, int render_id)
        {
            int perg = pergunta;
            int render = render_id;
            
            return dbRepository.pesquisaPreenchimentoRender(perg, render);
        }

        public Prenchimentos pesquisaPreenchimentoValor(string selectedValue, int id)
        {
            string pesquisa = selectedValue;
            int render = id;

            return dbRepository.pesquisaPreenchimentoValorResp(pesquisa , render);
        }
    }
}