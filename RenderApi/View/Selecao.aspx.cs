using RenderApi.Control;
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
        static int registro;
        Usuarios user;
        List<Renderizar> LRend;
        Questionarios quest;
        List<Questionarios> listaQuest = new List<Questionarios>();

        Controle controle = new Controle();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                registro = Convert.ToInt32(Session["user"]);
                if (registro == 0)
                {
                    lblUser.Text = "Acesso Público";
                    user = controle.pesquisaUsuarioReg(registro);
                }
                else
                {
                    user = controle.pesquisaUsuarioReg(registro);
                    lblUser.Text = user.registro.ToString() + " - " + user.nome;
                }
                
                LRend = controle.pesquisaRenderizarReg(user.id);
                foreach (Renderizar value in LRend)
                {
                    quest = controle.pesquisaQuestionarioId(value.id_questionario);
                    if (quest != null)
                    {
                        listaQuest.Add(quest);
                    }
                }
                if (listaQuest.Count() > 0)
                {
                    carregaRadioList(listaQuest);
                }
                else
                {
                    lblMensagem.Visible = true;
                    lblMensagem.Text = "Não existem questionários para este usuário";
                }

            }

        }

        public void carregaRadioList(List<Questionarios> lista)
        {
            rblQuestionario.DataSource = lista;
            rblQuestionario.DataTextField = "nome";
            rblQuestionario.DataValueField = "id";
            rblQuestionario.DataBind();

            for (int i = 0; i < rblQuestionario.Items.Count; i++)
            {
                string opção = quest.nome;
                rblQuestionario.Items[i].Text = opção;
            }
            lblMensagem.Visible = true;
            lblMensagem.Text = "Selecione o questionário a ser realizado";
            rblQuestionario.Visible = true;

        }

        protected void rblQuestionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Id_quest"] = rblQuestionario.SelectedValue;
            Response.Redirect("Renderizado.aspx");
        }
    }
}