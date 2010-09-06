<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Inspira.Blog.WebMvc.Areas.Admin.ViewModels.Post.IndexViewModel>" %>

<h3>Filtrar posts</h3>

<div>
	Published status:
	<%= Html.DropDownListFor(f => f.Filter.Published, Model.Filter.PublishedSelectList, " -- All status -- ")%>
</div>

<div>
	Criado entre:
	<%=Html.TextBoxFor(f => f.Filter.CreatedAtStart)%>
	e
	<%=Html.TextBoxFor(f => f.Filter.CreatedAtEnd)%>
</div>

<div>
	Filtrar por blog:
	<%= Html.DropDownListFor(f => f.Filter.BlogID, Model.Filter.BlogSelectList, " -- All blogs -- ")%>
</div>

<div>
	Por texto:
	<%=Html.TextBoxFor(f => f.Filter.SearchQuery)%>
</div>

<div>
	<button type="submit">Filter</button>
</div>

<!--
p => 
	(
		:publicado == null
		||
		:publicado && p.PublishedAt != null && p.PublishedAt < DateTime.Now
		||
		!:publicado && p.PublishedAt == null
	)
	&&
	(
		:created_at_start == null || p.CreatedAt < :created_at_start
		&&
		:created_at_end == null || p.CreatedAt > :created_at_end
	)
	&&
	(
		:blogID == null
		||
		p.Blog.ID == :blogID
	)
	&&
	(
		:blogText == null
		||
		p.Title.Contains(:blogText)
		||
		p.Text.Contains(:blogText)
	)
-->	