<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.DateTime?>" %>
<% if (Model.HasValue && !Model.Value.Equals(DateTime.MinValue)) { %>
    <%=Html.TextBox("", Model.Value.ToString("dd/MM/yyyy"), new { @class = "text_field date" })%>
<% } else { %>
    <%=Html.TextBox("", string.Empty, new { @class = "text_field date" })%>
<% } %>