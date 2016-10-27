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
        Controle controle;
        Questionarios quest;
        static int questionario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                questionario = Convert.ToInt32(Session["questionarioId"]);
                quest = controle.pesquisaQuestionarioId(questionario);
                lblQuest.Text = quest.nome;
            }
        }
    }
}