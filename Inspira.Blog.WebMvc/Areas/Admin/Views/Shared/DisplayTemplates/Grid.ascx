<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Web.Generics.HtmlHelpers.IWebGrid>" %>

<input type="hidden" name="<%= this.ViewData.TemplateInfo.HtmlFieldPrefix %>.PreviousSortProperty" value="<%=Model.SortProperty %>" />
<input type="hidden" name="<%= this.ViewData.TemplateInfo.HtmlFieldPrefix %>.PreviousSortOrder" value="<%=Model.SortOrder %>" />
<table class="table">
	<thead>
		<tr>
			<th class="first"><input type="checkbox" class="select-all-activator" /></th>
			<% foreach (var property in Model.GetPropertyNames()) {
                if (property.Name == "ID" || property.Name.EndsWith("List")) continue;
          %>
            <th><button type="submit" name="<%= this.ViewData.TemplateInfo.HtmlFieldPrefix %>.SortProperty" value="<%=property.Name%>"><%=property.Caption %></button></th>
            <% } %>
			<th class="last"></th>
		</tr>
	</thead>
	<tbody>
<% 
    Int32 count = 0;
    foreach (var item in Model.GetDataSourceEnumerator()) {
    String alt = "";
    if (count % 2 == 1) alt = "class=\"alternate\"";
    count++;
        
        %>
    <tr <%=alt%>>
	    <td><input type="checkbox" class="select-all" name="DeletedItems" value="<%=Model.GetValue(item, "ID")%>" /></td>
        <% foreach (var property in Model.GetPropertyNames()) {
               if (property.Name == "ID" || property.Name.EndsWith("List")) continue;
            %>
	    <td><%= Model.GetValue(item, property.Name) %></td>
        <% } %>
	    <td>
            <a href='<%= Url.Action("Edit", new { ID = Model.GetValue(item, "ID") }) %>'><img src='<%= ResolveClientUrl("~/Content/icons/edit-icon.gif") %>' alt="Editar" /></a>
            <a href='<%= Url.Action("Delete", new { ID = Model.GetValue(item, "ID") }) %>'><img src='<%= ResolveClientUrl("~/Content/icons/delete-icon.gif") %>' alt="Delete" /></a>
	    </td>
    </tr>
<%
    }
        
    if (count == 0) { 
%>
<tr align="center"><td colspan="20">Não foi encontrado nenhum registro.</td></tr>
<%
    }
%>
</tbody>
<tfoot>
	<tr>
		<td colspan="20">
						
		</td>
	</tr>
</tfoot>
</table>