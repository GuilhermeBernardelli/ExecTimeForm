using System;
using System.Collections.Generic;
using System.Linq;

namespace CreateApi.Model
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

        public void rodaProcedure(int id_quest, int id_render, string user)
        {
            entityModel.realiza_preenchimento(id_quest, id_render, user);
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

        public void salvarNovoRender(Renderizar render)
        {
            entityModel.Renderizar.Add(render);
        }
                
        public void excluirDadosRender(Renderizar render)
        {
            entityModel.Renderizar.Remove(render);
        }

        public void salvarNovoUsuario(Usuarios usuario)
        {
            entityModel.Usuarios.Add(usuario);
        }

        public void excluirUsuario(Usuarios usuario)
        {
            entityModel.Usuarios.Remove(usuario);
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
                    where (questionario.nome.Equals(valor))
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
        public List<Usuarios> pesquisaGeralUsers(string valor)
        {         
            
                return (from usuario in entityModel.Usuarios
                        where (usuario.nome.Contains(valor))
                        select usuario).ToList();
            
        }

        public List<Usuarios> pesquisaGeralUserReg(int valor)
        {
            return (from usuario in entityModel.Usuarios
                    where (usuario.registro == (valor))
                    select usuario).ToList();
        }

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

        public Usuarios pesquisaUsuarioNome(string valor)
        {
            return (from usuario in entityModel.Usuarios
                    where (usuario.nome.Equals(valor))
                    select usuario).SingleOrDefault();
        }

        /*
         * Pesquisa de renderização
         */
        public List<Renderizar> pesquisaRenderizarReg(int valor)
        {
            DateTime time = DateTime.Now;
            return (from renderiza in entityModel.Renderizar
                    where (renderiza.id_usuario == (valor)
                    && renderiza.data_validade >= time.Date)
                    select renderiza).ToList();
        }

        public Perguntas pesquisaGeralPerguntaPerg_QuestNum(int questId, int pergNum)
        {
            return (from pergunta in entityModel.Perguntas
                    where pergunta.id_questionario == questId
                    && pergunta.numero == pergNum
                    select pergunta).Single();
        }

        public Renderizar pesquisaRenderizarId_User(int valor, int user)
        {
            return (from renderiza in entityModel.Renderizar
                    where (renderiza.id_questionario == valor)
                    && (renderiza.id_usuario == user)
                    select renderiza).SingleOrDefault();
        }


        /*
         * Pesquisas de Preenchimento
         */

        public Prenchimentos pesquisaPreenchimentoValorResp(string pesquisa, int render)
        {
            return (from preenchimento in entityModel.Prenchimentos
                    where (preenchimento.valor_resposta.Equals(pesquisa))
                    && (preenchimento.id_renderizar == render)
                    select preenchimento).SingleOrDefault();
        }

        public Prenchimentos pesquisaPreenchimentoIdPerg_Nome(int pesquisa, string user)
        {
            return (from preenchimento in entityModel.Prenchimentos
                    where (preenchimento.id_pergunta == pesquisa)
                    && (preenchimento.usuario.Equals(user))
                    select preenchimento).SingleOrDefault();
        }

        public Prenchimentos pesquisaPrenchimentoId(int pesquisa)
        {
            return (from preenchimento in entityModel.Prenchimentos
                    where (preenchimento.id == pesquisa)
                    select preenchimento).SingleOrDefault();
        }

        public List<Prenchimentos> pesquisaPreenchimentoRenderUser(int render, string user)
        {
            return (from preenchimento in entityModel.Prenchimentos
                    where (preenchimento.id_renderizar == render)
                    && (preenchimento.usuario.Equals(user))
                    select preenchimento).ToList();
        }

        public Prenchimentos pesquisaPreenchimentoNome(string pesquisa, string user, int id, int idPerg)
        {
            return (from preenchimento in entityModel.Prenchimentos
                    where (preenchimento.valor_resposta.Equals(pesquisa))
                    && (preenchimento.usuario.Equals(user))
                    && (preenchimento.id_renderizar == id)
                    && (preenchimento.id_pergunta == idPerg)
                    select preenchimento).SingleOrDefault();
        }
    }
}