﻿using RenderApi.Control;
using RenderApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RenderApi.View
{
    public partial class Selecao : System.Web.UI.Page
    {
        //variavel global para armazenar a variavel de sessão do usuário vindo da pagina Index.aspx
        static int registro;
        //objetos do tipo de dados da base
        static Usuarios user;
        Questionarios quest;
        Renderizar render = new Renderizar();
        //Listas dos tipos referentes a base de dados
        List<Renderizar> LRend;        
        List<Questionarios> listaQuest = new List<Questionarios>();
        //objeto de ligação com a camada de control
        Controle controle = new Controle();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //verifica se o login foi feito por usuário + senha na página Index.aspx
                    if (Session["user"].ToString() == "")
                    {
                        //Variaveis de sessão recebidas no postback url
                        string regist = Convert.ToString(Request.Form["hddRegFunc"]);
                        string nome = Convert.ToString(Request.Form["hddNomeFunc"]);
                        string perfil = Convert.ToString(Request.Form["hddEnumPerfil"]);

                        //verifica a pré existencia do usuário na base de dados
                        if (controle.pesquisaUsuarioReg(Convert.ToInt32(regist)) == null)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Usuário CAC não cadastrado na aplicação')", true);
                            //Adiciona o usuário do acesso via postback url a base de dados 
                            controle.salvarUsuario(user);
                            user.nome = nome;
                            user.registro = Convert.ToInt32(registro);
                            user.perfil = Convert.ToInt32(perfil);
                            //salva a adição de usuário
                            controle.atualizarDados();
                        }
                    }
                    //busca na base de dados o usuário no caso de login por usuário + senha
                    else
                    {
                        //busca na base de dados por meio de variavel de sessão
                        registro = Convert.ToInt32(Session["user"]);
                        user = controle.pesquisaUsuarioReg(registro);
                    }
                    //verifica se na interface login o usuário utilizou o login anônimo e exibe na tela informação referente ao login
                    if (registro == 0)
                    {
                        lblUser.Text = "Acesso Público";
                    }
                    else
                    {
                        lblUser.Text = user.registro.ToString() + " - " + user.nome;
                    }
                    //atribui a lista de objetos do tipo renderização aqueles que tenha como usuário o registro do login
                    LRend = controle.pesquisaRenderizarReg(user.id);
                    foreach (Renderizar value in LRend)
                    {
                        //variavel para impedir a duplicidade de inclusão na lista de questionários
                        bool publico = false;
                        //verifica se o usuário que acessou é público
                        if (registro != 0)
                        {
                            //busca para cada elemento a existencia deste formulário no formato público
                            render = controle.pesquisaRenderizarIdUser(value.id_questionario, 0);
                            if (render != null)
                            {
                                //localizando exclui esse render do perfil do usuário mantendo somente o acesso público
                                controle.excluirRenderizar(value);
                                controle.atualizarDados();
                                //localizado o id do questionário como público
                                publico = true;
                            }
                        }
                        //verifica se o objeto faz parte da lista e se não é público
                        if (value != null && !publico)
                        {
                            //busca para o objeto o questionário referente ao render
                            quest = controle.pesquisaQuestionarioId(value.id_questionario);
                            if (quest != null)
                            {
                                //adiciona ao objeto lista de questionários este elemento, desde que não seja nulo
                                listaQuest.Add(quest);

                            }
                        }
                    }
                    //se o registro for diferente do perfil de acesso público apresenta ainda os questionários públicos na lista deste usuário
                    if (registro != 0)
                    {
                        //atribui a lista de objetos do tipo renderização aqueles que tenha como usuário o registro do usuário público
                        LRend = controle.pesquisaRenderizarReg(0);
                        foreach (Renderizar value in LRend)
                        {
                            //busca para o objeto o questionário referente ao render
                            quest = controle.pesquisaQuestionarioId(value.id_questionario);
                            if (quest != null)
                            {
                                //adiciona ao objeto lista de questionários este elemento, desde que não seja nulo
                                listaQuest.Add(quest);
                            }
                        }

                    }
                    //se houverem elementos na lista passa esta como parametro para a função que carrega a radio button list
                    if (listaQuest.Count() > 0)
                    {
                        carregaRadioList(listaQuest);
                    }
                    //não havendo elementos na lista informa ao usuário
                    else
                    {
                        lblMensagem.Visible = true;
                        lblMensagem.Text = "Não existem questionários públicos ou para este usuário";
                    }
                }
                //instruções circundadas com try, catch para evitar a exibição de possíveis erros ao usuário
                catch { }
            }

        }
        //função para carregar a lista de questionários na radio button list
        public void carregaRadioList(List<Questionarios> lista)
        {
            try
            {
                //defini a lista de radio buttons
                rblQuestionario.DataSource = lista;
                rblQuestionario.DataTextField = "nome";
                rblQuestionario.DataValueField = "id";
                rblQuestionario.DataBind();

                //para cada elemento da lista atribui o titulo do questíonário como texto
                for (int i = 0; i < rblQuestionario.Items.Count; i++)
                {
                    string opção = lista[i].nome;
                    rblQuestionario.Items[i].Text = opção;
                }
                //modifica os controles da view
                lblMensagem.Visible = true;
                lblMensagem.Text = "Selecione o questionário a ser realizado";
                rblQuestionario.Visible = true;
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros ao usuário
            catch { }
        }
        //função que executa ação baseada na mudança da seleção da radio button list
        protected void rblQuestionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            //atribui a variavel de sessão o valor do item escolhido e chama a página de renderização
            Session["Id_quest"] = Convert.ToInt32(rblQuestionario.SelectedValue);
            Response.Redirect("Renderizado.aspx");
        }
    }
}