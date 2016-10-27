using FormApi.Control;
using FormApi.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FormApi.View
{
    public partial class Questionario : System.Web.UI.Page
    {
        Controle controle = new Controle();
        Questionarios quest;
        Perguntas perg;
        Respostas resp;
        static int pergCount = 1;
        static int perguntaId = 0;
        static int questionarioId = 0;
        static string respostaTitulo;
        static int ordemResp, qntOpc;

        protected void Page_Load(object sender, EventArgs e)
        {   
            
            if (!IsPostBack)
            {
                List<Tipos> listaTipos = controle.pesquisaTipos();
                foreach (Tipos value in listaTipos)
                {
                    ddlReposta.Items.Add(value.desc_tipo);
                }
            }           
        }

        public void limpaFunction()
        {
            btnNovo.Enabled = true;
            txtTitulo.Enabled = true;
            txtTitulo.Text = "";
        }

        public void carregaRadioList(List<Respostas> listaRespostas)
        {            
            rblOpções.DataSource = listaRespostas;
            rblOpções.DataTextField = "resposta";
            rblOpções.DataValueField = "id";
            rblOpções.DataBind();

            for(int i=0; i < rblOpções.Items.Count; i++)
            {
                string opção = listaRespostas[i].resposta;
                rblOpções.Items[i].Text = opção;
            }
            pnlOpções.Visible = true;
            rblOpções.Visible = true;           
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            if (txtTitulo.Text.Equals(""))
            {
                lblAlerta.Visible = true;
                lblAlerta.Text = "* O campo Titulo do Questionário não pode ser vazio.";
            }
            else
            {
                try
                {
                    quest = new Questionarios();
                    quest.nome = txtTitulo.Text.Trim();

                    txtTitulo.Enabled = false;
                    lblAlerta.Visible = false;                    
                    btnNovo.Enabled = false;                                       
                    btnEditar.Enabled = true;
                    btnAdicionar.Enabled = true;                    
                   
                    controle.salvarQuestionario(quest);
                    controle.atualizarDados();
                    questionarioId = quest.id;
                   
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertaAtualizadoOK", "alert('Inclusão realizada com sucesso.');", true);
                }
                catch (DbUpdateException ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertaSelecionar", "alert('Erro ao atualizar dados, verifique se os dados estão corretos.')", true);
                    limpaFunction();
                }
            }            
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {                        
            txtTitulo.Enabled = true;
            btnEditar.Enabled = false;
            btnAdicionar.Enabled = false;
            btnSalvar.Visible = true;
            btnNovo.Visible = false;
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            quest = new Questionarios();
            quest = controle.pesquisaQuestionarioId(questionarioId);
            quest.nome = txtTitulo.Text.Trim();
            controle.atualizarDados();
            btnNovo.Visible = true;
            btnSalvar.Visible = false;
            btnEditar.Enabled = true;
            txtTitulo.Enabled = false;
            btnAdicionar.Enabled = true;
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            pnlAlerta.Visible = false;
            pnlPerguntas.Visible = true;
            lblN.Text = pergCount.ToString();
            btnAdicionar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = true;
            pnlPerguntas.Enabled = true;
            chkObrigatorio.Checked = false;
            txtPergunta.Text = "";
            pnlSair.Visible = false;
        }      

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = true;
            btnEditar.Enabled = true;            
            pnlPerguntas.Visible = false;                  

            List<Perguntas> Lperg = controle.pesquisaPerguntaQuestionario(questionarioId);
            foreach (Perguntas value in Lperg)
            {
                List<Respostas> Lresp = controle.pesquisaRespostaQuestão(value.id);
                foreach (Respostas var in Lresp)
                {
                    controle.excluirResposta(var);
                }
                controle.excluirPergunta(value);
            }
            quest = controle.pesquisaQuestionarioId(questionarioId);
            controle.excluirQuestionario(quest);
            controle.atualizarDados();
            btnEditar.Enabled = false;
            txtTitulo.Text = "";
            txtTitulo.Enabled = true;
            btnExcluir.Enabled = false;
            pergCount = 0;
        }

        protected void ddlReposta_SelectedIndexChanged(object sender, EventArgs e)
        {
            ordemResp = 1;
            qntOpc = 0;            
            lblAlerta.Visible = false;
            if (txtPergunta.Text.Equals(""))
            {
                lblAlertaResp.Text = "* O campo pergunta deve ser preenchido";
                pnlAlerta.Visible = true;
                ddlReposta.SelectedIndex = 0;
            }

            else if (ddlReposta.SelectedValue.Equals("Selecione"))
            {
                pnlAlerta.Visible = true;
                lblAlertaResp.Text = "* Seleção de tipo de resposta obrigatória";
            }

            else if (txtPergunta.Text.Length > 150)
            {
                pnlAlerta.Visible = true;
                lblAlertaResp.Text = "* O campo pergunta está limitado a 150 caracteres, reformule a pergunta";
                ddlReposta.SelectedIndex = 0;
            }

            else
            {
                int flag = 0;                
                List<Perguntas> validação = controle.pesquisaGeralPergunta(txtPergunta.Text.Trim());
                foreach (Perguntas value in validação)
                {
                    if(questionarioId == value.id_questionario)
                    {
                        if (txtPergunta.Text.Trim().ToUpper().Equals(value.titulo.ToUpper()))
                            flag++;
                    }
                }

                if (flag == 1)
                {                   
                    pnlAlerta.Visible = true;
                    lblAlertaResp.Text = "* Não deve haver duplicidade, já existe esta pergunta neste questionário";
                    txtPergunta.Text = "";
                    ddlReposta.SelectedIndex = 0;
                }

                else
                {                    
                    perg = new Perguntas();
                    perg.id_questionario = questionarioId;
                    perg.titulo = txtPergunta.Text.Trim();
                    perg.numero = pergCount;
                    if (chkObrigatorio.Checked)
                    {
                        perg.obrigatorio = true;
                    }
                    else
                    {
                        perg.obrigatorio = false;
                    }

                    if (ddlReposta.SelectedValue.Equals("Opção") || ddlReposta.SelectedValue.Equals("Lista"))
                    {
                        pnlResposta.Visible = true;
                        Tipos tipo = controle.pesquisaTiposNome(ddlReposta.SelectedValue);
                        perg.tipo = tipo.id;
                    }

                    else if (ddlReposta.SelectedValue.Equals("Texto") || ddlReposta.SelectedValue.Equals("Numero") || ddlReposta.SelectedValue.Equals("Data"))
                    {
                        Tipos tipo = controle.pesquisaTiposNome(ddlReposta.SelectedValue);
                        perg.tipo = tipo.id;
                        btnAdcPergunta_Click(sender, e);
                    }

                    controle.salvarPergunta(perg);
                    controle.atualizarDados();
                    perguntaId = perg.id;
                    lblOpção.Visible = false;
                    pnlPerguntas.Enabled = false;
                    btnExcluir.Enabled = false;
                }
            }
        }

        protected void btnOpção_Click(object sender, EventArgs e)
        {
            lblOpção.Visible = false;           
            btnUpOrdem.Visible = false;
            btnDownOrdem.Visible = false;
            lblOrdem.Visible = false;
            btnRemovOpção.Visible = false;

            if (txtOpção.Text.Equals(""))
            {
                lblOpção.Visible = true;
                lblOpção.Text = "* O valor do texto para opção não deve ser vazio";
            }

            else
            {
                int flag = 0;
                List<Respostas> validação = controle.pesquisaGeralResposta(txtOpção.Text.Trim());
                foreach (Respostas value in validação)
                {
                    if (perguntaId == value.id_pergunta)
                    {
                        if (txtOpção.Text.Trim().ToUpper().Equals(value.resposta.ToUpper()))
                            flag++;
                    }
                }

                if (flag == 1)
                {
                    lblOpção.Visible = true;  
                    lblOpção.Text = "* Não deve haver duplicidade, já existe esta opção de resposta";
                    txtOpção.Text = "";                    
                }

                else
                {
                    resp = new Respostas();
                    resp.ordem = ordemResp;
                    resp.id_pergunta = perguntaId;

                    int result;
                    bool EhInt = int.TryParse(txtOpção.Text, out result);
                    if (EhInt)
                    {
                        resp.resposta = "\"" + txtOpção.Text + "\"";
                    }
                    else
                    {
                        resp.resposta = txtOpção.Text.Trim();
                    }
                    controle.salvarResposta(resp);
                    controle.atualizarDados();
                    ordemResp++;
                    qntOpc++;
                }
            }
            List<Respostas> listaResp = controle.pesquisaRespostaQuestão(perguntaId);
            carregaRadioList(listaResp);            
            txtOpção.Text = "";
        }

        protected void rblOpções_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnUpOrdem.Visible = true;
            btnDownOrdem.Visible = true;
            lblOrdem.Visible = true;
            btnRemovOpção.Visible = true;
            respostaTitulo = rblOpções.SelectedValue;
        }

        protected void btnRemovOpção_Click(object sender, EventArgs e)
        {
            int excluido;          
            btnRemovOpção.Visible = false;
            btnUpOrdem.Visible = false;
            btnDownOrdem.Visible = false;
            lblOrdem.Visible = false;
            resp = controle.pesquisaRespostaId(Convert.ToInt32(respostaTitulo));
            excluido = resp.ordem;
            controle.excluirResposta(resp);
            controle.atualizarDados();
            List<Respostas> listaResp = controle.pesquisaRespostaQuestão(perguntaId);
            foreach (Respostas value in listaResp)
            {
                if(value.ordem > excluido)
                {
                    value.ordem--;
                }
            }            
            carregaRadioList(listaResp);
            controle.atualizarDados();
            qntOpc--;   
        }

        protected void btnCancOpção_Click(object sender, EventArgs e)
        {
            perg = controle.pesquisaPerguntaId(perguntaId);
            List<Respostas> Lresp = controle.pesquisaRespostaQuestão(perg.id);
            foreach (Respostas value in Lresp)
            {
                controle.excluirResposta(value);
            }
            controle.excluirPergunta(perg);
            controle.atualizarDados();
            pnlPerguntas.Enabled = true;
            pnlOpções.Visible = false;
            pnlResposta.Visible = false;
            btnExcluir.Enabled = true;
            lblOpção.Visible = false;
            btnUpOrdem.Visible = false;
            btnDownOrdem.Visible = false;
            btnRemovOpção.Visible = false;
            List<Respostas> listaResp = controle.pesquisaRespostaQuestão(perguntaId);
            carregaRadioList(listaResp);
            ddlReposta.SelectedIndex = 0;            
        }

        protected void btnAdcPergunta_Click(object sender, EventArgs e)
        {
            perg = controle.pesquisaPerguntaId(perguntaId);
            perg.qnt_opcoes = qntOpc;
            controle.atualizarDados();

            pergCount++;
            pnlResposta.Visible = false;
            pnlOpções.Visible = false;
            pnlPerguntas.Visible = false;
            pnlPerguntas.Enabled = true;
            txtPergunta.Text = "";
            chkObrigatorio.Checked = false;
            ddlReposta.SelectedIndex = 0;
            btnAdicionar.Enabled = true;
            pnlSair.Visible = true;
        }

        protected void btnSalvarSair_Click(object sender, EventArgs e)
        {

            controle.atualizarDados();
            btnNovo.Enabled = true;
            quest = controle.pesquisaQuestionarioId(questionarioId);
            txtTitulo.Text = "";
            btnNovo.Enabled = true;
            btnSalvar.Visible = false;
            btnAdicionar.Enabled = false;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertaAtualizado", "alert('Criado o questionario com ID:" + questionarioId.ToString() + " e titulo:" + quest.nome + "');", true);
        }

        protected void btnUpOrdem_Click(object sender, ImageClickEventArgs e)
        {
            List<Respostas> Lresp = controle.pesquisaRespostaQuestão(perguntaId);
            resp = controle.pesquisaRespostaId(Convert.ToInt32(respostaTitulo));
            if (resp.ordem == 1)
            {

            }
            else
            {
                resp.ordem--;
                controle.atualizarDados();

                foreach (Respostas value in Lresp)
                {
                    if (resp.ordem == value.ordem)
                    {

                        resp = controle.pesquisaRespostaId(value.id);
                        resp.ordem++;
                        controle.atualizarDados();
                    }
                }
                Lresp = controle.pesquisaRespostaQuestão(perguntaId);
                carregaRadioList(Lresp);
                rblOpções.SelectedIndex = resp.ordem - 2;
            }
        }

        protected void btnDownOrdem_Click(object sender, ImageClickEventArgs e)
        {            
            List<Respostas> Lresp = controle.pesquisaRespostaQuestão(perguntaId);
            resp = controle.pesquisaRespostaId(Convert.ToInt32(respostaTitulo));
            if (resp.ordem >= Lresp.Count)
            {

            }
            else
            {
                resp.ordem = resp.ordem + 2;
                controle.atualizarDados();

                foreach (Respostas value in Lresp)
                {
                    if (resp.ordem == value.ordem)
                    {
                        resp = controle.pesquisaRespostaId(value.id);
                        resp.ordem--;
                        controle.atualizarDados();
                    }
                }
                Lresp = controle.pesquisaRespostaQuestão(perguntaId);
                carregaRadioList(Lresp);
                rblOpções.SelectedIndex = resp.ordem;
            }
        }
    }
}