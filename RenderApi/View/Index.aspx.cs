using RenderApi.Control;
using RenderApi.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RenderApi.View
{
    public partial class Index : System.Web.UI.Page
    {
        //classe do tipo referente a base de dados
        Usuarios user;
        //classe para a comunicação com a camada control
        Controle controle = new Controle();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagem.Text = "Interface de Login";
            //parametroes para comunicação com o sistema de login CAC
            string cacString = ConfigurationManager.AppSettings.Get("cacUrlLogin");
            btnCAC.PostBackUrl = cacString;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //variavel para validação do registro
                int registro;
                //valida o preenchimento dos campos registro e senha
                if (!txtRegistro.Text.Equals("") && !txtSenha.Text.Equals(""))
                {
                    //pesquisa por um usuário com o registro utilizado como entrada no campo registro
                    registro = Convert.ToInt32(txtRegistro.Text);
                    user = controle.pesquisaUsuarioReg(registro);
                    //variavel auxiliar para armazenar o conteudo dos valores digitados no campo senha
                    string senha = txtSenha.Text;
                    //verifica a existência do usuário na base de dados e informa o usuário do sistema
                    if (user == null)
                    {
                        //exibe mensagem e limpa os campos
                        lblAlerta.Text = "Registro/Senha inválidos, ou usuário não cadastrado";
                        txtRegistro.Text = "";
                        txtSenha.Text = "";
                    }
                    //verifica se a senha digitada no campo corresponde a senha cadastrada
                    else if (user.senha.Equals(senha))
                    {
                        //atribui a variavel de sessão o valor do registro do usuário e chama a interface de seleção
                        Session["user"] = registro;
                        Response.Redirect("Selecao.aspx");
                    }
                    //barra o usuário nos casos omissos e de usuário e senha não coincidentes
                    else
                    {
                        //exibe mensagem e limpa os campos
                        lblAlerta.Text = "Registro/Senha inválidos, ou usuário não cadastrado";
                        txtRegistro.Text = "";
                        txtSenha.Text = "";
                    }
                }
                //informa sobre a obrigatoriedade do preenchimento dos campos registro e senha
                else
                {
                    lblAlerta.Text = "* Campos registro e senha de preenchimento obrigatório";
                }
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros ao usuário
            catch { }
        }
        //função que inicia a aplicação com usuário anônimo
        protected void btnPublico_Click(object sender, EventArgs e)
        {
            //atribui a variavel de sessão o valor do usuário público e chama a interface de seleção
            int registro = 0;
            Session["user"] = registro;
            Response.Redirect("Selecao.aspx");
        }
    }
}