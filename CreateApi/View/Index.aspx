<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CreateApi.View.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 616px;            
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
<body style="height: 685px">
    <form id="form1" runat="server">
    <div class="auto-style1" align="center">
    
        <asp:Panel ID="pnlLogin" runat="server" Height="266px" HorizontalAlign="Center" Width="535px" BorderStyle="Solid" BorderWidth="1px">
            <br />
            <br />
            <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
            <br />
            <asp:Label ID="lblAlerta" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            <br />
            Registro:
            <asp:TextBox ID="txtRegistro" runat="server" Width="169px" AutoCompleteType="Disabled" TextMode="Number" ValidateRequestMode="Enabled"></asp:TextBox>
            <br />
            <br />
            Senha:&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtSenha" runat="server" Width="169px" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:CheckBox ID="chkAdministrador" runat="server" Text="Administrador : " TextAlign="Left" />
            <br />
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" class="button" OnClick="btnLogin_Click" Width="130px"/>
            &nbsp;&nbsp;
            <asp:Button ID="btnCAC" runat="server" Text="Login com CAC" class="button" Width="130px"/>
            &nbsp;&nbsp;
            <asp:Button ID="btnPublico" runat="server" class="button" OnClick="btnPublico_Click" Text="Quest. Públicos" Width="130px" />
            <br />
            <br />
        </asp:Panel>
    
        <asp:Panel ID="pnlNovo" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False" Font-Size="Medium" Height="270px" Visible="False" Width="540px">
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" Text="Defina a senha para este usuário"></asp:Label>
            <br />
            <br />
            <br />
            Nova Senha :
            <asp:TextBox ID="txtNovo" runat="server" TextMode="Password" Width="167px"></asp:TextBox>
            <br />
            <br />
            Confirmação :
            <asp:TextBox ID="txtConfirma" runat="server" TextMode="Password" Width="167px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnSalvar" runat="server" CssClass="button" OnClick="btnSalvar_Click" Text="Salvar" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancelar" runat="server" CssClass="button" Text="Cancelar" OnClick="btnCancelar_Click" />
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
