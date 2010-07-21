<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<table>
	<tr>
		<th></th>
	<% foreach (var property in Web.Generics.ViewHelper.GetProperties(Model)) { %>
		<th><%= property.Name%></th>
	<% } %>
	</tr> 
	<% foreach (var item in (IList)Model) { %>
	<% var objParams = Web.Generics.ViewHelper.GetParamObject(item); %>
	<tr>
		<td>
			<%= Html.ActionLink("Edit", "Edit", objParams)%>
			|
			<%= Html.ActionLink("Details", "Details", objParams)%>
			|
			<%= Html.ActionLink("Delete", "Delete", objParams)%>
		</td>
	<% foreach (var property in Web.Generics.ViewHelper.GetProperties(Model)) { %>
		<td>
			<%= Html.Encode(property.GetValue(item, null)) %>
		</td>
		<% } %>
	</tr>
	<% } %>
</table>
<p>
	<%= Html.ActionLink("Create New", "Create") %>
</p>