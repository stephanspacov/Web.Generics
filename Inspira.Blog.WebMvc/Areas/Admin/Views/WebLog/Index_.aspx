<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<Inspira.Blog.WebMvc.Areas.Admin.ViewModels.WebLog.ListViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Index</h2>
	<div class="block">
		<div class="content">
			<h2 class="title">Blog</h2>
			<div class="inner">
				<h3>lista de grobs</h3>
				<div class="content">
					<%= Html.Grid(m => m.DefaultGrid) %>
				</div>
			</div>
			<div class="inner">
				<h3>otra lista de grobs</h3>
				<div class="content">
					<%= Html.Grid(m => m.Wrapper.OutroGrid) %>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BoxContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
