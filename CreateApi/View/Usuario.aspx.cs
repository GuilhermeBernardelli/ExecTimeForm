using CreateApi.Control;
using CreateApi.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CreateApi.View
{
    public partial class Usuario : System.Web.UI.Page
    {
        //objeto que faz a comunicação com a camada control
        Controle controle = new Controle();
        //objetos e listas das classes da base de dados
        Renderizar render;
        List<Questionarios> LQuest;
        List<Usuarios> LUser;
        /* variavel para identificar o tipo de inclusão 
         * "quest" para questionario ao usuário
         * e "user" para usuário ao questionário */
        static string tipo = null;
        //variavel para impedir inclusão duplicada
        bool existe = false;
        //variavel data para a inclusão de validade ao adicionar usuários ao questionário
        static string data;
        //uma list para armazenar as datas de validade na inclusão de questionarios ao usuário
        static List<DateTime> dataLista = new List<DateTime>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //função para reestabelecer os controles a condição inicial, não altera os panels
        public void limpaFunction()
        {
            chkSelecionados.Items.Clear();
            rblPerfil.SelectedIndex = -1;
            //altera unicamente o panel para inclusão de usuários
            pnlUser.Visible = false;
            lblAvisoQuest.Visible = false;
            lblAvisoUser.Visible = false;
            rblQuest.Visible = false;
            rblUser.Visible = false;
            txtQuest.Text = "";
            txtUser.Text = "";            
            txtRegistro.Text = "";
            txtNomeCompleto.Text = "";
            
        }
        //funções para seleção de método de inclusão:
        //inclusão de usuários ao questionário
        protected void btnUserQuest_Click(object sender, EventArgs e)
        {
            //habilita o panel para busca do questionário que irá receber usuários
            pnlPesquisaQuest.Visible = true;
            //modifica os controles da view
            pnlMetodo.Visible = false;
        }
        //inclusão de questionários ao usuário
        protected void btnQuestUser_Click(object sender, EventArgs e)
        {
            //habilita o panel para busca do usuário que irá receber questionários
            pnlPesquisaUser.Visible = true;
            //modifica os controles da view
            pnlMetodo.Visible = false;
        }
        //inclusão de novo usuário
        protected void btnAdcUsuario_Click(object sender, EventArgs e)
        {
            //modifica os controles da view para o padrão de adição de usuário
            pnlUser.Visible = true;
            pnlMetodo.Visible = false;
            txtRegistro.Text = "";
            txtNomeCompleto.Text = "";
            rblPerfil.SelectedIndex = -1;
        }
        //função para busca de questionários
        protected void btnPesqQuest_Click(object sender, EventArgs e)
        {
            //verifica a existência de parametro de busca
            if (txtQuest.Text.Equals(""))
            {
                lblAvisoQuest.Visible = true;
                lblAvisoQuest.Text = "* O campo pesquisa não pode ser vazio";
            }
            //havendo parametros de busca executa as instruções a seguir
            else
            {
                lblAvisoQuest.Visible = false;
                //adiciona a variavel tipo Lista Questionarios os questionários que contenham o parametro de busca
                LQuest = controle.pesquisaGeralQuestionario(txtQuest.Text);
                //verifica se houveram questionários que atenderam ao parametro de busca
                if (LQuest.Count == 0)
                {
                    lblAvisoQuest.Visible = true;
                    lblAvisoQuest.Text = "A pesquisa não retornou resultados, altere o parametro e tente novamente";
                }
                //else if parametro com mais de X resultados

                /*Caso ao menos um questionário atenda ao parametro de busca, 
                * e não existam mais de X questionários que atendem este parametro
                * executa as instruções a seguir */
                else
                {
                    //carrega a radio button list com os resultados da busca
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
        //função para a busca de usuários
        protected void btnPesqUser_Click(object sender, EventArgs e)
        {
            //verifica a se existe algum parametro de busca
            if (txtUser.Text.Equals(""))
            {
                lblAvisoUser.Visible = true;
                lblAvisoUser.Text = "* O campo pesquisa não pode ser vazio";
            }
            //havendo parametros de busca executa as instruções a seguir
            else
            {
                lblAvisoUser.Visible = false;
                //pesquisa a base de usuários e retorna aqueles que atendam o parametro de busca
                LUser = controle.pesquisaGeralUsuarios(txtUser.Text);
                //verifica se a pesquisa retornou vazia
                if (LUser.Count == 0)
                {
                    lblAvisoUser.Visible = true;
                    lblAvisoUser.Text = "A pesquisa não retornou resultados, altere o parametro e tente novamente";
                }
                //else if parametro com mais de X resultados

                /*Caso ao menos um usuário atenda ao parametro de busca, 
                * e não existam mais de X usuários que atendem este parametro
                * executa as instruções a seguir */
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
            //verifica se já houve a seleção de tipo de pesquisa
            if (tipo == null)
            {
                //modifica os controles da view para a inclusão de usuários ao questionário
                btnSalvar.Enabled = false;
                lblTitulo.Text = "Inclusão de usuários ao questionário";
                lblTipo.Visible = true;
                lblTipo.Text = rblQuest.SelectedItem.Text + ":";
                pnlPesquisaQuest.Visible = false;
                pnlPrincipal.Visible = true;
                btnAlterar.Text = "Outro Question.";
                //atribui valor a variavel tipo
                tipo = "quest";                
            }
            //caso já haja um tipo atribuido a variavel tipo executa as instruções a seguir
            else
            {
                //habilita o botão Salvar Alterações
                btnSalvar.Enabled = true;
                for (int i = 0; i < chkSelecionados.Items.Count; i++)
                {
                    //verifica na relação dos questionários adicionados ao usuário de existe o questionário selecionado
                    if (chkSelecionados.Items[i].Equals(rblQuest.SelectedItem))
                    {
                        //caso exista atribui valor true a variavel
                        existe = true;
                    }
                }
                //no caso da variavel existe permanecer como false
                if (!existe)
                {
                    //modifica os conteudos de texto da view
                    btnVoltaQuest.Text = "Desfazer";
                    txtQuest.Text = "";
                    //adiciona a checkbox list de questionários o item selecionado
                    chkSelecionados.Items.Add(rblQuest.SelectedItem);
                    //limpa a radio button list de seleção
                    rblQuest.Items.Clear();
                    //modifica os controles da view
                    chkSelecionados.Visible = true;
                    pnlPrincipal.Enabled = false;
                    pnlPesquisaQuest.Visible = false;
                    pnlDataValidade.Visible = true;
                }
                //caso já exista no checkbox list o valor selecionado na radio button list, executa as instruções a seguir para evitar duplicidade
                else
                {
                    existe = false;
                    lblAvisoQuest.Visible = true;
                    lblAvisoQuest.Text = "Questionário já incluso na lista";
                }
            }
        }
        //função para inclusão de questionários ao usuário
        protected void rblUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            //verifica se já houve a seleção de tipo de pesquisa
            if (tipo == null)
            {
                //modifica os controles da view para a inclusão de questionários ao usuário
                btnSalvar.Enabled = false;
                lblTitulo.Text = "Inclusão de questionários ao usuário";
                lblTipo.Visible = true;
                lblTipo.Text = rblUser.SelectedItem.Text + ":";
                pnlPesquisaUser.Visible = false;
                pnlPrincipal.Visible = true;
                btnAlterar.Text = "Outro Usuário";
                //atribui valor a variavel tipo
                tipo = "user";
            }
            //caso já haja um tipo atribuido a variavel tipo executa as instruções a seguir
            else
            {
                //habilita o botão Salvar Alterações
                btnSalvar.Enabled = true;
                for (int i = 0; i < chkSelecionados.Items.Count; i++)
                {
                    //verifica na relação dos usuários adicionados ao questionário se existe o usuário selecionado
                    if (chkSelecionados.Items[i].Equals(rblUser.SelectedItem))
                    {
                        //caso exista atribui valor true a variavel
                        existe = true;
                    }
                }
                //no caso da variavel existe permanecer como false
                if (!existe)
                {
                    //modifica os conteudos de texto da view
                    txtUser.Text = "";
                    //adiciona a checkbox list de usuários o item selecionado
                    chkSelecionados.Items.Add(rblUser.SelectedItem);
                    //limpa a radio button list de seleção
                    rblUser.Items.Clear();
                    //modifica os controles da view
                    chkSelecionados.Visible = true;
                    btnVoltaUser.Text = "Desfazer";
                }
                //caso já exista no checkbox list o valor selecionado na radio button list, executa as instruções a seguir para evitar duplicidade
                else
                {
                    existe = false;
                    lblAvisoUser.Visible = true;
                    lblAvisoUser.Text = "Usuário já incluso na lista";
                }
            }
        }
        //função para realizar inclusões no item selecionado
        protected void btnInclude_Click(object sender, EventArgs e)
        {
            //modifica os controles da view
            btnInclude.Enabled = false;
            //limpa o estado dos controles da view
            limpaFunction();
            // realiza a leitura da variavel tipo
            if (tipo.Equals("user"))
            {
                //modifica os controles da view
                pnlPesquisaQuest.Visible = true;
                txtQuest.Text = "";
            }
            else//tipo.Equals("quest")
            {
                //modifica os controles da view, panel para inclusão da data de validade do questionário
                pnlDataValidade.Visible = true;
            }
        }
        
        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            //modifica os controles da view
            btnInclude.Enabled = true;
            limpaFunction();
            //condicionais para identificar o tipo de alteração
            if (tipo.Equals("quest"))
            {
                //modifica os controles da view
                pnlPrincipal.Visible = false;
                pnlPesquisaQuest.Visible = true;
                pnlPesquisaUser.Visible = false;
                txtQuest.Text = "";
                //estabelece como nulo o tipo de metodo de inclusão
                tipo = null;
            }
            else
            {
                //modifica os controles da view
                pnlPrincipal.Visible = false;
                pnlPesquisaUser.Visible = true;
                pnlPesquisaQuest.Visible = false;
                txtUser.Text = "";
                //estabelece como nulo o tipo de metodo de inclusão
                tipo = null;
            }
        }
        // função para cancelar alterações e retornar ao estado inicial da page
        protected void btnCancela_Click(object sender, EventArgs e)
        {
            tipo = null;
            Response.Redirect("Usuario.aspx");            
        }
        //função para salvar alterações realizadas
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (tipo.Equals("quest"))
            {
                //cria uma lista do tipo usuários
                LUser = new List<Usuarios>();
                foreach (ListItem value in chkSelecionados.Items)
                {
                    //para cada elemento na checkbutton list adiciona um novo usuário
                    LUser.Add(controle.pesquisaUsuarioNomeCompleto(value.Text));
                }
                foreach (Usuarios value in LUser)
                {
                    //para cada usuário adicionado a lista, inclui os dados na base de dados do modulo de renderização
                    render = new Renderizar();
                    controle.salvarRender(render);
                    render.id_questionario = Convert.ToInt32(rblQuest.SelectedValue);
                    render.data_renderizado = DateTime.Now;
                    render.id_usuario = value.id;
                    //utiliza como parametro a data da variável data
                    render.data_validade = Convert.ToDateTime(data);
                    controle.atualizarDados();
                }
            }
            else//tipo.Equals("user")
            {
                //cria uma lista do tipo questionários
                LQuest = new List<Questionarios>();
                int i = 0;
                foreach (ListItem value in chkSelecionados.Items)
                {
                    //para cada elemento na checkbutton list adiciona um novo questionário
                    LQuest.Add(controle.pesquisaQuestionarioNome(value.Text));
                }
                foreach (Questionarios value in LQuest)
                {
                    //para cada questionário adicionado a lista, inclui os dados na base de dados do modulo de renderização
                    render = new Renderizar();
                    controle.salvarRender(render);
                    render.id_usuario = Convert.ToInt32(rblUser.SelectedValue);
                    render.data_renderizado = DateTime.Now;
                    render.id_questionario = value.id;    
                    //utiliza como parametro a lista de datas                
                    render.data_validade = dataLista[i++];
                    controle.atualizarDados();
                }
            }
            Response.Redirect("Usuario.aspx");
        }
        //interface para o tratamento do objeto data
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

       

        protected void btnSalvaUser_Click(object sender, EventArgs e)
        {
            try
            {
                Usuarios usuario = new Usuarios();
                controle.salvarUsuario(usuario);
                if (!txtNomeCompleto.Text.Equals(""))
                {
                    if (!txtRegistro.Text.Equals(""))
                    {
                        if (rblPerfil.SelectedIndex != -1)
                        {
                            usuario.nome = txtNomeCompleto.Text;
                            usuario.registro = Convert.ToInt32(txtRegistro.Text);
                            usuario.perfil = Convert.ToInt32(rblPerfil.SelectedValue);
                            controle.atualizarDados();
                        }
                        else
                        {
                            lblAvisoUsuario.Visible = true;
                            lblAvisoUsuario.Text = "* A seleção de perfil do usuário é obrigatória";
                        }
                    }
                    else
                    {
                        lblAvisoUsuario.Visible = true;
                        lblAvisoUsuario.Text = "* O campo Registro é de preenchimento obrigatório";
                    }
                }
                else
                {
                    lblAvisoUsuario.Visible = true;
                    lblAvisoUsuario.Text = "* O campo Nome Completo é de preenchimento obrigatório";
                }
            }
            catch (DbUpdateException ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertaDb", "alert('Não foi possivel criar o usuário, tente novamente')",true);
                limpaFunction();
            }
        }

        protected void btnCancelaUser_Click(object sender, EventArgs e)
        {
            pnlUser.Visible = false;
            pnlMetodo.Visible = true;
            limpaFunction();            
        }
    }
}