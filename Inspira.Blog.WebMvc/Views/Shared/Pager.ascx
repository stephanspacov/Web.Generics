<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Web.Generics.FilterParameters>" %>

<% if (Model.NumberOfPages > 1) { %>
<div class="pagination">
    <input type="hidden" name="PageIndex" value="<%=Model.PageIndex %>" />
    <%if (Model.HasPrevious) { %><button class="prev_page" name="NextPageIndex" value="<%=Model.PageIndex - 1 %>"> << Previous</button><% } %>
    <% for(Int32 i = 1; i<=Model.NumberOfPages;i++) { %>
        <% if (i == Model.PageIndex) { %>
        <span class="current"><%=i %></span>
        <% } else { %>
        <button type="submit" name="NextPageIndex" value="<%=i %>"><%=i %></button>
        <% } %>
    <% } %>
    <%if (Model.HasNext) { %><button class="next_page" rel="next" name="NextPageIndex" value="<%=Model.PageIndex + 1 %>">Next >></button><%} %>
</div>
<% } %>

<div class="pager pagination" style="display:none">
<% 
    if (Model.Total > Model.PageSize)
    {
%>
<!-- Check whether it will show << and < or not -->
<% if (Model.PageIndex > 1)
   { %>
	<span class="disabled prev_page"><button type="submit" name="goTO" value="first">&lt;&lt;</button></span>
	<button type="submit" name="PageIndex" value="<%=Model.PageIndex - 1 %>">&lt;</button>
<% } %>

 Página <input size="2" type="text" name="PageNumber" value='<%= Model.PageIndex %>'/> de <%= Model.NumberOfPages%>
 <button type="submit" name="goTO" value="gosto"> ir para a página</button>
<% if (Model.PageIndex < Model.NumberOfPages)
   { %>
	<button type="submit" name="PageIndex" value="<%=Model.PageIndex + 1 %>">&gt;</button>
	<button type="submit" name="goTO" value="last">&gt;&gt;</button>
<% } %>
<%= Html.DropDownList("PageSize", Model.PageSizes)%>
 Registros <%= Model.PageIndex * Model.PageSize - (Model.PageSize - 1)%> -
<% if (Model.PageIndex * Model.PageSize > Model.Total)
   {%>
<%= Model.Total%>
<% }
   else
   {%>
<%= Model.PageIndex * Model.PageSize%>
<% } %> de <%= Model.Total%>

<% 
    }
%>
</div>