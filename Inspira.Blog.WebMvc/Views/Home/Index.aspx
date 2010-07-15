<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<HomeIndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Inspira - Blogs
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="block" id="block-text">
    <div class="secondary-navigation">
        <ul class="wat-cf">
            <li class="active first"><a href="#block-text">Text</a></li>
            <li><a href="#block-tables">Tables</a></li>
            <li><a href="#block-forms">Forms</a></li>
            <li><a href="#block-messages">Messages</a></li>
            <li><a href="#block-forms-2">2 Columns Forms</a></li>
            <li><a href="#block-lists">Lists</a></li>
        </ul>
    </div>
    <div class="content">
        <h2 class="title">Dashboard</h2>
        <div class="inner">
            <div>
                <p class="first">
                Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. <span class="hightlight">Excepteur sint occaecat cupidatat non proident</span>, sunt in culpa qui officia deserunt mollit anim id est laborum.
                </p>
                <p> <span class="small">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore</span></p>
                <p> <span class="gray">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore</span></p>
                <hr />
                <p>
                Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. <span class="hightlight">Excepteur sint occaecat cupidatat non proident</span>, sunt in culpa qui officia deserunt mollit anim id est laborum.
                </p>
            </div>

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
        </div>
    </div>
</div>
</asp:Content>
