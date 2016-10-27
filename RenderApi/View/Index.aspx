<%@ Page Title="" Language="C#" MasterPageFile="~/View/PaginaBase.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="RenderApi.View.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/StyleSheet.css" rel="stylesheet" />
    <asp:Panel ID="pnlPrincipal" runat="server" Height="250px" HorizontalAlign="Center">
        <br />
        <br />
        <asp:Label ID="lblMensagem" runat="server"></asp:Label>
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
</asp:Content>
