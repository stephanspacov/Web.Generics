<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<HomeIndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Inspira - Blogs
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="block" id="block-text">
    <div class="content">
        <h2 class="title">Join us!</h2>
        <div class="inner">
            <div>
                <%= Html.ActionLink("Create your blog", "SignUp", "Account")  %>
            </div>

            <div id="all-blogs">
                <h3>All blogs</h3>
                <%= Html.List(Model.WebLogs, "{0} - {1}", w => w.Title, w => w.CreatedAt)%>
            </div>

            <div id="last-blogs">
                <h3>Last blogs</h3>
                <%= Html.List(Model.LastWebLogs, "{0} - {1}", w => w.Title, w => w.CreatedAt)%>
            </div>

            <div id="last-published-posts">
                <h3>Last published posts</h3>
                <%= Html.List(Model.LastPublishedPosts, "{0} - {1} (publicado em {2})", p => p.Title, p => p.CreatedAt, p => p.PublishedAt)%>
            </div>
        </div>
    </div>
</div>
</asp:Content>
