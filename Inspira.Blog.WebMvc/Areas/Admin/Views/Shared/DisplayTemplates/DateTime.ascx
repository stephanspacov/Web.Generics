<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.DateTime?>" %>
<%=Html.Encode(Model.HasValue ? Model.Value.ToString("dd/MM/yyyy") : string.Empty)%>