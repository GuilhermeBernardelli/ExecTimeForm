using CreateApi.Control;
using CreateApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateApi.View
{
    public partial class Usuario : System.Web.UI.Page
    {
        Controle controle = new Controle();
        List<Questionarios> LQuest;
        List<Usuarios> LUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblTipo.Visible = false;
            if (!IsPostBack)
            {
                /*
                questionario = Convert.ToInt32(Session["questionarioId"]);
                quest = controle.pesquisaQuestionarioId(questionario);
                lblQuest.Text = quest.nome;
                */
            }
        }

        protected void btnQuestUser_Click(object sender, EventArgs e)
        {
            pnlPesquisaQuest.Visible = true;
            pnlMetodo.Visible = false;
        }

        protected void btnUserQuest_Click(object sender, EventArgs e)
        {
            pnlPesquisaUser.Visible = true;
            pnlMetodo.Visible = false;
        }  

        protected void btnPesqQuest_Click(object sender, EventArgs e)
        {
            if (txtQuest.Text.Equals(""))
            {
                lblAvisoQuest.Visible = true;
                lblAvisoQuest.Text = "* O campo pesquisa não pode ser vazio";
            }
            else
            {
                lblAvisoQuest.Visible = false;
                LQuest = controle.pesquisaGeralQuestionario(txtQuest.Text);
                if (LQuest.Count == 0)
                {
                    lblAvisoQuest.Visible = true;
                    lblAvisoQuest.Text = "A pesquisa não retornou resultados, altere o parametro e tente novamente";
                }
                else
                {
                    rblQuest.Visible = true;
                    rblQuest.DataSource = LQuest;
                    rblQuest.DataTextField = "nome";
                    rblQuest.DataValueField = "id";
                    rblQuest.DataBind();
                    for (int i = 0; i < LQuest.Count; i++)
                    {
                        string opção = LQuest[i].nome;
                        rblQuest.Items[i].Text = opção;
                    }
                }
            }
        }

        protected void btnPesqUser_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Equals(""))
            {
                lblAvisoUser.Visible = true;
                lblAvisoUser.Text = "* O campo pesquisa não pode ser vazio";
            }
            else
            {
                lblAvisoUser.Visible = false;
                LUser = controle.pesquisaGeralUsuários(txtUser.Text);
                if (LUser.Count == 0)
                {
                    lblAvisoUser.Visible = true;
                    lblAvisoUser.Text = "A pesquisa não retornou resultados, altere o parametro e tente novamente";
                }
                else
                {
                    rblUser.Visible = true;
                    rblUser.DataSource = LUser;
                    rblUser.DataTextField = "nome";
                    rblUser.DataValueField = "id";
                    rblUser.DataBind();
                    for (int i = 0; i < LUser.Count; i++)
                    {
                        string opção = LUser[i].nome;
                        rblUser.Items[i].Text = opção;
                    }
                }
            }
        }
    }
}