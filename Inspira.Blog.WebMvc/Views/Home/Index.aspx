<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<HomeIndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Inspira - Blogs
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="all-blogs">
        <h2>All blogs</h2>
        <%= Html.List(Model.WebLogs, "{0} - {1}", w => w.Title, w => w.CreatedAt)%>
    </div>

    <div id="last-blogs">
        <h2>Last blogs</h2>
        <%= Html.List(Model.LastWebLogs, "{0} - {1}", w => w.Title, w => w.CreatedAt)%>
    </div>

    <div id="last-published-posts">
        <h2>Last published posts</h2>
        <%= Html.List(Model.LastPublishedPosts, "{0} - {1} (publicado em {2})", p => p.Title, p => p.CreatedAt, p => p.PublishedAt)%>
    </div>
</asp:Content>
