<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<String>" %>
<%=Html.TextArea("", Model, new { @class = "tinymce" }) %>