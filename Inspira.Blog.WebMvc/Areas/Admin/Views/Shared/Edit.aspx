<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Editar
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<% Html.EnableClientValidation(); %>
	<div ID="Content">
	    <div id="Form">
            <h2 class="FormHeader ui-widget-header ui-corner-top">Editar</h2>
            <div class="ui-widget-content  ui-corner-bottom">
                <% Html.RenderPartial("Partial/Form"); %>
            </div>
        </div>
	</div> 
</asp:Content>
