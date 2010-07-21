<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Web.Generics.HtmlHelpers.IWebPager>" %>

<% if (Model.AllowPaging) { %> 
<div class="pagination">
    <!-- Check whether it will show << and < or not -->
    <% if (Model.PageIndex > 1)
       { %>
    <button type="submit" name="<%= this.ViewData.TemplateInfo.HtmlFieldPrefix %>.PageIndex" value="1">&lt;&lt;</button>
    <button type="submit" name="<%= this.ViewData.TemplateInfo.HtmlFieldPrefix %>.PageIndex" value="<%=Model.PageIndex - 1 %>">&lt; anterior</button>
    <% }

       // Check if it has more than 5 pages before the current one
       if (Model.PageIndex - 5 > 1)
       { %>
    <button type="submit" name="<%= this.ViewData.TemplateInfo.HtmlFieldPrefix %>.PageIndex" value="<%= Model.PageIndex - 6 %>">...</button>
    <% }
       for (int index = 5; index > 0; index--)
       {
            if (Model.PageIndex - index > 0)
            { %>
    <button type="submit" name="<%= this.ViewData.TemplateInfo.HtmlFieldPrefix %>.PageIndex" value="<%= Model.PageIndex - index %>"><%= Model.PageIndex - index %></button>
    <%      }
       } %>
    <span class="current"><%= Model.PageIndex%></span>
    <% for (int index = 1; index <= 5; index++)
       {
           if (Model.PageIndex + index <= Model.NumberOfPages)
           { %>
    <button type="submit" name="<%= this.ViewData.TemplateInfo.HtmlFieldPrefix %>.PageIndex" value="<%= Model.PageIndex + index %>"><%= Model.PageIndex + index %></button>
    <%     }
       }
        // Check if it has more than 5 pages after the current one
       if (Model.PageIndex + 5 < Model.NumberOfPages)
       { %>
    <button type="submit" name="<%= this.ViewData.TemplateInfo.HtmlFieldPrefix %>.PageIndex" value="<%= Model.PageIndex + 6 %>">...</button>
    <% } %> 
        
    <% // Check whether it will show > and >> or not
    if (Model.PageIndex < Model.NumberOfPages)
       { %>
    <button type="submit" name="<%= this.ViewData.TemplateInfo.HtmlFieldPrefix %>.PageIndex" value="<%= Model.PageIndex + 1 %>">próxima &gt;</button>
    <button type="submit" name="<%= this.ViewData.TemplateInfo.HtmlFieldPrefix %>.PageIndex" value="<%= Model.NumberOfPages %>">&gt;&gt;</button>
    <% } %>
    <span>Página <%= Model.PageIndex %> de <%= Model.NumberOfPages %></span>
    </div>
<% } %>

<%--
<div class="pager pagination" style="display:none">
<% 
    if (Model.TotalItemCount > Model.PageSize)
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
	<button type="submit" name="IdiomaGrid.PageIndex" value="<%=Model.PageIndex + 1 %>">&gt;</button>
	<button type="submit" name="goTO" value="last">&gt;&gt;</button>
<% } %>
<%= Html.DropDownList("PageSize", new SelectList(new[] { "5", "10", "20" }))%>
 Registros <%= Model.PageIndex * Model.PageSize - (Model.PageSize - 1)%> -
<% if (Model.PageIndex * Model.PageSize > Model.TotalItemCount)
   {%>
<%= Model.TotalItemCount%>
<% }
   else
   {%>
<%= Model.PageIndex * Model.PageSize%>
<% } %> de <%= Model.TotalItemCount%>

<% 
    }
%>
</div>
--%>