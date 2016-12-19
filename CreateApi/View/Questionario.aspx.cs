using CreateApi.Control;
using CreateApi.Model;
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
        //objeto que faz a comunicação com a camada control
        Controle controle = new Controle();
        //classes referente aos objetos da base de dados 
        Questionarios quest;
        Perguntas perg;
        Respostas resp;
        //contador de pergunta
        static int pergCount = 1;
        //variavel referente ao conteudo da base de dados, pergunta.id
        static int perguntaId = 0;
        //variavel referente ao conteudo da base de dados, questionario.id
        static int questionarioId = 0;
        //variavel que recebe o valor selecionado na lista de opções de respostas para determinada pergunta
        static string respostaTitulo;
        /* variaveis que recebem respectivamente, o numero da opção de resposta para ordenação,
         *  e a quantidade de opções de resposta de determinada pergunta */        
        static int ordemResp, qntOpc;

        protected void Page_Load(object sender, EventArgs e)
        {
            //função executada apenas no page load
            if (!IsPostBack)
            {
                btnVoltar.Enabled = true;
                //pesquisa na base os tipos de respostas possivel cadastrados
                List<Tipos> listaTipos = controle.pesquisaTipos();
                //adiciona a descrição de cada tipo encontrado no drop down list de tipos
                foreach (Tipos value in listaTipos)
                {
                    ddlReposta.Items.Add(value.desc_tipo);
                }

                try
                {
                    //cria objeto da novo da base de dados
                    Usuarios user = new Usuarios();
                    //verifica se o login foi feito por usuário + senha na página Index.aspx
                    if (Session["user"].ToString() == "")
                    {
                        //Variaveis de sessão recebidas no postback url
                        string registro = Convert.ToString(Request.Form["hddRegFunc"]);
                        string nome = Convert.ToString(Request.Form["hddNomeFunc"]);
                        string perfil = Convert.ToString(Request.Form["hddEnumPerfil"]);

                        //verifica a pré existencia do usuário na base de dados
                        if (controle.pesquisaUsuarioReg(Convert.ToInt32(registro)) == null)
                        {
                            //Adiciona o usuário do acesso via postback url a base de dados 
                            controle.salvarUsuario(user);
                            user.nome = nome;
                            user.registro = Convert.ToInt32(registro);
                            user.perfil = Convert.ToInt32(perfil);
                            //salva a adição de usuário
                            controle.atualizarDados();
                        }
                    }
                    //busca na base de dados o usuário no caso de login por usuário + senha
                    else
                    {
                        //busca na base de dados por meio de variavel de sessão
                        int registro = Convert.ToInt32(Session["user"]);
                        user = controle.pesquisaUsuarioReg(registro);
                    }
                    //verifica se o perfil do usuário possui os privilégios para a utilização do módulo
                    if (user.perfil != 1) //perfil 1 = administrador da aplicação
                    {
                        Response.Redirect("Index.aspx");
                    }
                }
                //instruções circundadas com try, catch para evitar a exibição de possíveis erros
                catch { }            
            }           
        }
        //função para determinar que o questionário volte ao estado original
        public void limpaFunction()
        {            
            btnNovo.Enabled = true;
            txtTitulo.Enabled = true;
            txtTitulo.Text = "";
        }
        //função para preencher a radio button list com a lista das opções de resposta de determinada pergunta
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
        //função para adição de perguntas a um questionário
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                //impede a execução da função caso o questionário não tenha um nome determinado no campo de texto referente ao titulo
                if (txtTitulo.Text.Equals(""))
                {
                    lblAlerta.Visible = true;
                    lblAlerta.Text = "* O campo Titulo do Questionário não pode ser vazio.";
                }
                //caso haja um titulo executa as instruções seguintes
                else
                {
                    // tenta, criar objeto questionario, atribuir um titulo e um id
                    try
                    {
                        quest = new Questionarios();
                        quest.nome = txtTitulo.Text.Trim();
                        quest.ativo = true;

                        txtTitulo.Enabled = false;
                        lblAlerta.Visible = false;
                        btnNovo.Enabled = false;
                        btnEditar.Enabled = true;
                        btnAdicionar.Enabled = true;
                        btnVoltar.Enabled = false;

                        controle.salvarQuestionario(quest);
                        controle.atualizarDados();
                        questionarioId = quest.id;

                        //informa ao usuário o sucesso da operação
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertaAtualizadoOK", "alert('Inclusão realizada com sucesso.');", true);
                    }
                    catch (DbUpdateException ex)
                    {
                        //informa o não sucesso da operação ao usuário, e o porque
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertaSelecionar", "alert('Houve o seguinte erro ao inserir os dados = " + ex.ToString() + " .')", true);
                        limpaFunction();
                    }
                }
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros ao usuário
            catch { }         
        }

        //altera os comandos da view para o modo de edição
        protected void btnEditar_Click(object sender, EventArgs e)
        {                        
            txtTitulo.Enabled = true;
            btnEditar.Enabled = false;
            btnAdicionar.Enabled = false;
            //modifica a visibilidade dos botões entre o de adição e o de alteração
            btnSalvar.Visible = true;
            btnNovo.Visible = false;
        }

        //função para salvar alterações feitas no titulo do questionário
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                quest = new Questionarios();
                //busca na base de dados o questionario a ser atualizado
                quest = controle.pesquisaQuestionarioId(questionarioId);
                quest.nome = txtTitulo.Text.Trim();
                quest.ativo = true;
                controle.atualizarDados();
                btnNovo.Visible = true;
                btnSalvar.Visible = false;
                btnEditar.Enabled = true;
                txtTitulo.Enabled = false;
                btnAdicionar.Enabled = true;
                btnVoltar.Enabled = false;
                //informa ao usuário o sucesso da operação
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertaAtualizadoOK", "alert('Alteração realizada com sucesso.');", true);
            }
            catch (DbUpdateException ex)
            {
                //informa o não sucesso da operação ao usuário, e o porque
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertaSelecionar", "alert('Houve o seguinte erro ao atualizar os dados = " + ex.ToString() + " .')", true);
                limpaFunction();
            }
        }

        //altera os comandos da view para o modo de inserção de perguntas
        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            pnlAlerta.Visible = false;
            pnlPerguntas.Visible = true;
            //atribui a Label o numero a que se refere determinada pergunta
            lblN.Text = pergCount.ToString();
            btnAdicionar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = true;
            pnlPerguntas.Enabled = true;
            chkObrigatorio.Checked = false;
            txtPergunta.Text = "";
            pnlSair.Visible = false;
            btnVoltar.Enabled = false;
        }      
        // função para exclusão do questionário
        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                // modifica os controles da página para atender a função
                btnNovo.Enabled = true;
                btnEditar.Enabled = true;
                pnlPerguntas.Visible = false;

                // atribui a variavel as perguntas referentes ao questionário que está sendo criado
                List<Perguntas> Lperg = controle.pesquisaPerguntaQuestionario(questionarioId);
                foreach (Perguntas value in Lperg)
                {
                    //para cada pergunta procura as opções de resposta e atribui a variavel
                    List<Respostas> Lresp = controle.pesquisaRespostaQuestão(value.id);
                    foreach (Respostas var in Lresp)
                    {
                        //exclui da base de respostas cada opção da pergunta referente a este value.id
                        controle.excluirResposta(var);
                    }
                    // após a exclusão das opções de resposta, exclui a pergunta
                    controle.excluirPergunta(value);
                }//volta ao inicio do primeiro laço foreach

                //atribui a variavel quest o questionário que sofreu a exclusão de resposta e perguntas
                quest = controle.pesquisaQuestionarioId(questionarioId);
                //exclui o questionário
                controle.excluirQuestionario(quest);
                controle.atualizarDados();
                //retorna os comandos a condição inicial
                btnEditar.Enabled = false;
                txtTitulo.Text = "";
                txtTitulo.Enabled = true;
                btnExcluir.Enabled = false;
                btnVoltar.Enabled = true;
                // reinicia a variável que faz a contagem de perguntas
                pergCount = 1;
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros ao usuário
            catch { }
        }

        //drop down list referente aos tipos de resposta existente na base de dados
        protected void ddlReposta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //reinicia a contagem de ordem das opções de resposta e a quantidade de opções 
                ordemResp = 1;
                qntOpc = 0;
                lblAlerta.Visible = false;
                // verifica se o campo do titulo da pergunta está preenchido
                if (txtPergunta.Text.Equals(""))
                {
                    lblAlertaResp.Text = "* O campo pergunta deve ser preenchido";
                    pnlAlerta.Visible = true;
                    ddlReposta.SelectedIndex = 0;
                }
                // verifica se o drop down list não está na posição inicial
                else if (ddlReposta.SelectedValue.Equals("Selecione"))
                {
                    pnlAlerta.Visible = true;
                    lblAlertaResp.Text = "* Seleção de tipo de resposta obrigatória";
                }
                // verifica se a pergunta possuí mais de 150 caracteres, limite atual da base de dados, Tabela Perguntas, campo titulo
                else if (txtPergunta.Text.Length > 150)
                {
                    pnlAlerta.Visible = true;
                    lblAlertaResp.Text = "* O campo pergunta está limitado a 150 caracteres, reformule a pergunta";
                    ddlReposta.SelectedIndex = 0;
                }

                else
                {
                    //cria uma variavel para a validação de duplicidade de pergunta, atribui o valor false
                    bool flag = false;
                    //cria uma variavel, validação, com todas as peguntas do questionário
                    List<Perguntas> validação = controle.pesquisaPerguntaQuestionario(questionarioId);
                    foreach (Perguntas value in validação)
                    {
                        //para cada pergunta testa se existe outra pergunta com o titulo da pergunta atual
                        if (txtPergunta.Text.Trim().ToUpper().Equals(value.titulo.ToUpper()))
                        {
                            //havendo duplicidade altera a variável flag
                            flag = true;
                        }
                    }
                    //valida a duplicidade pela variavel flag
                    if (flag)
                    {
                        //modifica os controles da view para o estado de inclusão de pergunta               
                        pnlAlerta.Visible = true;
                        lblAlertaResp.Text = "* Não deve haver duplicidade, já existe esta pergunta neste questionário";
                        txtPergunta.Text = "";
                        ddlReposta.SelectedIndex = 0;
                    }
                    /*não havendo duplicidade, valor vazio de titulo, titulo com mais de 150 caracteres 
                    e havendo uma seleção valida no drop down list, executa as instruções seguintes */
                    else
                    {
                        //instancia de pergunta                
                        perg = new Perguntas();
                        controle.salvarPergunta(perg);
                        //preenchimento dos valores da instancia criada
                        perg.id_questionario = questionarioId;
                        perg.titulo = txtPergunta.Text.Trim();
                        perg.numero = pergCount;
                        //verifica a obrigatoriedade de resposta para esta pergunta
                        if (chkObrigatorio.Checked)
                        {
                            perg.obrigatorio = true;
                        }
                        else
                        {
                            perg.obrigatorio = false;
                        }
                        //verifica se a pergunta pode possuir opções de resposta
                        if (ddlReposta.SelectedValue.Equals("Opção") || ddlReposta.SelectedValue.Equals("Lista"))
                        {
                            //modifica os controles da pagina view para a adição de opções de resposta
                            pnlResposta.Visible = true;
                            //busca na base o tipo cadastrado pelo valor selecionado da drop down list
                            Tipos tipo = controle.pesquisaTiposNome(ddlReposta.SelectedValue);
                            //atribui a pergunta o id deste tipo
                            perg.tipo = tipo.id;
                            //salva as alterações para a base de dados gerar a primary key, tipo int, identity
                            controle.atualizarDados();
                            //atribui a variavel o valor do campo identity
                            perguntaId = perg.id;
                        }
                        // caso a pergunta não aceite opções em sua resposta
                        else if (ddlReposta.SelectedValue.Equals("Texto") || ddlReposta.SelectedValue.Equals("Número") || ddlReposta.SelectedValue.Equals("Data"))
                        {

                            //busca na base o tipo cadastrado pelo valor selecionado da drop down list
                            Tipos tipo = controle.pesquisaTiposNome(ddlReposta.SelectedValue);
                            //atribui a pergunta o id deste tipo
                            perg.tipo = tipo.id;
                            //salva as alterações para a base de dados gerar a primary key, tipo int, identity
                            controle.atualizarDados();
                            //atribui a variavel o valor do campo identity
                            perguntaId = perg.id;
                            //realiza a ação do click do botão adicionar pergunta
                            btnAdcPergunta_Click(sender, e);
                        }
                        //modifica os controles para o estado de adição de pergunta ou salvar questionário
                        lblOpção.Visible = false;
                        pnlPerguntas.Enabled = false;
                        btnExcluir.Enabled = false;
                    }
                }
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros ao usuário
            catch { }
        }
        //função para adição de nova opção de resposta
        protected void btnOpção_Click(object sender, EventArgs e)
        {
            try
            {
                //modifica os controles da view para aceitar adição de nova opção de resposta
                lblOpção.Visible = false;
                btnUpOrdem.Visible = false;
                btnDownOrdem.Visible = false;
                lblOrdem.Visible = false;
                btnRemovOpção.Visible = false;
                //valida o preenchimento do campo texto, titulo da opção a ser adicionada
                if (txtOpção.Text.Equals(""))
                {
                    lblOpção.Visible = true;
                    lblOpção.Text = "* O valor do texto para opção não deve ser vazio";
                }
                //validação do tamanho do texto da opção, limitado a 20 caracteres, conforme a base Respostas, campo resposta
                else if (txtOpção.Text.Length > 20)
                {
                    pnlAlerta.Visible = true;
                    lblAlertaResp.Text = "* A opção está limitada a 20 caracteres, reformule a pergunta";
                    ddlReposta.SelectedIndex = 0;
                }
                //caso preenchido o campo texto, executa o conjunto de instruções abaixo
                else
                {
                    //cria flag para validação da duplicidade de opção de resposta
                    bool flag = false;
                    //cria uma variavel com todas opções de resposta para a pergunta
                    List<Respostas> validação = controle.pesquisaRespostaQuestão(perguntaId);
                    foreach (Respostas value in validação)
                    {
                        //para cada opção valida se o texto já existe para aquela pergunta
                        if (txtOpção.Text.Trim().ToUpper().Equals(value.resposta.ToUpper()))
                        {
                            //havendo duplicidade altera o valor do flag de validação
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        //modifica os controles da view para a condição de adição de opção
                        lblOpção.Visible = true;
                        lblOpção.Text = "* Não deve haver duplicidade, já existe esta opção de resposta";
                        txtOpção.Text = "";
                    }
                    //não havendo duplicidade, valor vazio de texto para opção e opção com mais de 20 caracteres executa as instruções seguintes
                    else
                    {
                        //instancia de resposta
                        resp = new Respostas();
                        // atribui os valores de resposta
                        resp.ordem = ordemResp;
                        resp.id_pergunta = perguntaId;
                        //cria variaveis para verificar se a opção é um numero
                        int result;
                        bool EhInt = int.TryParse(txtOpção.Text, out result);
                        if (EhInt)
                        {
                            //sendo a opção um número coloca aspas ao lado dele
                            resp.resposta = "\"" + txtOpção.Text + "\"";
                        }
                        else
                        {
                            //não sendo número remove espaços a frente e após o texto da opção
                            resp.resposta = txtOpção.Text.Trim();
                        }
                        // salva a resposta e atualiza a base de dados
                        controle.salvarResposta(resp);
                        controle.atualizarDados();
                        //incrementa os valores de ordem da resposta e quantidade de opções que a pergunta possui
                        ordemResp++;
                        qntOpc++;
                    }
                }
                //carrega na lista e expõe no radio button list as opções de resposta da pergunta que está sendo criada
                List<Respostas> listaResp = controle.pesquisaRespostaQuestão(perguntaId);
                carregaRadioList(listaResp);
                // modifica o controle para adição de nova opção ou salvar a pergunta            
                txtOpção.Text = "";
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros ao usuário
            catch { }
        }

        protected void rblOpções_SelectedIndexChanged(object sender, EventArgs e)
        {
            //modifica os controles da view caso haja a seleção na radio button list
            btnUpOrdem.Visible = true;
            btnDownOrdem.Visible = true;
            lblOrdem.Visible = true;
            btnRemovOpção.Visible = true;
            respostaTitulo = rblOpções.SelectedValue;
        }
        //função para exclusão de opção selecionada
        protected void btnRemovOpção_Click(object sender, EventArgs e)
        {
            try
            {
                // cria variavel para manter a ordenação da radio button list
                int excluido;
                // modifica os controles da view para retornar ao estado anterior ao da seleção de objeto da radio button list       
                btnRemovOpção.Visible = false;
                btnUpOrdem.Visible = false;
                btnDownOrdem.Visible = false;
                lblOrdem.Visible = false;
                //carrega na variavel o conteudo da resposta selecionada
                resp = controle.pesquisaRespostaId(Convert.ToInt32(respostaTitulo));
                // atribui o numero de ordem da resposta a ser excluida
                excluido = resp.ordem;
                //exclui a resposta e atualiza a base de dados
                controle.excluirResposta(resp);
                controle.atualizarDados();
                //carrega a variavel lista com as opções restantes
                List<Respostas> listaResp = controle.pesquisaRespostaQuestão(perguntaId);
                foreach (Respostas value in listaResp)
                {
                    // verifica, entre as opções restantes, quais estavam depois da opção excluida 
                    if (value.ordem > excluido)
                    {
                        //decrementa o numero de ordem das opções posteriores a opção excluida
                        value.ordem--;
                    }
                }
                //carrega a radio button list com as opções restantes e salva as alterações na base de dados            
                carregaRadioList(listaResp);
                controle.atualizarDados();
                //decrementa a quantidade de opções de resposta daquela pergunta
                qntOpc--;
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros ao usuário
            catch { }
        }
        //função para cancelar a inclusão de opções de resposta para a pergunta
        protected void btnCancOpção_Click(object sender, EventArgs e)
        {
            try
            {
                //carrega na variavel os valores daquela pergunta
                perg = controle.pesquisaPerguntaId(perguntaId);
                // cria uma lista com as opções de resposta desta pergunta
                List<Respostas> Lresp = controle.pesquisaRespostaQuestão(perg.id);
                foreach (Respostas value in Lresp)
                {
                    //exclui cada opção de resposta para a pergunta
                    controle.excluirResposta(value);
                }
                //exclui a pergunta e atualiza a base de dados
                controle.excluirPergunta(perg);
                controle.atualizarDados();
                //modifica os controles da view para a inclusão de nova pergunta ou salvar o questionário
                pnlPerguntas.Enabled = true;
                pnlOpções.Visible = false;
                pnlResposta.Visible = false;
                btnExcluir.Enabled = true;
                lblOpção.Visible = false;
                btnUpOrdem.Visible = false;
                btnDownOrdem.Visible = false;
                btnRemovOpção.Visible = false;
                ddlReposta.SelectedIndex = 0;
                txtPergunta.Text = "";
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros ao usuário
            catch { }
        }
        //função para salvar pergunta com opções de resposta
        protected void btnAdcPergunta_Click(object sender, EventArgs e)
        {
            try
            {
                //pesquisa a instancia de pergunta criada
                perg = controle.pesquisaPerguntaId(perguntaId);
                //atribui ao campo a quantidade final de opções armazenado na variavel qntOpc
                perg.qnt_opcoes = qntOpc;
                //salva as alterações
                controle.atualizarDados();
                //incrementa o contador de perguntas
                pergCount++;
                //modifica o estado dos controle da view
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
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros ao usuário
            catch { }
        }
        //função para salvar as alterações de inclusão de novo questionário e suas perguntas e opções de resposta
        protected void btnSalvarSair_Click(object sender, EventArgs e)
        {
            try
            {
                //salva o estado final
                controle.atualizarDados();
                //pesquisa na base o questionário referente ao id criado para inclusão
                quest = controle.pesquisaQuestionarioId(questionarioId);
                //modifica os controles da view para o estado inicial
                btnNovo.Enabled = true;
                txtTitulo.Text = "";
                btnNovo.Enabled = true;
                btnSalvar.Visible = false;
                btnAdicionar.Enabled = false;
                //exibe mensagem em box com a informação de inclusão de questionário e informa o id e titulo deste
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertaAtualizado", "alert('Criado o questionario com ID:" + questionarioId.ToString() + " e titulo:" + quest.nome + "');", true);
            }
            catch (DbUpdateException ex)
            {
                //mensagem em caso de erros ao atualizar o banco de dados
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertaNaoAtualizado", "alert('Não foi possivel efetuar a criação do questionario ID:" + questionarioId.ToString() + " e titulo:" + quest.nome + ", error message: " + ex.ToString() + "');", true);
            }
            finally
            {
                //redireciona o usuário devolta á pagina de atribuição, criação e seleção
                Response.Redirect("Usuario.aspx");
            }
        }
        //função para alterar a ordenação da opção selecionada para cima
        protected void btnUpOrdem_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //carrega na variavel a lista de opções para a resposta
                List<Respostas> Lresp = controle.pesquisaRespostaQuestão(perguntaId);
                //carrega na variavel a opção selecionada na radio button list
                resp = controle.pesquisaRespostaId(Convert.ToInt32(respostaTitulo));
                //verifica se a opção selecionada para subir na lista já não esta no topo, não estando executa as instruções abaixo
                if (resp.ordem != 1)
                {
                    //decrementa o numero de ordem dessa questão, sobe na radio button list, e salva a alteração
                    resp.ordem--;
                    controle.atualizarDados();
                    foreach (Respostas value in Lresp)
                    {
                        //para cada opção na lista verifica quem ocupa a posição de ordem que a opção selecionada passou a ocupar 
                        if (resp.ordem == value.ordem)
                        {
                            //carrega na variavel a opção que está na posição que a opção deslocada acima ocupará
                            resp = controle.pesquisaRespostaId(value.id);
                            //incrementa o valor de ordem desta opção, ocupa a posição da anterior na radio button list, e salva a alteração
                            resp.ordem++;
                            controle.atualizarDados();
                        }
                    }
                    //carrega a lista com a nova ordenação na variavel
                    Lresp = controle.pesquisaRespostaQuestão(perguntaId);
                    //carrega na radio button list essa lista
                    carregaRadioList(Lresp);
                    //mantem como opção selecionada a opção que subiu na lista, e que estava previamente a esta função selecionada
                    rblOpções.SelectedIndex = resp.ordem - 2;
                }
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros ao usuário
            catch { }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            //ao pressionar voltar o sistema tenta salvar o questionário aberto
            try
            {
                btnSalvarSair_Click(sender, e);
            }
            //caso não consiga tenta excluir
            catch
            {
                btnExcluir_Click(sender, e);
            }
            //e finalmente, chama a página de seleção
            finally
            {
                Response.Redirect("Usuario.aspx");
            }
        }

        //função para alterar a ordenação da opção selecionada para baixo
        protected void btnDownOrdem_Click(object sender, ImageClickEventArgs e)
        {
            try {
                //carrega na variavel a lista de opções para a resposta
                List<Respostas> Lresp = controle.pesquisaRespostaQuestão(perguntaId);
                //carrega na variavel a opção selecionada na radio button list
                resp = controle.pesquisaRespostaId(Convert.ToInt32(respostaTitulo));
                //verifica se a opção selecionada já não está no final da lista, não estando executa o conjunto de intruções abaixo
                if (resp.ordem < Lresp.Count)
                {
                    //incrementa em 2 valores o numero de ordem da questão selecionada e salva a alteração
                    resp.ordem = resp.ordem + 2;
                    controle.atualizarDados();
                    foreach (Respostas value in Lresp)
                    {
                        //para cada objeto na lista de opções verifica aquele que está na posição que o selecionado passou a ocupar
                        if (resp.ordem == value.ordem)
                        {
                            //carrega na variavel o objeto da lista que esta na posição que o selecionado passou a ocupar
                            resp = controle.pesquisaRespostaId(value.id);
                            //decrementa o valor de ordem deste objeto e salva a alteração
                            resp.ordem--;
                            controle.atualizarDados();
                        }
                    }
                    //carrega na variavel a lista alterada
                    Lresp = controle.pesquisaRespostaQuestão(perguntaId);
                    //atualiza o controle da radio button list com os novos valores de ordenação
                    carregaRadioList(Lresp);
                    //altera o valor selecionado para manter o item alterado como seleção
                    rblOpções.SelectedIndex = resp.ordem;
                }
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros ao usuário
            catch { }
        }   

    }
}