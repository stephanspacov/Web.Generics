<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<fieldset>
	<legend>Fields</legend>
	<%= Html.DisplayForModel() %>
</fieldset>
	<% var objParams = Web.Generics.ViewHelper.GetParamObject(Model); %>
<p>
	<%= Html.ActionLink("Edit", "Edit", objParams) %> |
	<%= Html.ActionLink("Back to List", "Index") %>
</p>


