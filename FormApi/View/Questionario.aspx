<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Questionario.aspx.cs" Inherits="FormApi.View.Questionario" EnableSessionState="True" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <style type="text/css">
        .auto-style1:hover {
            background-color: #EEEEEE;
            color: #4ecf2f;
            border: 1px solid #C1C1C1;
        }

        .auto-style2:hover {
            background-color: #EEEEEE;
            color: #4ecf2f;
            border: 1px solid #C1C1C1;
        }
        
        .auto-style3 {
            height: 153px;
            width: 600px;
        }

        .auto-style4 {
            margin-top: 4px;
        }

        .auto-style1 {
            color: #009933;
            background-color: #d8d8d8;
            margin-left: 0px;
            margin-top: 12px;
        }

        .auto-style2 {
            color: #009933;
            background-color: #d8d8d8;
            margin-left: 15px;
        }
              

        .auto-style1:disabled{
            background-color: #c3c3c3;
            color: #808080;
            border: 1px solid #C1C1C1;
        }

        .auto-style2:disabled{
            background-color: #c3c3c3;
            color: #808080;
            border: 1px solid #C1C1C1;
        }              

        .auto-style5 {
            margin-top: 5px;
        }

        .auto-style6 {
            margin-left: 0px;
        }

        .auto-style7 {
            margin-top: 4px;
            margin-left: 15px;
        }
        .auto-style9 {
            color: #009933;
            background-color: #d8d8d8;
            margin-left: 117px;
            margin-top: 8px;
        }
        .auto-style10 {
            color: #009933;
            background-color: #d8d8d8;
            margin-left: 15px;
            margin-top: 6px;
        }

        .auto-style11 {
            color: #009933;
            background-color: #d8d8d8;
            margin-left: 138px;
        }

        .auto-style12 {
            color: #009933;
            background-color: #d8d8d8;
            margin-left: 0px;
            margin-top: 9px;
        }

        .auto-style14 {
            margin-top: 9px;
        }

        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="auto-style3">
                <asp:Panel ID="Panel1" runat="server" Height="140px" Width="650px">
                    &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Text="Titulo do Questionário:"></asp:Label>
                    <asp:TextBox ID="txtTitulo" runat="server" CssClass="auto-style4" Font-Bold="True" Font-Size="XX-Large" Height="50px" MaxLength="31" Width="650px"></asp:TextBox>
                    <asp:Panel ID="Panel2" runat="server" Height="17px">
                        <asp:Label ID="lblAlerta" runat="server" ForeColor="Red" Font-Bold="True" Font-Size="Small"></asp:Label>
                    </asp:Panel>
                    <asp:Panel ID="pnlButton" runat="server" Height="57px" HorizontalAlign="Center" Width="650px" ToolTip="inicia novo questionário">
                        <asp:Button ID="btnNovo" runat="server" CssClass="auto-style1" Font-Bold="True" OnClick="btnNovo_Click" Text="Novo" Width="115px" />
                        <asp:Button ID="btnSalvar" runat="server" CssClass="auto-style1" Font-Bold="True" OnClick="btnSalvar_Click" Text="Salvar" Visible="False" Width="115px" />
                        <asp:Button ID="btnEditar" runat="server" CssClass="auto-style2" Font-Bold="True" OnClick="btnEditar_Click" Text="Editar" Width="115px" Enabled="False" ToolTip="Edita titulo do questionário" />
                        <asp:Button ID="btnExcluir" runat="server" CssClass="auto-style2" Enabled="False" Font-Bold="True" OnClick="btnExcluir_Click" Text="Excluir" Width="115px" />
                        <asp:Button ID="btnAdicionar" runat="server" CssClass="auto-style2" Font-Bold="True" Text="Adic. Perguntas" Width="115px" Enabled="False" OnClick="btnAdicionar_Click" ToolTip="Adiciona perguntas ao questionário criado" />
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>
        <asp:Panel ID="pnlPerguntas" runat="server" Direction="LeftToRight" Height="135px" Width="693px" Visible="False" Font-Bold="True">
            &nbsp;&nbsp;&nbsp;&nbsp;Adicione o Titulo da Pergunta:<br />
            &nbsp;<asp:Label ID="lblN" runat="server" Font-Bold="True"></asp:Label>
            -
            <asp:TextBox ID="txtPergunta" runat="server" CssClass="auto-style5" Height="53px" MaxLength="70" Rows="3" TextMode="MultiLine" Width="617px" ValidateRequestMode="Enabled"></asp:TextBox>
            <br />
            <asp:CheckBox ID="chkObrigatorio" runat="server" Font-Size="Small" Text="Exigir Resposta?" ToolTip="Torna a resposta em branco inválida." />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblTipo" runat="server" Font-Size="Small" Text="Tipo de Resposta:"></asp:Label>
            &nbsp;<asp:DropDownList ID="ddlReposta" runat="server" CssClass="auto-style6" Height="23px" Width="293px" AutoPostBack="True" OnSelectedIndexChanged="ddlReposta_SelectedIndexChanged" TabIndex="-1" ToolTip="Lista - o usuário pode selecionar mais de uma opção.
