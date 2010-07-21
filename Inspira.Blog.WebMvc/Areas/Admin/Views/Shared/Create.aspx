<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Web.Generics.UserInterface.GenericViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Create</h2>
	<%-- Html.RenderPartial("Partial/Form"); --%>

	<% using (Html.BeginForm()) {%>
    <%= Html.ValidationSummary(string.Empty, new { Class="msg_erro" })%>  
    <fieldset class="FormEditor"><legend>Fields</legend>
        <%=Html.EditorFor(model => model.Instance)%>
        <div class="cb">
	        <input type="submit" value="Save" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" />
        </div>
    </fieldset>
	<% } %>
	<div >
		<%= Html.ActionLink("Back to List", "Index") %>
	</div>
</asp:Content>
