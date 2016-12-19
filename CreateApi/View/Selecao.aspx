<%@ Page Title="" Language="C#" MasterPageFile="~/View/PaginaBase.Master" AutoEventWireup="true" CodeBehind="Selecao.aspx.cs" Inherits="RenderApi.View.Selecao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            margin-left: 0px;
            margin-top: 0px;
            margin-bottom: 9px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlPrincipal" runat="server" Height="169px" HorizontalAlign="Left">
        Acesso do usuário<br />
        <br />
        <asp:Label ID="lblUser" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblMensagem" runat="server"></asp:Label>
        <br /> <br />
        <asp:RadioButtonList ID="rblQuestionario" runat="server" AutoPostBack="True" CellPadding="0" CellSpacing="0" CssClass="auto-style2" RepeatLayout="Flow" OnSelectedIndexChanged="rblQuestionario_SelectedIndexChanged" Visible="False">
        </asp:RadioButtonList>

    </asp:Panel>
</asp:Content>
