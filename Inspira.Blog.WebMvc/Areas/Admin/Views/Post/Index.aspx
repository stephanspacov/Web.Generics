<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inspira.Blog.WebMvc.Areas.Admin.ViewModels.Post.IndexViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Blogs</h2>	
	<p>Lista de blogs</p>

	<%= Html.Grid(model => model.AllPosts) %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BoxContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
