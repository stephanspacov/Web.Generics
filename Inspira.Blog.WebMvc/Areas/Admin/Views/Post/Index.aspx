<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inspira.Blog.WebMvc.Areas.Admin.ViewModels.Post.IndexViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Posts</h2>	
	<p>Lista de posts</p>

	<% using (Html.BeginForm(null, null, FormMethod.Get)) { %>
		<% Html.RenderPartial("Partial/Filter", Model); %>
		<%= Html.Grid(model => model.AllPosts) %>
	<% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BoxContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>