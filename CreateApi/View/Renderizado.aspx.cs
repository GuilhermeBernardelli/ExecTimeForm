using CreateApi.Control;
using CreateApi.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RenderApi.View
{
    public partial class Renderizado : System.Web.UI.Page
    {
        //variavel global referente ao id do questionário selecionado na página Selecao.aspx
        static int id;
        //variavel global referente ao usuário que realizou o login na página Index.aspx
        static int reg;
        /* variaveis globais, respectivamente, contador  para identificar a posição no questionário
         * contador que identifica o tamanho do questionário */
        static int count = 0, countMax;
        //variavel global booleana para identificar se o item pergunta foi respondido, pode haver obrigatoriedade do preenchimento
        static bool selecao = false;
        //variaveis referente aos formatos de dados da base
        static List<Perguntas> perg;
        static List<Respostas> resp;
        static List<Prenchimentos> preenchimentos;
        static Usuarios user;
        static Renderizar render; 
        static Prenchimentos valores = new Prenchimentos();
        //variaveis referente aos objetos a serem renderizados em tempo de execução
        static RadioButtonList radioResposta;
        static CheckBox chkResposta;        
        static Panel panelPergunta;
        static Panel panelResposta;
        //objeto para comunicação com a camada control
        Controle controle = new Controle();

        protected void Page_Load(object sender, EventArgs e)
        {            
            //executa as instruções no primeiro load da página
            if (!IsPostBack)
            {
                try
                {                 
                    //identifica o valor das variaveis de sessão
                    id = Convert.ToInt32(Session["Id_quest"]);
                    reg = Convert.ToInt32(Session["user"]);
                    //pesquisa na base o usuário e armazena na variavel global
                    user = controle.pesquisaUsuarioReg(reg);
                    //pesquisa na base o questionário, informação armazenada em variavel local
                    Questionarios quest = controle.pesquisaQuestionarioId(id);
                    //pesquisa na base o renderizador e armazena na variavel global
                    render = controle.pesquisaRenderizarIdUser(id, user.id);
                    //modifica os controles da view para personalização
                    lblID.Text = id.ToString();
                    lblTitulo.Text = quest.nome;
                    //identifica se o usuário é público, não sendo segue as instruções do if a seguir
                    if (reg != 0)
                    {
                        //mais uma vez personaliza os controles da view
                        lblUser.Text = user.registro.ToString() + " - " + user.nome;
                        //pesquisa a lista das perguntas do questionário a ser renderizado
                        perg = controle.pesquisaPerguntaQuestionario(id);
                        //identifica se existe o formulario já preparado para preenchimento na base de dados, tabela Prenchimentos
                        preenchimentos = controle.pesquisaPreenchimento_render_user(render.id, lblUser.Text);
                        if (preenchimentos.Count == 0)
                        {
                            //não identificando, executa Stored Procedure que preenche todas as questões na tabela Prenchimentos com valores 0
                            controle.construtor_formulario(id, render.id, lblUser.Text);
                        }
                        //atribui a variavel o valor da quantidade de questões
                        countMax = perg.Count;
                        //modifica os controles da view
                        pnlButton.Visible = true;
                        //chama função para renderizar a pergunta em tempo de execução
                        posicionaPergunta();
                    }
                    //se o usuário for público condiciona ele ao preenchimento do campo texto nome do usuário
                    else
                    {
                        // modifica os controles da view
                        lblUser.Text = "Insira seu nome:";
                        txtUser.Visible = true;
                        btnIniciar.Visible = true;
                    }
                }
                //instruções circundadas com try, catch para evitar a exibição de possíveis erros
                catch { }

            }
            //a cada ação de postback da pagina carrega novamente a pergunta em tela
            else
            {
                posicionaPergunta();
            }            

        }
        //função do botão iniciar, somente visible = true, se o usuário for anônimo
        protected void btnIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                //verifica se o usuário preencheu o campo de nome
                if (txtUser.Text != "")
                {
                    //modifica os controles da view
                    btnIniciar.Visible = false;
                    pnlButton.Visible = true;
                    lblAviso.Visible = false;
                    txtUser.Visible = false;
                    //personaliza os controles
                    lblUser.Text = txtUser.Text;
                    //pesquisa a lista das perguntas do questionário a ser renderizado
                    perg = controle.pesquisaPerguntaQuestionario(id);
                    //atribui a variavel o valor da quantidade de questões
                    countMax = perg.Count;
                    //identifica se existe o formulario já preparado para preenchimento na base de dados, tabela Prenchimentos
                    if (controle.pesquisaPreenchimento_render_user(render.id, lblUser.Text).Count == 0)
                    {
                        //não identificando, executa Stored Procedure que preenche todas as questões na tabela Prenchimentos com valores 0
                        controle.construtor_formulario(id, render.id, lblUser.Text);
                    }
                    //chama função para renderizar a pergunta em tempo de execução
                    posicionaPergunta();
                }
                //bloqueia a execução até o preenchimento do campo nome
                else
                {
                    lblAviso.Visible = true;
                    lblAviso.Text = "* O campo nome deve ser preenchido !!!";
                }
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }
        }
        //função para renderizar as perguntas em tempo de execução
        public void posicionaPergunta()
        {            
            //atribui o valor para a variavel como false = não respondido
            selecao = false;
            try
            {                                 
                //adiciona ao painel principal um parágrafo
                pnlPrincipal.Controls.Add(new LiteralControl("<p></p>"));
                //Remove qualque instancia de panel pergunta adicionada ao panel principal
                pnlPrincipal.Controls.Remove(panelPergunta);
                //cria um novo panel
                panelPergunta = new Panel
                {
                    //atribui uma id a este panel
                    ID = "pergunta_" + perg[count].id.ToString()
                };
                //cria uma nova label
                Label labelPergunta = new Label();
                //define o texto da label criada como sendo o numero e titulo da questão atual = count
                labelPergunta.Text = perg[count].numero.ToString() + " - " + perg[count].titulo;
                //adiciona no panel principal o panel criado
                pnlPrincipal.Controls.Add(panelPergunta);
                //adiciona no panel criado a label criada
                panelPergunta.Controls.Add(labelPergunta);
                //adiciona ao panel principal um novo parágrafo
                panelPergunta.Controls.Add(new LiteralControl("<br>"));

                //identifica o tipo de resposta atribuido a esta pergunta
                if (perg[count].tipo == 1)
                {
                    //carrega os elementos no caso do tipo ser uma radio button list
                    carregarRadioButtonList();                    
                }

                else if (perg[count].tipo == 2)
                {
                    //carrega os elementos no caso do tipo ser uma checkbox list
                    carregarCheckList();
                }

                else if (perg[count].tipo == 3)
                {
                    //carrega os elementos no caso do tipo ser um textbox
                    carregarTextBox();
                }

                else if (perg[count].tipo == 4)
                {
                    //carrega os elementos no caso do tipo ser um textbox com formato date
                    carregarDateTextBox();
                }

                else if (perg[count].tipo == 5)
                {
                    //carrega os elementos no caso do tipo ser um textbox com formato number
                    carregarNumberTextBox();
                }
                //caso seja adicionado um tipo a base que não coincide com os elementos do render
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('A pergunta " + perg[count].id + " possuí um formato de resposta não definido no escopo, alterar linha 135 do arquivo Renderizado.aspx.cs');", true);
                }
                
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }

        }               
        //função para construção de resposta no formato radio button list
        private void carregarRadioButtonList()
        {
            try
            {
                //pesquisa a lista de respostas da questão atual e atribui ela a variavel
                resp = controle.pesquisaRespostaQuestão(perg[count].id);
                //limpa o panel pergunta caso haja um panel resposta neste
                panelPergunta.Controls.Remove(panelResposta);
                //cria um novo panel resposta
                panelResposta = new Panel();
                //adiciona ao panel pergunta este novo panel resposta
                panelPergunta.Controls.Add(panelResposta);
                //cria uma radio button list
                radioResposta = new RadioButtonList
                {
                    //atribui a este alguns elementos
                    ID = perg[count].id.ToString(),
                    AutoPostBack = true
                };
                //alimenta esta radio button list com a lista de respostas
                radioResposta.DataSource = resp;
                radioResposta.DataTextField = "resposta";
                radioResposta.DataValueField = "id";
                radioResposta.DataBind();
                //adiciona a esta lista a função do evento de seleção
                radioResposta.Attributes.Add("OnSelectIndexChanged", "radioResposta_SelectedIndexChanged");
                radioResposta.SelectedIndexChanged += new EventHandler(radioResposta_SelectedIndexChanged);

                for (int i = 0; i < radioResposta.Items.Count; i++)
                {
                    //para cada elemento atribui o texto da resposta correspondente na lista
                    string opção = resp[i].resposta;
                    radioResposta.Items[i].Text = opção;
                }

                for (int i = 0; i < radioResposta.Items.Count; i++)
                {
                    //busca na base de dados os preenchimentos para a questão do referido render
                    valores = controle.pesquisaPreenchimentoUserName(radioResposta.Items[i].Text, lblUser.Text);
                    //para cada resposta verifica se existe algum valor preenchido na tabela de preenchimentos, onde preenchido = 1
                    if (valores.resposta == 1)
                    {
                        //caso haja algum valor preenchido altera o estado deste para selected
                        radioResposta.Items[i].Selected = true;
                        //muda o estado da variavel para true, true = respondida
                        selecao = true;
                    }
                }
                //adiciona no panel resposta o controle criado com as respostas
                panelResposta.Controls.Add(radioResposta);
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }
        }
        //função para construção de resposta no formato checkbox list
        private void carregarCheckList()
        {
            try
            {
                //pesquisa as opções de resposta para a pergunta
                resp = controle.pesquisaRespostaQuestão(perg[count].id);
                //limpa o panel pergunta
                panelPergunta.Controls.Remove(panelResposta);
                //cria um novo panel resposta e adiciona este ao panel pergunta
                panelResposta = new Panel();
                panelPergunta.Controls.Add(panelResposta);

                for (int i = 0; i < resp.Count; i++)
                {
                    //cria um novo checkbox para cada opção de resposta que exista para esta pergunta 
                    chkResposta = new CheckBox
                    {
                        ID = perg[count].id.ToString() + i,
                        AutoPostBack = true
                    };
                    //adiciona a estes elementos a função do evento de mudança de estado
                    chkResposta.Attributes.Add("OnCheckedChanged", "chkResposta_CheckedChanged");
                    chkResposta.CheckedChanged += new EventHandler(chkResposta_CheckedChanged);
                    //atribui o texto do elemento
                    chkResposta.Text = resp[i].resposta;
                    panelResposta.Controls.Add(chkResposta);
                    //busca na base de dados os preenchimentos para a questão do referido render
                    valores = controle.pesquisaPreenchimentoUserName(resp[i].resposta, lblUser.Text);
                    //verifica quais elementos estão ou não selecionados
                    if (valores.resposta == 1)
                    {
                        //altera o estado do elemento ao carregar
                        chkResposta.Checked = true;
                        //altera o valor da variavel de selecao para true
                        selecao = true;
                    }
                }
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }
        }
        //função para construção de resposta no formato textbox simples
        private void carregarTextBox()
        {
            try
            {
                //cria um novo panel resposta
                panelResposta = new Panel();
                //adiciona este panel no panel pergunta
                panelPergunta.Controls.Add(panelResposta);
                //cria uma textbox
                TextBox txtResposta = new TextBox
                {
                    ID = perg[count].id.ToString(),
                    AutoPostBack = true,
                    MaxLength = 150,
                    Width = 450
                };
                //adiciona atributo para o evento de alteração no conteudo do textbox
                txtResposta.Attributes.Add("OnTextChanged", "txtResposta_TextChanged");
                txtResposta.TextChanged += new EventHandler(txtResposta_TextChanged);
                //pesquisa se esse campo texto já foi respondido por este usuário           
                valores = controle.pesquisaPreenchimento_perg_userNome(perg[count].id, lblUser.Text);
                //se campo já respondido anteriormente, renderiza a resposta junto ao campo
                if (valores.resposta == 1)
                {
                    txtResposta.Text = valores.valor_resposta;
                    selecao = true;
                }
                //adiciona o campo no panel resposta
                panelResposta.Controls.Add(txtResposta);
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }
        }
        //função para construção de resposta no formato textbox date (não funciona no IE 10 e anteriores)
        private void carregarDateTextBox()
        {
            try
            {
                //cria um novo panel resposta
                panelResposta = new Panel();
                //adiciona este panel no panel pergunta
                panelPergunta.Controls.Add(panelResposta);
                //cria uma textbox
                TextBox dataResposta = new TextBox
                {
                    ID = perg[count].id.ToString(),
                    TextMode = TextBoxMode.Date,
                    Width = 150,
                    AutoPostBack = true
                };
                //adiciona atributo para o evento de alteração no conteudo do textbox
                dataResposta.Attributes.Add("OnTextChanged", "txtResposta_TextChanged");
                dataResposta.TextChanged += new EventHandler(txtResposta_TextChanged);
                //pesquisa se esse campo texto já foi respondido por este usuário
                valores = controle.pesquisaPreenchimento_perg_userNome(perg[count].id, lblUser.Text);
                //se campo já respondido anteriormente, renderiza a resposta junto ao campo
                if (valores.resposta == 1)
                {
                    dataResposta.Text = valores.valor_resposta;
                    selecao = true;
                }
                //adiciona o campo no panel resposta
                panelResposta.Controls.Add(dataResposta);
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }
        }
        //função para construção de resposta no formato textbox number (não funciona no IE 10 e anteriores)
        private void carregarNumberTextBox()
        {
            try
            {
                //cria um novo panel resposta
                panelResposta = new Panel();
                //adiciona este panel no panel pergunta
                panelPergunta.Controls.Add(panelResposta);
                //cria uma textbox
                TextBox numResposta = new TextBox
                {
                    ID = perg[count].id.ToString(),
                    Width = 65,
                    MaxLength = 6,
                    TextMode = TextBoxMode.Number,
                    AutoPostBack = true
                };
                //adiciona atributo para o evento de alteração no conteudo do textbox
                numResposta.Attributes.Add("OnTextChanged", "txtResposta_TextChanged");
                numResposta.TextChanged += new EventHandler(txtResposta_TextChanged);
                //pesquisa se esse campo texto já foi respondido por este usuário
                valores = controle.pesquisaPreenchimento_perg_userNome(perg[count].id, lblUser.Text);
                //se campo já respondido anteriormente, renderiza a resposta junto ao campo
                if (valores.resposta == 1)
                {
                    numResposta.Text = valores.valor_resposta;
                    selecao = true;
                }
                //adiciona o campo no panel resposta
                panelResposta.Controls.Add(numResposta);
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }
        }
        //evento adicionado aos controles tipo checkbox criados
        private void chkResposta_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //atribui a variavel caixa o valor do sender tipificando antes o objeto como checkbox
                CheckBox caixa = sender as CheckBox;
                //pesquisa o estado da resposta na base de preenchimentos
                valores = controle.pesquisaPreenchimentoUserName(caixa.Text, lblUser.Text);

                if (caixa.Checked)
                {
                    //se o objeto estiver marcado executa as alterações
                    valores.resposta = 1;
                    selecao = true;
                    controle.atualizarDados();
                }
                else
                {
                    //se o objeto estiver desmarcado executa as alterações
                    valores.resposta = 0;
                    controle.atualizarDados();
                }
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }
        }
        //evento adicionado aos controles tipo radio button list criados
        protected void radioResposta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //atribui as variaveis, respectivamente, o valor de respondido, e o texto do item selecionado
                selecao = true;
                string itemSelecionado = radioResposta.SelectedItem.Text;
                //verifica todos os elementos da resposta
                for (int i = 0; i < radioResposta.Items.Count; i++)
                {
                    if (itemSelecionado.Equals(radioResposta.Items[i].Text))
                    {
                        //altera o estado do item selecionado                   
                        valores = controle.pesquisaPreenchimentoUserName(radioResposta.Items[i].Text, lblUser.Text);
                        valores.resposta = 1;
                        controle.atualizarDados();
                    }
                    else
                    {
                        //altera o estado dos itens não selecionados
                        valores = controle.pesquisaPreenchimentoUserName(radioResposta.Items[i].Text, lblUser.Text);
                        valores.resposta = 0;
                    }
                }
                //salva as alterações
                controle.atualizarDados();
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }
        }
        //evento adicionado aos controles tipo textbox criados
        private void txtResposta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //atribui a variavel text o valor do sender tipificando antes o objeto como textbox          
                TextBox text = sender as TextBox;
                //pesquisa o estado da resposta na base de preenchimentos
                valores = controle.pesquisaPreenchimento_perg_userNome(perg[count].id, lblUser.Text);
                //atribui o valor da resposta            
                valores.valor_resposta = text.Text;
                if (valores.valor_resposta.Equals(""))
                {
                    //atribui a resposta o valor de não respondida caso o campo esteja em branco
                    selecao = false;
                    valores.resposta = 0;
                }
                else
                {
                    //atribui as variaveis a condição de respondida
                    selecao = true;
                    valores.resposta = 1;
                }
                //salva as alterações
                controle.atualizarDados();
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }
        }
        //função evento do botão salvar
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                //cria uma nova label
                Label lblAlerta = new Label()
                {
                    ForeColor = System.Drawing.Color.Red,
                    Text = "Resposta obrigatória"
                };
                //adiciona esta label ao panel pergunta
                panelPergunta.Controls.Add(lblAlerta);
                //cria uma variavel para contar as respostas com valor válido
                int respostas = 0;
                for (int i = 0; i < countMax; i++)
                {
                    //verifica a cada pergunta a obrigatoriedade de resposta
                    if (perg[i].obrigatorio)
                    {
                        //verifica se a pergunta de resposta obrigatória é do tipo radio button
                        if (perg[i].tipo == 1)
                        {
                            //armazena na variavel 
                            resp = controle.pesquisaRespostaQuestão(perg[i].id);
                            foreach (Respostas value in resp)
                            {
                                //para cada resposta possivel verifica se o valor esta selecionado
                                valores = controle.pesquisaPreenchimentoUserName(value.resposta, lblUser.Text);
                                if (valores.resposta == 1)
                                {
                                    //se localizar um valor marcado incrementa as respostas validas
                                    respostas++;
                                }
                            }
                        }
                        //verifica se a pergunta de resposta obrigatória é do tipo checkbox
                        else if (perg[i].tipo == 2)
                        {
                            //cria variavel local para verificar se algum box está selecionado, valor padrão false
                            bool testeChk = false;
                            //armazena todas as opções de resposta para a pergunta
                            resp = controle.pesquisaRespostaQuestão(perg[i].id);
                            foreach (Respostas value in resp)
                            {
                                //para cada valor de resposta verifica se a caixa esta marcada
                                valores = controle.pesquisaPreenchimentoUserName(value.resposta, lblUser.Text);
                                if (valores.resposta == 1)
                                {
                                    //identificando ao menos uma caixa como marcada altera o valor da variavel de verificação
                                    testeChk = true;
                                }
                            }
                            if (testeChk)
                            {
                                //sendo o valor da variavel de verificação verdadeiro incrementa as respostas válidas
                                respostas++;
                            }
                        }
                        //todas as respostas do tipo texto entraram nesta condição
                        else
                        {
                            //persquisa o valor do preenchimento para a resposta
                            valores = controle.pesquisaPreenchimento_perg_userNome(perg[i].id, lblUser.Text);
                            //verifica se ela esta respondida e em caso positivo incrementa as respostas válidas
                            if (valores.resposta == 1)
                            {
                                respostas++;
                            }
                        }

                    }
                    //resposta não obrigatória
                    else
                    {
                        //se a resposta é não obrigatória ela em branco é uma resposta valida, logo a variavel respostas é incrementada
                        respostas++;
                    }
                }
                //verifica se todas as respostas possuem respostas válidas segundo seus critérios individuais
                if (respostas == countMax)
                {
                    //atribui true a variavel
                    selecao = true;
                }
                //se houver uma ou mais respostas obrigatórias em branco
                else
                {
                    selecao = false;
                }
                //verifica o estado da variavel que avalia o preenchimento das questões
                if (selecao)
                {
                    try
                    {
                        //salva os preenchimentos e informa o sucesso ao usuário
                        controle.atualizarDados();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('As suas respostas foram salvas, obrigado pela colaboração');", true);
                        //limpa e abandona o estado da sessão
                        Session.Clear();
                        Session.Abandon();
                        //reinicia o contador de perguntas
                        count = 0;
                        //encaminha novamente a tela de login
                        Response.Redirect("Index.aspx");
                    }
                    catch
                    {
                        //trata de forma generica qualquer exceção e envia mensagem de erro ao usuário
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Por algum motivo não foi possivel salvar as suas respostas');", true);
                    }
                }

                else
                {
                    //na impossibilidade de encerrar a aplicação pela existência de resposta obrigatórias vazias informa o usuário
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Existe(m) " + (countMax - respostas).ToString() + " perguntas obrigatórias sem resposta');", true);
                }
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }
        }
        //função do botão para avançar perguntas
        protected void btnRight_Click(object sender, ImageClickEventArgs e)
        {
            try
            {             
                //cria uma label
                Label lblAlerta = new Label()
                {
                    ForeColor = System.Drawing.Color.Red,
                    Text = "Resposta obrigatória"
                };
                //adiciona essa label ao panel de pergunta
                panelPergunta.Controls.Add(lblAlerta);
                //modifica os controles da view
                txtUser.Visible = false;
                lblAviso.Visible = false;
                //verifica se a pergunta atual é de resposta obrigatória e se já está respondida ou de resposta não obrigatória
                if ((perg[count].obrigatorio && selecao) || (!perg[count].obrigatorio))
                {
                    //altera o estado da variavel seleção
                    selecao = false;
                    //modifica os controles da view
                    lblAlerta.Visible = false;
                    //remove a pergunta atual do panel principal
                    pnlPrincipal.Controls.Remove(panelPergunta);
                    //verifica se a pergunta atual já não é a última
                    if (count >= countMax - 1)
                    {
                        //sendo a última bloqueia sua posição
                        count = countMax - 1;
                        posicionaPergunta();
                    }

                    else
                    {
                        //não sendo avança para a próxima pergunta
                        count++;
                        posicionaPergunta();
                    }
                }
                //sendo a pergunta de resposta obrigatória e não havendo resposta válida é exibido uma label com alerta 
                else
                {
                    lblAlerta.Visible = true;
                }
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }
        }
        //função do botão para voltar a pergunta anterior
        protected void btnLeft_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //cria uma label
                Label lblAlerta = new Label()
                {
                    ForeColor = System.Drawing.Color.Red,
                    Text = "Resposta obrigatória"
                };
                //adiciona essa label ao panel de pergunta
                panelPergunta.Controls.Add(lblAlerta);
                //modifica os controles da view
                txtUser.Visible = false;
                lblAviso.Visible = false;
                //verifica se a pergunta atual é de resposta obrigatória e se já está respondida ou de resposta não obrigatória
                if ((perg[count].obrigatorio && selecao) || (!perg[count].obrigatorio))
                {
                    //altera o estado da variavel seleção
                    selecao = false;
                    //modifica os controles da view
                    lblAlerta.Visible = false;
                    //remove a pergunta atual do panel principal
                    pnlPrincipal.Controls.Remove(panelPergunta);
                    //verifica se a pergunta atual já não é a primeira
                    if (count == 0)
                    {
                        //sendo a primeira bloqueia sua posição
                        posicionaPergunta();
                    }

                    else
                    {
                        //não sendo retrocede para a pergunta anterior
                        count--;
                        posicionaPergunta();

                    }
                }
                //sendo a pergunta de resposta obrigatória e não havendo resposta válida é exibido uma label com alerta 
                else
                {
                    lblAlerta.Visible = true;
                }
            }
            //instruções circundadas com try, catch para evitar a exibição de possíveis erros
            catch { }
        }
               
    }
}