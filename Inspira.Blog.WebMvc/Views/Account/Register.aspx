<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Web.Generics.UserInterface.Models.RegisterModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Inspira - Blogs - Sign Up
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="block" id="block-text">
        <div class="content">
            <h2 class="title">Register to create your blog</h2>
            <div class="inner">
				<div class="flash">
				<% if (Model.ShowMinimumCharaterLengthMessage) { %>
					<div class='message error'>
						<p>Passwords are required to be a minimum of <%: Model.MinimumCharaterLength %> characters in length.</p>
					</div>
				<% } %>
				</div>
                <form action="" method="post" class="form">
                <div class="group fieldWithErrors">
                    <label class="label">
                        Name:
                        <span class="error"><%= Html.ValidationMessageFor(v => v.UserName)%></span>
                    </label>
                    <%= Html.TextBoxFor(v => v.UserName, new { @class = "text_field" })%>
                    <span class="description">Ex: Mark</span>
                </div>
                <div class="group fieldWithErrors">
                    <label class="label">
                        E-mail:
                        <span class="error"><%= Html.ValidationMessageFor(v => v.Email) %></span>
                    </label>
                    <%= Html.TextBoxFor(v => v.Email, new { @class = "text_field" })%>
                    <span class="description">Ex: mark@email.com</span>
                </div>
                <div class="group fieldWithErrors">
                    <label class="label">
                        Password:
                        <span class="error"><%= Html.ValidationMessageFor(v => v.Password) %></span>
                    </label>
                    <%= Html.PasswordFor(v => v.Password, new { @class = "text_field" })%>
                </div>
                <div class="group fieldWithErrors">
                    <label class="label">
                        Confirm password:
                        <span class="error"><%= Html.ValidationMessageFor(v => v.ConfirmPassword) %></span>
                    </label>
                    <%= Html.PasswordFor(v => v.ConfirmPassword, new { @class = "text_field" })%>
                </div>
                <div class="group navform wat-cf">
                    <button class="button" type="submit">
                        <img src='<%=ResolveClientUrl("~/assets/img/common/icons/tick.png")%>' alt="Create my account" />
                        Create my account
                    </button>
                    <a href="../" class="button">
                        <img src="../assets/img/common/icons/cross.png" alt="Cancel" />
                        Cancel </a>
                </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
