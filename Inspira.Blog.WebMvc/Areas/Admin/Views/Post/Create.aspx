<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inspira.Blog.WebMvc.Areas.Admin.ViewModels.Post.CreateViewModel>" %>

<asp:Content ID="Content5" ContentPlaceHolderID="TitleContent" runat="server">
    Inspira - Blogs - Create a Blog
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div class="block" id="block-text">
        <div class="content">
            <div class="inner">
                <form action="" method="post" class="form">
                <div class="group fieldWithErrors">
                    <label class="label">
                        Title: <span class="error">
                            <%= Html.ValidationMessageFor(v => v.PostTitle) %></span>
                    </label>
                    <%= Html.TextBoxFor(v => v.PostTitle, new { @class = "text_field" })%>
                    <span class="description">Ex: Mark's text</span>
                </div>
                <div class="group fieldWithErrors">
                    <label class="label">
                        Text:<span class="error">
                            <%= Html.ValidationMessageFor(v => v.TextArea) %></span>
                    </label>
                    <%=Html.TextAreaFor(v => v.TextArea, new { @class = "text_area", rows="10", cols="80"}) %>
                    <span class="description">Write here a long text</span>
                </div>
                <div class="group navform wat-cf">
                    <button class="button" type="submit">
                        <img src='<%=ResolveClientUrl("~/assets/img/common/icons/tick.png")%>' alt="Save Post" />
                        Save
                    </button>
                    <button class="button" type="submit">
                        <img src='<%=ResolveClientUrl("~/assets/img/common/icons/tick.png")%>' alt="Publish Post" />
                        Publish
                    </button>
                    <a href="../" class="button">
                        <img src='<%=ResolveClientUrl("~/assets/img/common/icons/cross.png")%>' alt="Return" />
                        Return </a>
                </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
