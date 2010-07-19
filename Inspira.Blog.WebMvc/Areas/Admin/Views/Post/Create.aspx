<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inspira.Blog.WebMvc.ViewModels.Account.SignUpViewModel>" %>

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
                            <input class="text_field" type="text">
                            <%= Html.ValidationMessageFor(v => v.BlogTitle) %></span>
                    </label>
                    <span class="description">Ex: Mark's text</span>
                </div>
                <div class="group fieldWithErrors">
                    <label class="label">
                        Text:
                    </label>
                    <textarea class="text_area" rows="10" cols="80"></textarea>
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