Opção - o usuário deverá escolher uma opção.
Texto - o usuário poderá digitar um texto livre.
Data - o usuário deverá digitar uma data válida.
Número - o usuário deverá digitar um número valido.">
            </asp:DropDownList>
            <asp:Panel ID="pnlAlerta" runat="server" HorizontalAlign="Right" Visible="False" Width="644px">
                <asp:Label ID="lblAlertaResp" runat="server" Font-Bold="True" ForeColor="Red" Font-Size="Small"></asp:Label>
                &nbsp;&nbsp;
            </asp:Panel>
            </asp:Panel>
        <asp:Panel ID="pnlResposta" runat="server" Height="108px" Width="650px" Visible="False" Font-Bold="True">
            &nbsp;&nbsp;&nbsp;&nbsp; Adicione texto para a opção de resposta :<br />
            <asp:TextBox ID="txtOpção" runat="server" CssClass="auto-style7" Width="613px"></asp:TextBox>
            <br />
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblOpção" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Visible="False"></asp:Label>
            <br />
            <asp:Button ID="btnOpção" runat="server" CssClass="auto-style9" Font-Bold="True" OnClick="btnOpção_Click" Text="Adicionar Opção" Width="120px" />
            <asp:Button ID="btnCancOpção" runat="server" CssClass="auto-style11" Font-Bold="True" Text="Cancelar" Width="120px" OnClick="btnCancOpção_Click" />
            <asp:Button ID="btnSalvarPergunta" runat="server" CssClass="auto-style2" Font-Bold="True" Text="Salvar Pergunta" Width="120px" OnClick="btnAdcPergunta_Click" />
        </asp:Panel>
        <asp:Panel ID="pnlOpções" runat="server" Height="70px" Width="650px" Visible="False">
            <asp:RadioButtonList ID="rblOpções" runat="server" OnSelectedIndexChanged="rblOpções_SelectedIndexChanged" AutoPostBack="True" CssClass="auto-style14">
            </asp:RadioButtonList>
            <asp:Label ID="lblOrdem" runat="server" Font-Size="Small" Text="Ordenar Opção:" Visible="False"></asp:Label>
            <br />
            <asp:ImageButton ID="btnUpOrdem" runat="server" CssClass="auto-style2" Height="25px" ImageUrl="~/Image/uparrow.png" OnClick="btnUpOrdem_Click" Visible="False" Width="25px" />
            <asp:ImageButton ID="btnDownOrdem" runat="server" CssClass="auto-style2" Height="25px" ImageUrl="~/Image/downarrow.png" OnClick="btnDownOrdem_Click" Visible="False" Width="25px" />
            <asp:Button ID="btnRemovOpção" runat="server" CssClass="auto-style10" Font-Bold="True" Text="Remover Opção" Visible="False" Width="120px" OnClick="btnRemovOpção_Click" />
        </asp:Panel>
        <asp:Panel ID="pnlSair" runat="server" Height="38px" HorizontalAlign="Center" Visible="False" Width="650px">
            <asp:Button ID="btnSalvarSair" runat="server" Text="Salvar Question." CssClass="auto-style12" Font-Bold="True" Width="120px" OnClick="btnSalvarSair_Click" />
        </asp:Panel>
    </form>
</body>
</html>