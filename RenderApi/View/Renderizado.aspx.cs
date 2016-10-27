using RenderApi.Control;
using RenderApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RenderApi.View
{
    public partial class Renderizado : System.Web.UI.Page
    {
        static int id;
        static int reg;
        static int count = 0, countMax;
        static bool selecao = false;

        static List<Perguntas> perg;
        static List<Respostas> resp;
        static Usuarios user;
        static Renderizar render;
        static Prenchimentos valores = new Prenchimentos();

        static RadioButtonList radioResposta;
        static CheckBox chkResposta;        
        static Panel panelPergunta;
        static Panel panelResposta;

        Controle controle = new Controle();

        protected void Page_Load(object sender, EventArgs e)
        {            

            if (!IsPostBack)
            {
                id = Convert.ToInt32(Session["Id_quest"]);
                reg = Convert.ToInt32(Session["user"]);
                user = controle.pesquisaUsuarioReg(reg);
                Questionarios quest = controle.pesquisaQuestionarioId(id);
                render = controle.pesquisaRenderizarIdUser(id, user.id);
                lblID.Text = id.ToString();
                lblTitulo.Text = quest.nome;

                if (reg != 0)
                {
                    user = controle.pesquisaUsuarioReg(reg);
                    lblUser.Text = user.registro.ToString() + " - " + user.nome;
                    perg = controle.pesquisaPerguntaQuestionario(id);
                    if (controle.pesquisaPreenchimento_render_user(render.id, lblUser.Text).Count == 0)
                    {
                        controle.construtor_formulario(id, render.id, lblUser.Text);
                    }
                    countMax = perg.Count;
                    pnlButton.Visible = true;
                    posicionaPergunta();
                }

                else
                {
                    lblUser.Text = "Insira seu nome:";
                    txtUser.Visible = true;
                    btnIniciar.Visible = true;
                }

            }
            else
            {
                posicionaPergunta();
            }            

        }

        protected void btnIniciar_Click(object sender, EventArgs e)
        {
            if (txtUser.Text != "")
            {
                btnIniciar.Visible = false;
                lblAviso.Visible = false;
                txtUser.Visible = false;
                lblUser.Text = txtUser.Text;
                perg = controle.pesquisaPerguntaQuestionario(id);
                countMax = perg.Count;
                if (controle.pesquisaPreenchimento_render_user(render.id, lblUser.Text).Count == 0)
                {
                    controle.construtor_formulario(id, render.id, lblUser.Text);
                }
                posicionaPergunta();
                pnlButton.Visible = true;
            }
            else
            {
                lblAviso.Visible = true;
                lblAviso.Text = "* O campo nome deve ser preenchido !!!";                          
            }
        }

        public void posicionaPergunta()
        {
            selecao = false;
            try
            {                                 
                pnlPrincipal.Controls.Add(new LiteralControl("<p></p>"));
                panelPergunta = new Panel
                {
                    ID = "pergunta_" + perg[count].id.ToString()
                };
                Label labelPergunta = new Label();
                labelPergunta.Text = perg[count].numero.ToString() + " - " + perg[count].titulo;
                pnlPrincipal.Controls.Add(panelPergunta);
                panelPergunta.Controls.Add(labelPergunta);
                panelPergunta.Controls.Add(new LiteralControl("<br>"));

                if (perg[count].tipo == 1)
                {
                    carregarRadioButtonList();                    
                }

                else if (perg[count].tipo == 2)
                {
                    carregarCheckList();
                }

                else if (perg[count].tipo == 3)
                {
                    carregarTextBox();
                }

                else if (perg[count].tipo == 4)
                {
                    carregarDateTextBox();
                }

                else if (perg[count].tipo == 5)
                {
                    carregarNumberTextBox();
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('A pergunta " + perg[count].id + " possuí um formato de resposta não definido no escopo, alterar linha 135 do arquivo Renderizado.aspx.cs');", true);
                }
                
            }

            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Houve algum problema para renderizar a pergunta );", true);
            }

        }               

        private void carregarRadioButtonList()
        {
            resp = controle.pesquisaRespostaQuestão(perg[count].id);
            panelPergunta.Controls.Remove(panelResposta);
            panelResposta = new Panel();
            panelPergunta.Controls.Add(panelResposta);
            radioResposta = new RadioButtonList
            {
                ID = perg[count].id.ToString(),
                EnableViewState = true,
                AutoPostBack = true
            };
            radioResposta.DataSource = resp;
            radioResposta.DataTextField = "resposta";
            radioResposta.DataValueField = "id";
            radioResposta.DataBind();
            radioResposta.Attributes.Add("OnSelectIndexChanged", "radioResposta_SelectedIndexChanged");
            radioResposta.SelectedIndexChanged += new EventHandler(radioResposta_SelectedIndexChanged);

            for (int i = 0; i < radioResposta.Items.Count; i++)
            {
                string opção = resp[i].resposta;
                radioResposta.Items[i].Text = opção;                
            }
            
            for (int i = 0; i < radioResposta.Items.Count; i++)
            {
                valores = controle.pesquisaPreenchimentoValor(radioResposta.Items[i].Text, render.id);
                if (valores.resposta == 1)
                {
                    radioResposta.Items[i].Selected = true;
                    selecao = true;
                }
            }

            panelResposta.Controls.Add(radioResposta);
                
        }

        private void carregarCheckList()
        {
            resp = controle.pesquisaRespostaQuestão(perg[count].id);
            panelPergunta.Controls.Remove(panelResposta);
            panelResposta = new Panel();
            panelPergunta.Controls.Add(panelResposta);
            chkResposta = new CheckBox();
            for (int i = 0; i < resp.Count; i++)
            {
                chkResposta = new CheckBox
                {
                    ID = perg[count].id.ToString() + i,
                    AutoPostBack = true
                };
                chkResposta.Attributes.Add("OnSelectIndexChanged", "radioResposta_SelectedIndexChanged");
                chkResposta.CheckedChanged += new EventHandler(chkResposta_CheckedChanged);

                chkResposta.Text = resp[i].resposta;
                panelResposta.Controls.Add(chkResposta);
                
                valores = controle.pesquisaPreenchimentoValor(resp[i].resposta, render.id);
                if (valores.resposta == 1)
                {
                    chkResposta.Checked = true;
                    selecao = true;
                }
                                                               
            }                                   
            
        }        

        private void carregarTextBox()
        {
            Panel panelResposta = new Panel();
            panelPergunta.Controls.Add(panelResposta);
            TextBox txtResposta = new TextBox
            {
                ID = perg[count].id.ToString(),
                AutoPostBack = true,
                MaxLength = 150,
                Width = 450
            };
            txtResposta.Attributes.Add("OnTextChanged", "txtResposta_TextChanged");
            txtResposta.TextChanged += new EventHandler(txtResposta_TextChanged);
                       
            valores = controle.pesquisaPreenchimento_perg_render(perg[count].id, render.id);
            if (valores.resposta == 1)
            {
                txtResposta.Text = valores.valor_resposta;
                selecao = true;
            }
            panelResposta.Controls.Add(txtResposta);
                      
        }
        
        private void carregarDateTextBox()
        {
            Panel panelResposta = new Panel();
            panelPergunta.Controls.Add(panelResposta);
            TextBox dataResposta = new TextBox
            {
                ID = perg[count].id.ToString(),
                Width = 150,
                TextMode = TextBoxMode.Date,
                AutoPostBack = true
            };
            dataResposta.Attributes.Add("OnTextChanged", "txtResposta_TextChanged");
            dataResposta.TextChanged += new EventHandler(txtResposta_TextChanged);

            valores = controle.pesquisaPreenchimento_perg_render(perg[count].id, render.id);
            if (valores.resposta == 1)
            {
                dataResposta.Text = valores.valor_resposta;
                selecao = true;
            }
            panelResposta.Controls.Add(dataResposta);
            
            
        }

        private void carregarNumberTextBox()
        {
            Panel panelResposta = new Panel();
            panelPergunta.Controls.Add(panelResposta);
            TextBox numResposta = new TextBox
            {
                ID = perg[count].id.ToString(),
                Width = 65,
                MaxLength = 6,
                TextMode = TextBoxMode.Number,
                AutoPostBack = true
            };
            numResposta.Attributes.Add("OnTextChanged", "txtResposta_TextChanged");
            numResposta.TextChanged += new EventHandler(txtResposta_TextChanged);

            valores = controle.pesquisaPreenchimento_perg_render(perg[count].id, render.id);
            if (valores.resposta == 1)
            {
                numResposta.Text = valores.valor_resposta;
                selecao = true;
            }
            panelResposta.Controls.Add(numResposta);
            
        }

        private void chkResposta_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox caixa = sender as CheckBox;

            valores = controle.pesquisaPreenchimentoValor(caixa.Text, render.id);
            if (caixa.Checked)
            {
                valores.resposta = 1;
                selecao = true;
                controle.atualizarDados();
            }
            else
            {
                valores.resposta = 0;
                controle.atualizarDados();
            }
            
        }

        protected void radioResposta_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecao = true;
            string itemSelecionado = radioResposta.SelectedItem.Text;

            for (int i = 0; i < radioResposta.Items.Count; i++)
            {
                if (itemSelecionado.Equals(radioResposta.Items[i].Text))
                {
                    valores = controle.pesquisaPreenchimentoValor(radioResposta.SelectedItem.Text, render.id);
                    valores.resposta = 1;
                    controle.atualizarDados();
                }
                else
                {
                    valores = controle.pesquisaPreenchimentoValor(radioResposta.Items[i].Text, render.id);
                    valores.resposta = 0;                
                }
            }
            controle.atualizarDados();
                                   
        }
        
        private void txtResposta_TextChanged(object sender, EventArgs e)
        {            
            TextBox text = sender as TextBox;
            valores = controle.pesquisaPreenchimento_perg_render(perg[count].id, render.id);
            valores.resposta = 1;
            valores.valor_resposta = text.Text;
            if (valores.valor_resposta.Equals(""))
            {
                selecao = false;
            }
            else
            {
                selecao = true;
            }
            controle.atualizarDados();          
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Label lblAlerta = new Label()
            {
                ForeColor = System.Drawing.Color.Red,
                Text = "Resposta obrigatória"
            };
            panelPergunta.Controls.Add(lblAlerta);
            if (perg[count].obrigatorio && selecao)
            {
                try
                {

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('As suas respostas foram salvas, obrigado pela colaboração');", true);
                    Response.Redirect("Index.aspx");
                    count = 0;
                }

                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Por algum motivo não foi possivel salvar as suas respostas');", true);
                }
            }
            else if (!perg[count].obrigatorio)
            {
                selecao = false;
                lblAlerta.Visible = false;
                try
                {

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('As suas respostas foram salvas, obrigado pela colaboração');", true);
                    Response.Redirect("Index.aspx");
                    count = 0;
                }

                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Por algum motivo não foi possivel salvar as suas respostas');", true);
                }
            }
            else
            {
                lblAlerta.Visible = true;
            }

        }

        protected void btnRight_Click(object sender, ImageClickEventArgs e)
        {
            Label lblAlerta = new Label()
            {
                ForeColor = System.Drawing.Color.Red,
                Text = "Resposta obrigatória"
            };
            panelPergunta.Controls.Add(lblAlerta);
            txtUser.Visible = false;
            lblAviso.Visible = false;
            if (perg[count].obrigatorio && selecao)
            {
                selecao = false;
                lblAlerta.Visible = false;
                pnlPrincipal.Controls.Remove(panelPergunta);

                if (count >= countMax - 1)
                {
                    count = countMax - 1;
                    posicionaPergunta();
                }

                else
                {
                    count++;
                    posicionaPergunta();
                }
            }
            else if (!perg[count].obrigatorio)
            {
                selecao = false;
                lblAlerta.Visible = false;
                pnlPrincipal.Controls.Remove(panelPergunta);

                if (count >= countMax - 1)
                {
                    count = countMax - 1;
                    posicionaPergunta();
                }

                else
                {
                    count++;
                    posicionaPergunta();
                }
            }
            else
            {
                lblAlerta.Visible = true;
            }
        }


        protected void btnLeft_Click(object sender, ImageClickEventArgs e)
        {
            Label lblAlerta = new Label()
            {
                ForeColor = System.Drawing.Color.Red,
                Text = "Resposta obrigatória"
            };
            panelPergunta.Controls.Add(lblAlerta);
            txtUser.Visible = false;
            lblAviso.Visible = false;
            if (perg[count].obrigatorio && selecao)
            {
                selecao = false;
                lblAlerta.Visible = false;
                pnlPrincipal.Controls.Remove(panelPergunta);

                if (count == 0)
                {

                    posicionaPergunta();
                }

                else
                {
                    count--;
                    posicionaPergunta();

                }
            }
            else if (!perg[count].obrigatorio)
            {
                pnlPrincipal.Controls.Remove(panelPergunta);
                lblAlerta.Visible = false;
                if (count == 0)
                {

                    posicionaPergunta();
                }

                else
                {
                    count--;
                    posicionaPergunta();

                }
            }
            else
            {
                lblAlerta.Visible = true;
            }
        }       
    }
}