<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="CreateApi.View.Usuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
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
        .auto-style3 {
            color: #009933;
            background-color: #d8d8d8;
            margin-left: 15px;
            margin-top: 10px;
        }
    </style>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="pnlPrincipal" runat="server" Height="121px" Width="652px">
            Tela de seleção de usuários aptos a responder ao questionário:<br />
            <br />
            <asp:Label ID="lblQuest" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Button7" runat="server" CssClass="auto-style2" Font-Bold="True" Font-Italic="False" Text="Realizar inclusões" Width="130px" />
            <asp:Button ID="Button8" runat="server" CssClass="auto-style2" Font-Bold="True" Font-Italic="False" Text="Selec. Questionário" Width="130px" />
            <asp:Button ID="Button5" runat="server" CssClass="auto-style2" Font-Bold="True" Font-Italic="False" Text="Salvar Alterações" Width="130px" />
            <asp:Button ID="Button6" runat="server" CssClass="auto-style2" Font-Bold="True" Font-Italic="False" Text="Excluir Alterações" Width="130px" />
        </asp:Panel>
    
    </div>
        <asp:Panel ID="pnlPesquisaQuest" runat="server" Height="136px" Width="652px">
            Digite parte do titulo do Questionário<br />
            <asp:TextBox ID="TextBox2" runat="server" Width="488px"></asp:TextBox>
            <asp:Button ID="Button9" runat="server" CssClass="auto-style2" Font-Bold="True" Text="Pesquisar" Width="120px" />
            <br />
            <asp:RadioButtonList ID="RadioButtonList2" runat="server">
            </asp:RadioButtonList>
            <asp:Button ID="btnIncluir1" runat="server" CssClass="auto-style3" Font-Bold="True" Font-Italic="False" Text="Incluir Usuário" Width="120px" />
            <asp:Button ID="btnIncluir2" runat="server" CssClass="auto-style3" Font-Bold="True" Font-Italic="False" Text="Voltar" Width="120px" />
        </asp:Panel>
        <asp:Panel ID="pnlMetodo" runat="server" Height="63px" Width="652px">
            Selecione o método de inclusão de usuários:<br />
            <asp:Button ID="btnPesquisar" runat="server" CssClass="auto-style2" Font-Bold="True" Text="Por pesquisa" ToolTip="Pesquisa no banco de usuários cadastrados" Width="120px" />
            <asp:Button ID="Button2" runat="server" CssClass="auto-style2" Font-Bold="True" Text="Por registro" ToolTip="Inclusão pelo registro do usuário" Width="120px" />
        </asp:Panel>
        <asp:Panel ID="pnlPesquisa" runat="server" Height="136px" Width="652px" Visible="False">
            Digite parte do nome ou o registro do usuário<br />
            <asp:TextBox ID="TextBox1" runat="server" Width="488px"></asp:TextBox>
            <asp:Button ID="Button3" runat="server" CssClass="auto-style2" Font-Bold="True" Text="Pesquisar" Width="120px" />
            <br />
            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
            </asp:RadioButtonList>
            <asp:Button ID="btnIncluir" runat="server" CssClass="auto-style3" Font-Bold="True" Font-Italic="False" Text="Incluir Usuário" Width="120px" />
            <asp:Button ID="btnIncluir0" runat="server" CssClass="auto-style3" Font-Bold="True" Font-Italic="False" Text="Voltar" Width="120px" />
        </asp:Panel>
    </form>
</body>
</html>
