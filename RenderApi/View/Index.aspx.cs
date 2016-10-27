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
                registro = 0;
            }
            else
            {
                registro = Convert.ToInt32(txtRegistro.Text);
            }
            user = controle.pesquisaUsuarioReg(registro);
            string senha = txtSenha.Text;
            if (user.senha.Equals(senha))
            {
                Session["user"] = registro;
                Response.Redirect("Selecao.aspx");
            }
            else
            {
                lblAlerta.Text = "Registro ou Senha inválidos";
            }
            
        }
    }
}