<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Decimal>" %>
<%=Html.Encode(Model.ToString("0.00"))%>
