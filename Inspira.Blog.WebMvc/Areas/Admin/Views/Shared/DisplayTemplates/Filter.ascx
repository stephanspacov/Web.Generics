<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Web.Generics.HtmlHelpers.IWebFilter>" %>

<fieldset>
	<legend>Filtrar por</legend>

	<div class="group" id="div1">
		<%=Html.Label("Busca por Texto") %>
		<%= Html.EditorFor(t=>t.SearchQuery)%>
	</div>
	<div class="buttons">
		<button type="submit" class="positive">
			Buscar
		</button>
	</div>
</fieldset>