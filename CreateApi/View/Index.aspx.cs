using CreateApi.Control;
using CreateApi.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateApi.View
{
    public partial class Index : System.Web.UI.Page
    {
        //variavel global para armazenar o numero do registro do usuário
        static int registro;
        //objeto das classes da base de dados
        Usuarios user;
        //objeto para comunicação com a camada de controle
        Controle controle = new Controle();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Adição de texto na Label
                lblMensagem.Text = "Interface de Login";
                //Parametros para a comunicação com a interface de Login CAC
                string cacString = ConfigurationManager.AppSettings.Get("cacUrlLogin");
                btnCAC.PostBackUrl = cacString;
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //verifica se o campo usuário foi preenchido e trava a aplicação até o preenchimento
                if (txtRegistro.Text.Equals(""))
                {
                    lblAlerta.Text = "* O campo usuário e senha é de preenchimento obrigatório";
                }
                else
                {
                    //atribui a variavel registro o conteudo no campo usuário
                    registro = Convert.ToInt32(txtRegistro.Text);
                    //busca na base de dados o usuário por meio desse registro
                    user = controle.pesquisaUsuarioReg(registro);

                    //se o resultado dessa busca for nulo informa o usuário e reinicia o estado da página
                    if (user == null)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Alerta", "alert('Registro ou Senha inválidos, ou usuário sem os privilégios necessários')", true);
                        txtRegistro.Text = "";
                        txtSenha.Text = "";
                    }
                    //se o usuário existir, mas a senha estiver com o valor NULL na base de dados habilita o panel para cadastro de senha
                    else if (user.senha == null)
                    {
                        //modifica os controles da view 
                        pnlLogin.Visible = false;
                        pnlNovo.Visible = true;
                    }
                    //ações no caso da senha estar correta e o perfil possuir os privilégios para o uso do modulo
                    else if (user.senha.Equals(txtSenha.Text) && user.perfil == 1)
                    {
                        //cria variavel de sessão com o registro do usuário
                        Session["user"] = registro;
                        Response.Redirect("Usuario.aspx");
                    }
                    //se os padrões não se incluirem em nenhum dos casos exibe as mensagens de usuário e senha inválidos ou ausência de privilégios
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Alerta", "alert('Registro ou Senha inválidos, ou usuário sem os privilégios necessários')", true);
                        txtRegistro.Text = "";
                        txtSenha.Text = "";
                    }
                }
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }
        }
        // função de salvar senhas para usuários novos
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                //verifica a igualdade entre os campos senha e confimação
                if (txtNovo.Text.Equals(txtConfirma.Text))
                {
                    //busca na base de dados o usuário referente ao registro da tela inicial
                    user = controle.pesquisaUsuarioReg(registro);
                    //atribui a senha escolhida a este e salva a alteração
                    user.senha = txtNovo.Text.Trim();
                    controle.atualizarDados();
                    //informa o usuário e restabele os controles da view ao estado inicial
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Alerta", "alert('Senha incluida com sucesso')", true);
                    pnlLogin.Visible = true;
                    pnlNovo.Visible = false;
                    txtRegistro.Text = "";
                    txtSenha.Text = "";
                }
                else
                {
                    //informa o usuário que existe discrepancia entre a senha digitada e o campo de confirmação
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Alerta", "alert('A senha e confirmação não coincidem')", true);
                    txtNovo.Text = "";
                    txtConfirma.Text = "";
                }
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //restabelece as condições iniciais da page
            Response.Redirect("Index.aspx");
        }
    }
}