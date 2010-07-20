<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inspira.Blog.WebMvc.ViewModels.Posts.IndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="block" id="block-text">
        <div class="content">
            <h2 class="title">
                Título do blog</h2>
            <div class="inner">
                <%foreach (Post p in Model.PostsPublicados)
                  {%>
                <div class="post">
                    <h3>
                        <%= Html.ActionLink(p.Title, "Details", new { ID = p.ID }) %>
                    </h3>
                    <span class="gray">
                        <%=p.PublishedAt%></span>
                    <p>
                        <%=p.Text.Replace(Environment.NewLine, "<br />")%></p>
                </div>
                <%} %>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BoxContent" runat="server">
</asp:Content>
