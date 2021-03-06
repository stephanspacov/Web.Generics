﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inspira.Blog.WebMvc.Areas.Admin.ViewModels.Post.CreateViewModel>" %>

<asp:Content ID="Content5" ContentPlaceHolderID="TitleContent" runat="server">
    Inspira - Blogs - Create a Blog
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div class="block" id="block-text">
        <div class="content">
            <div class="inner">
				<% if (Model.BlogCreated) { %>
				<div class="flash">
					<div class="message notice">
						<p>Your post has been successfully saved!</p>
					</div>
				</div>
				<% } %>
                <form action="" method="post" class="form">
				<%= Html.HiddenFor(v => v.PostID) %>
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
                            <%= Html.ValidationMessageFor(v => v.PostText) %></span>
                    </label>
                    <%=Html.TextAreaFor(v => v.PostText, new { @class = "text_area", rows = "10", cols = "80" })%>
                    <span class="description">Write here a long text</span>
                </div>
                <div class="group navform wat-cf">
                    <button class="button" type="submit" name="action" value="Save">
                        <img src='<%=ResolveClientUrl("~/assets/img/common/icons/tick.png")%>' alt="Save Post" />
                        Save
                    </button>
                    <button class="button" type="submit" name="action" value="Publish">
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
