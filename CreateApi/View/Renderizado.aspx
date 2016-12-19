<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Renderizado.aspx.cs" Inherits="RenderApi.View.Renderizado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../CSS/StyleSheet.css" rel="stylesheet" />
    
    <style type="text/css">
        #panelPergunta{ 
            position:absolute;
            top: 0;
            left: 0;
            background-color: red;
        }
        #panelResposta{
            position:static;
            top: 0;
            left: 0;
        }
        #linha{
            position:static;
            width: 100%;
        }
    </style>
    
</head>
<body style="height: 206px">
    <form id="form1" runat="server" submitdisabledcontrols="True" visible="True">
    <div>
    
        <asp:Panel ID="pnlTitulo" runat="server" Height="134px">
            Questionário ID
            <asp:Label ID="lblID" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblTitulo" runat="server" Font-Size="XX-Large"></asp:Label>
            <br />
            <asp:Label ID="lblUser" runat="server" Font-Size="Large"></asp:Label>
            <br />
            <asp:TextBox runat="server" Visible="False" Width="468px" ID="txtUser" Font-Bold="True" Font-Size="Large" ></asp:TextBox>
            <asp:Label ID="lblAviso" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            <br />
            &nbsp;&nbsp;
            <asp:Button ID="btnIniciar" runat="server" CssClass="button" OnClick="btnIniciar_Click" Text="Iniciar" Visible="False" />
        </asp:Panel>
    
    </div>
        <asp:Panel ID="pnlButton" runat="server" Height="45px" Visible="False">
            &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnLeft" runat="server" Height="36px" ImageUrl="~/Image/leftArrow.png" OnClick="btnLeft_Click" Width="36px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnRight" runat="server" Height="36px" ImageUrl="~/Image/rightArrow.png" OnClick="btnRight_Click" Width="36px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar Resp." class="button" OnClick="btnSalvar_Click" />
        </asp:Panel>
        <asp:Panel ID="pnlPrincipal" runat="server" Height="58px">
        </asp:Panel>
    </form>
</body>
</html>
