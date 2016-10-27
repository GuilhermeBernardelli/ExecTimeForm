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
    public partial class Index : System.Web.UI.Page
    {
        Usuarios user;
        Controle controle = new Controle();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagem.Text = "Interface de Login";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            int registro;
            if (txtRegistro.Text.Equals(""))
            {
                lblAlerta.Text = "* O campo usuário e senha é de preenchimento obrigatório";
            }
            else
            {
                registro = Convert.ToInt32(txtRegistro.Text);
                user = controle.pesquisaUsuarioReg(registro);
                
                string senha = txtSenha.Text;
                if (user.senha.Equals(senha) && user.perfil == 1)
                {
                    Session["user"] = registro;
                    Response.Redirect("Usuario.aspx");
                }
                else
                {
                    lblAlerta.Text = "Registro ou Senha inválidos";
                }
            }

        }
    }
}