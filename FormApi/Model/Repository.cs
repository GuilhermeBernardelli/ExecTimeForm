using System.Collections.Generic;
using System.Linq;

namespace FormApi.Model
{
    public class Repository
    {
        DbEntities entityModel = new DbEntities();

        /*
         * Realizando alterações no banco
         */
        public void salvarAlteracoes()
        {
            entityModel.SaveChanges();
        }

        public void salvarNovoQuestionario(Questionarios questionario)
        {
            entityModel.Questionarios.Add(questionario);
        }

        public void excluirDadosQuestionario(Questionarios questionario)
        {
            entityModel.Questionarios.Remove(questionario);
        }

        public void salvarNovaPergunta(Perguntas pergunta)
        {
            entityModel.Perguntas.Add(pergunta);
        }

        public void excluirDadosPergunta(Perguntas pergunta)
        {
            entityModel.Perguntas.Remove(pergunta);
        }

        public void salvarNovaResposta(Respostas resposta)
        {
            entityModel.Respostas.Add(resposta);
        }

        public void excluirDadosResposta(Respostas resposta)
        {
            entityModel.Respostas.Remove(resposta);
        }

        /*
         * Pesquisa Questionario
         */

        public List<Questionarios> pesquisaGeralQuestionario(string valor)
        {
            return (from questionario in entityModel.Questionarios
                    where (questionario.nome.Contains(valor)
                    && questionario.ativo == true)                    
                    select questionario).ToList();
        }

        public Questionarios pesquisaQuestionarioPorNome(string valor)
        {
            return (from questionario in entityModel.Questionarios
                    where (questionario.nome.Contains(valor))
                    select questionario).SingleOrDefault();
        }

        public Questionarios pesquisaQuestionarioPorId(int Id)
        {
            return (from questionario in entityModel.Questionarios
                    where (questionario.id == (Id))
                    select questionario).SingleOrDefault();
        }

        public int pesquisaIdQuestionario()
        {
            return (from questionario in entityModel.Questionarios
                    select questionario).Count();
        }

        /*
         * Pesquisa Pergunta
         */

        public List<Perguntas> pesquisaGeralPergunta(string valor)
        {
            return (from pergunta in entityModel.Perguntas
                    where (pergunta.titulo.Contains(valor))
                    select pergunta).ToList();
        }

        public List<Perguntas> pesquisaPerguntaPorQuestionario(int valor)
        {
            return (from pergunta in entityModel.Perguntas
                    where (pergunta.id_questionario == valor)
                    select pergunta).ToList();
        }

        public Perguntas pesquisaPerguntaPorNome(string valor)
        {
            return (from pergunta in entityModel.Perguntas
                    where (pergunta.titulo.Contains(valor))
                    select pergunta).SingleOrDefault();
        }

        public Perguntas pesquisaPerguntaPorId(int Id)
        {
            return (from pergunta in entityModel.Perguntas
                    where (pergunta.id == (Id))
                    select pergunta).SingleOrDefault();
        }

        /*
         * Pesquisa Resposta
         */

        public List<Respostas> pesquisaGeralRespostas(string valor)
        {
            return (from resposta in entityModel.Respostas
                    where (resposta.resposta.Contains(valor))
                    select resposta).ToList();
        }

        public List<Respostas> pesquisaRespostasPorPergunta(int id_pergunta)
        {
            return (from resposta in entityModel.Respostas
                    where (resposta.id_pergunta == id_pergunta)
                    orderby resposta.ordem ascending
                    select resposta).ToList();
        }

        public Respostas pesquisaRespostaPorNome(string valor)
        {
            return (from resposta in entityModel.Respostas
                    where (resposta.resposta.Contains(valor))
                    select resposta).SingleOrDefault();
        }

        public Respostas pesquisaRespostaPorId(int Id)
        {
            return (from resposta in entityModel.Respostas
                    where (resposta.id == (Id))
                    select resposta).SingleOrDefault();
        }

        /*
         * Pesquisa de tipo
         */
        public List<Tipos> pesquisaTodosTipos()
        {
            return (from tipos in entityModel.Tipos
                    select tipos).ToList();
        }

        public Tipos pesquisaValorTipo(string valor)
        {
            return (from tipos in entityModel.Tipos
                    where (tipos.desc_tipo.Equals(valor))
                    select tipos).SingleOrDefault();
        }

        /*
         * Pesquisa de usuário
         */
        public List<Usuarios> pesquisaTodosUsers()
        {
            return (from usuario in entityModel.Usuarios
                    select usuario).ToList();
        }

        public Usuarios pesquisaUsuarioReg(int valor)
        {
            return (from usuario in entityModel.Usuarios
                    where (usuario.registro == (valor))
                    select usuario).SingleOrDefault();
        }

    }
}