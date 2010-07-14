<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inspira.Blog.WebMvc.ViewModels.MeloViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Melo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Melo</h2>

    <h3>Eu sou o Marcio. <%= Model.Anos %></h3>

    <form action="/Marcio/Indeciso">
        Idade: <%=Html.TextBoxFor(x => x.Anos) %>
        <input type="hidden" name="sobrenome" value="Melo" />
        <button type="submit">Enviar</button>
    </form>
</asp:Content>
