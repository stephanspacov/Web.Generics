<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Inspira - Blogs - Cadastro
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="block" id="block-text">
        <div class="content">
            <h2 class="title">
                Cadastre-se</h2>
            <div class="inner">
                <form action="#" method="get" class="form">
                <div class="group">
                    <label class="label">
                        Nome:</label>
                    <input type="text" class="text_field">
                    <span class="description">Ex: João</span>
                </div>
                <div class="group">
                    <label class="label">
                        Email:</label>
                    <input type="text" class="text_field">
                    <span class="description">Ex: joão@email.com</span>
                </div>
                <div class="group">
                    <label class="label">
                        Nome do Blog:</label>
                    <input type="text" class="text_field">
                    <span class="description">Ex: Blog do João</span>
                </div>
                <div class="group">
                    <label class="label">
                        Senha:</label>
                    <input type="password" class="text_field">
                </div>
                <div class="group navform wat-cf">
                    <button class="button" type="submit">
                        <img src='<%=ResolveClientUrl("~/assets/img/common/icons/tick.png")%>' alt="Cadastrar">
                        Cadastrar
                    </button>
                    <a href="#header" class="button">
                        <img src="../assets/img/common/icons/cross.png" alt="Cancelar">
                        Cancelar </a>
                </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
