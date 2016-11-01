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
        Renderizar render;
        List<Questionarios> LQuest;
        List<Usuarios> LUser;
        static string tipo = null;
        bool existe = false;
        static string data;
        static List<DateTime> dataLista = new List<DateTime>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*
                questionario = Convert.ToInt32(Session["questionarioId"]);
                quest = controle.pesquisaQuestionarioId(questionario);
                lblQuest.Text = quest.nome;
                */
            }
        }

        public void limpaFunction()
        {
            chkSelecionados.Items.Clear();           
            lblAvisoQuest.Visible = false;
            lblAvisoUser.Visible = false;
            rblQuest.Visible = false;
            rblUser.Visible = false;
            txtQuest.Text = "";
            txtUser.Text = "";
        }

        protected void btnUserQuest_Click(object sender, EventArgs e)
        {
            pnlPesquisaQuest.Visible = true;
            pnlMetodo.Visible = false;
        }

        protected void btnQuestUser_Click(object sender, EventArgs e)
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
                LUser = controle.pesquisaGeralUsuarios(txtUser.Text);
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

        protected void rblQuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tipo == null)
            {
                btnSalvar.Enabled = false;
                lblTitulo.Text = "Inclusão de usuários ao questionário";
                lblTipo.Visible = true;
                lblTipo.Text = rblQuest.SelectedItem.Text + ":";
                pnlPesquisaQuest.Visible = false;
                pnlPrincipal.Visible = true;
                btnAlterar.Text = "Outro Question.";
                tipo = "quest";                
            }
            else
            {
                btnSalvar.Enabled = true;
                for (int i = 0; i < chkSelecionados.Items.Count; i++)
                {
                    if (chkSelecionados.Items[i].Equals(rblQuest.SelectedItem))
                    {
                        existe = true;
                    }
                }
                if (!existe)
                {
                    btnVoltaQuest.Text = "Desfazer";
                    txtQuest.Text = "";
                    chkSelecionados.Items.Add(rblQuest.SelectedItem);
                    rblQuest.Items.Clear();
                    chkSelecionados.Visible = true;
                    pnlPrincipal.Enabled = false;
                    pnlPesquisaQuest.Visible = false;
                    pnlDataValidade.Visible = true;
                }
                else
                {
                    existe = false;
                    lblAvisoQuest.Visible = true;
                    lblAvisoQuest.Text = "Questionário já incluso na lista";
                }
            }
        }

        protected void rblUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tipo == null)
            {
                btnSalvar.Enabled = false;
                lblTitulo.Text = "Inclusão de questionários ao usuário";
                lblTipo.Visible = true;
                lblTipo.Text = rblUser.SelectedItem.Text + ":";
                pnlPesquisaUser.Visible = false;
                pnlPrincipal.Visible = true;
                btnAlterar.Text = "Outro Usuário";
                tipo = "user";
            }
            else
            {
                btnSalvar.Enabled = true;
                for (int i = 0; i < chkSelecionados.Items.Count; i++)
                {
                    if (chkSelecionados.Items[i].Equals(rblUser.SelectedItem))
                    {
                        existe = true;
                    }
                }
                if (!existe)
                {
                    txtUser.Text = "";     
                    chkSelecionados.Items.Add(rblUser.SelectedItem);
                    rblUser.Items.Clear();
                    chkSelecionados.Visible = true;
                    btnVoltaUser.Text = "Desfazer";
                }
                else
                {
                    existe = false;
                    lblAvisoUser.Visible = true;
                    lblAvisoUser.Text = "Usuário já incluso na lista";
                }
            }
        }

        protected void btnVoltaUser_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnVoltaQuest_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnInclude_Click(object sender, EventArgs e)
        {
            btnInclude.Enabled = false;
            limpaFunction();
            if (tipo.Equals("user"))
            {
                pnlPesquisaQuest.Visible = true;
                txtQuest.Text = "";
            }
            else
            {
                pnlDataValidade.Visible = true;
                //pnlPesquisaUser.Visible = true;
                //txtUser.Text = "";
            }
            btnVoltaQuest.Text = "Voltar";
            btnVoltaUser.Text = "Voltar";
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            btnInclude.Enabled = true;
            limpaFunction();
            if (tipo.Equals("quest"))
            {
                pnlPrincipal.Visible = false;
                pnlPesquisaQuest.Visible = true;
                pnlPesquisaUser.Visible = false;
                txtQuest.Text = "";
                tipo = null;
            }
            else
            {
                pnlPrincipal.Visible = false;
                pnlPesquisaUser.Visible = true;
                pnlPesquisaQuest.Visible = false;
                txtUser.Text = "";
                tipo = null;
            }
        }

        protected void btnCancela_Click(object sender, EventArgs e)
        {
            tipo = null;
            Response.Redirect("Usuario.aspx");            
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (tipo.Equals("quest"))
            {
                LUser = new List<Usuarios>();
                foreach (ListItem value in chkSelecionados.Items)
                {
                    LUser.Add(controle.pesquisaUsuarioNomeCompleto(value.Text));
                }
                foreach (Usuarios value in LUser)
                {
                    render = new Renderizar();
                    controle.salvarRender(render);
                    render.id_questionario = Convert.ToInt32(rblQuest.SelectedValue);
                    render.data_renderizado = DateTime.Now;
                    render.id_usuario = value.id;
                    render.data_validade = Convert.ToDateTime(data);
                    controle.atualizarDados();
                }
            }
            else//tipo.Equals("user")
            {
                LQuest = new List<Questionarios>();
                int i = 0;
                foreach (ListItem value in chkSelecionados.Items)
                {
                    LQuest.Add(controle.pesquisaQuestionarioNome(value.Text));
                }
                foreach (Questionarios value in LQuest)
                {
                    render = new Renderizar();
                    controle.salvarRender(render);
                    render.id_usuario = Convert.ToInt32(rblUser.SelectedValue);
                    render.data_renderizado = DateTime.Now;
                    render.id_questionario = value.id;                    
                    render.data_validade = dataLista[i++];
                    controle.atualizarDados();
                }
            }
            Response.Redirect("Usuario.aspx");
        }

        protected void btnData_Click(object sender, EventArgs e)
        {
            if (tipo.Equals("user"))
            {                
                dataLista.Add(Convert.ToDateTime(txtData.Text));
                pnlPrincipal.Enabled = true;
                pnlPesquisaQuest.Visible = true;
                pnlDataValidade.Visible = false;
                txtData.Text = "";
            }
            else
            {
                data = txtData.Text;
                pnlDataValidade.Visible = false;
                pnlPesquisaUser.Visible = true;
                txtUser.Text = "";
            }
        }
    }
}