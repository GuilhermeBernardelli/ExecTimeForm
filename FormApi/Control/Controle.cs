using FormApi.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System;

namespace FormApi.Control
{
    public class Controle: DbContext
    {
        //Objeto da camada Model
        Repository dbRepository = new Repository();

        /*
         * Controle geral
         */

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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

        public List<Questionarios> pesquisaGeralQuestionario(string valor)
        {
            string pesquisa = valor;

            return dbRepository.pesquisaGeralQuestionario(pesquisa);
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

    }
}