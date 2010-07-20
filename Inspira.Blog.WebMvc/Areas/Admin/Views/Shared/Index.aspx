<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Web.Generics.UserInterface.GenericViewModel<Inspira.Blog.DomainModel.WebLog>>" %>
<%@ Import Namespace="Web.Generics.UserInterface.HtmlHelpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id='Content'>  
    <%= Html.Grid(model => model.DefaultGrid) %>
</div>      
</asp:Content>
