<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="CreateApi.View.Usuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript"> 
    function goBack()
    {
        window.history.back();
    }
    function data()
    {
        var data;
        do {
            data = prompt("digite uma data valida:");
        } while (data == null);
    }
    </script> 

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
        .auto-style5 {
            margin-top: 11px;
        }
    </style>
    
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="pnlPrincipal" runat="server" Height="162px" Width="652px" Visible="False">
            <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblTipo" runat="server" Font-Size="X-Large" Font-Underline="True"></asp:Label>
            <br />
            <asp:CheckBoxList ID="chkSelecionados" runat="server" CssClass="auto-style5">
            </asp:CheckBoxList>
            <br />
            <asp:Button ID="btnInclude" runat="server" CssClass="auto-style2" Font-Bold="True" Font-Italic="False" Text="Realizar inclusões" Width="130px" OnClick="btnInclude_Click" />
            <asp:Button ID="btnAlterar" runat="server" CssClass="auto-style2" Font-Bold="True" Font-Italic="False" Width="130px" OnClick="btnAlterar_Click" />
            <asp:Button ID="btnSalvar" runat="server" CssClass="auto-style2" Font-Bold="True" Font-Italic="False" Text="Salvar Alterações" Width="130px" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnCancela" runat="server" CssClass="auto-style2" Font-Bold="True" Font-Italic="False" Text="Cancelar" Width="130px" OnClick="btnCancela_Click" />
        </asp:Panel>
    
    </div>
        <asp:Panel ID="pnlMetodo" runat="server" Height="92px" Width="652px">
            Escolha um médodo de inclusão:<br />
            <br />
            <asp:Button ID="btnQuestUser" runat="server" CssClass="auto-style2" Font-Bold="True" OnClick="btnQuestUser_Click" Text="Quest. ao usuár." Width="120px" />
            <asp:Button ID="btnUserQuest" runat="server" CssClass="auto-style2" Font-Bold="True" OnClick="btnUserQuest_Click" Text="Usuár. ao quest." Width="120px" />
        </asp:Panel>
        <asp:Panel ID="pnlPesquisaUser" runat="server" Height="136px" Width="652px" Visible="False">
            Digite parte do nome ou o registro do usuário<br />
            <asp:TextBox ID="txtUser" runat="server" Width="488px"></asp:TextBox>
            <asp:Button ID="btnPesqUser" runat="server" CssClass="auto-style2" Font-Bold="True" Text="Pesquisar" Width="120px" OnClick="btnPesqUser_Click" />
            <br />
            <asp:Label ID="lblAvisoUser" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
            <asp:RadioButtonList ID="rblUser" runat="server" OnSelectedIndexChanged="rblUser_SelectedIndexChanged" AutoPostBack="True">
            </asp:RadioButtonList>
            <asp:Panel ID="pnlButtonUser" runat="server">
                <asp:Button ID="btnVoltaUser" runat="server" CssClass="auto-style3" Font-Bold="True" Font-Italic="False" Text="Voltar" Width="120px" OnClientClick="goBack()" OnClick="btnVoltaUser_Click"/>
            </asp:Panel>
            
        </asp:Panel>
        <asp:Panel ID="pnlPesquisaQuest" runat="server" Height="136px" Width="652px" Visible="False">
            Digite parte do titulo do Questionário<br />
            <asp:TextBox ID="txtQuest" runat="server" Width="488px"></asp:TextBox>
            <asp:Button ID="btnPesqQuest" runat="server" CssClass="auto-style2" Font-Bold="True" Text="Pesquisar" Width="120px" OnClick="btnPesqQuest_Click" />
            <br />
            <asp:Label ID="lblAvisoQuest" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButtonList ID="rblQuest" runat="server" OnSelectedIndexChanged="rblQuest_SelectedIndexChanged" AutoPostBack="True">
            </asp:RadioButtonList>
            <asp:Panel ID="pnlButtonQuest" runat="server">
                <asp:Button ID="btnVoltaQuest" runat="server" CssClass="auto-style3" Font-Bold="True" Font-Italic="False" Text="Voltar" Width="120px"  OnClientClick="goBack()" OnClick="btnVoltaQuest_Click"/>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="pnlDataValidade" runat="server" Height="77px" Visible="False" Width="651px">
            Digite ou selecione a data de validade do Questionário<br />
            <br />
            <asp:TextBox ID="txtData" runat="server" AutoPostBack="True" CausesValidation="True" TextMode="Date" ValidateRequestMode="Enabled" Width="162px"></asp:TextBox>
            <asp:Button ID="btnData" runat="server" CssClass="auto-style2" Font-Bold="True" OnClick="btnData_Click" Text="Confirma" Width="120px" />
        </asp:Panel>
    </form>
</body>
</html>
