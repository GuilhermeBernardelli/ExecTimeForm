<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CreateApi.View.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 387px;            
            margin-top: 64px;
        }

.button{
    width: 110px;
    background-color: lightgray;
    color: forestgreen;
    font-weight: bold;
}

    </style>
</head>
<body style="height: 452px">
    <form id="form1" runat="server">
    <div class="auto-style1" align="center">
    
        <asp:Panel ID="Panel1" runat="server" Height="266px" HorizontalAlign="Center" Width="535px" BorderStyle="Outset">
            <br />
            <br />
            <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
            <br />
            <asp:Label ID="lblAlerta" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            <br />
            Registro:
            <asp:TextBox ID="txtRegistro" runat="server" Width="169px"></asp:TextBox>
            <br />
            <br />
            Senha:&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtSenha" runat="server" Width="169px" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" class="button" OnClick="btnLogin_Click"/>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCAC" runat="server" Text="Login com CAC" class="button"/>
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
