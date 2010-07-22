<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inspira - Blog
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Post publicado</h2>

	<p>Post publicado com sucesso.</p>

	<p><a target="_blank" href="../../Post/Details/<%=ViewData["ID"] %>">Clique aqui para ver</a></p>
	<p><a href="../">Voltar para o gerenciador</a></p>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BoxContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
