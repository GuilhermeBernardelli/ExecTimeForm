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
    public partial class Acesso : System.Web.UI.Page
    {
        Controle controle = new Controle();
        Usuarios user = new Usuarios();
        static int registro;

        protected void Page_Load(object sender, EventArgs e)
        {

            //Variaveis de sessão recebidas no postback url
            string regist = Convert.ToString(Request.Form["hddRegFunc"]);
            string nome = Convert.ToString(Request.Form["hddNomeFunc"]);
            string perfil = Convert.ToString(Request.Form["hddEnumPerfil"]);

            //verifica a pré existencia do usuário na base de dados
            if (controle.pesquisaUsuarioReg(Convert.ToInt32(regist)) == null)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Usuário CAC não cadastrado na aplicação')", true);
                //Adiciona o usuário do acesso via postback url a base de dados 
                user = new Usuarios();
                controle.salvarUsuario(user);
                user.nome = nome;
                user.registro = Convert.ToInt32(regist);
                user.perfil = Convert.ToInt32(perfil);
                //salva a adição de usuário
                controle.atualizarDados();
            }
            else
            {
                //busca na base de dados por meio de variavel de sessão passada pelo CAC
                registro = Convert.ToInt32(regist);
                user = controle.pesquisaUsuarioReg(registro);
            }
            
            if (perfil == "1")
            {
                Session["user"] = user.registro;
                Response.Redirect("Usuario.aspx");
            }

            if (perfil != "1")
            {
                Session["user"] = user.registro;
                Response.Redirect("Selecao.aspx");
            }

        }
            
    }

}