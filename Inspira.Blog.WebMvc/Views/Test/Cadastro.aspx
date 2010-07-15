<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Inspira - Blogs - Cadastro
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="block" id="block-text">
    <div class="content">
        <h2 class="title">Cadastre-se</h2>
        <div class="inner">
            <form id="form1" runat="server">
            <div>
            Nome: <input type="text" name="Nome" size="40" maxlength="50" />
            <br />
            E-mail: <input type="text" name="Email" size="40" maxlength="50" />
            <br />
            Nome do Blog: <input type="text" name="NomeBlog" size="40" maxlength="50" />
            <br />
            Senha: <input type="password" name="senha" size="40" maxlength="20" />
            <br />
            <button type="submit">Cadastrar</button>
            </div>
            </form>

<form action="#" method="get" class="form">
                <div class="group">
                  <label class="label">Text field</label>
                  <input type="text" class="text_field">
                  <span class="description">Ex: a simple text</span>
                </div>
                <div class="group">
                  <div class="fieldWithErrors">
                    <label class="label" for="post_title">Title</label>
                    <span class="error">can't be blank</span>
                  </div>
                  <input type="text" class="text_field">
                  <span class="description">Ex: a simple text</span>
                </div>
                <div class="group">
                  <label class="label">Text area</label>
                  <textarea class="text_area" rows="10" cols="80"></textarea>
                  <span class="description">Write here a long text</span>
                </div>
                <div class="group navform wat-cf">
                  <button class="button" type="submit">
                    <img src="images/icons/tick.png" alt="Save"> Save
                  </button>
                  <a href="#header" class="button">
                    <img src="images/icons/cross.png" alt="Cancel"> Cancel
                  </a>
                </div>
              </form>
        </div>
    </div>
</div>
</asp:Content>