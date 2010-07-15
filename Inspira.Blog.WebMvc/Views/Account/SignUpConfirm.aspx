<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SignUpConfirm
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="block" id="block-text">
    <div class="content">
        <h2>Ok</h2>

        <div class="inner">
            <p>Your account has been created</p>
            <a href="../Admin/Post/Create">Create your first post!</a>
        </div>
    </div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BoxContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
