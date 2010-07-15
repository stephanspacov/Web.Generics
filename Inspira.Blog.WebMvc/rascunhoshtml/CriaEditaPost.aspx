<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CriaEditaPost.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Inserir/Editar post</h1>
    Título: <input type="text" name="TituloPost" size="50" maxlength="100" /><br />
    Conteúdo: <input type="text" name="ConteudoPost" size="50" style="height: 180px" /><br />
    <button type="submit">Salvar</button>
    </div>
    </form>
</body>
</html>
