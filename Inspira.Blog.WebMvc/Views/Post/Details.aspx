<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inspira.Blog.WebMvc.ViewModels.Posts.DetailsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="block" id="block-text">
        <div class="content">
            <h2 class="title">
                Título do blog</h2>
            <div class="inner">
                <div class="post">
                    <h3>
                        <%= Model.Post.Title%></h3>
                    <span class="gray">
                        <%=Model.Post.PublishedAt%></span>
                    <p>
                        <%=Model.Post.Text.Replace(Environment.NewLine, "<br />")%></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BoxContent" runat="server">
</asp:Content>
