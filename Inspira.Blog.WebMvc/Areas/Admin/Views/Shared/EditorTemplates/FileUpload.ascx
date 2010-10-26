<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Web.Generics.HtmlHelpers" %>
<% 
    string uniqueId = Guid.NewGuid().ToString();
    string propertyUniqueName = uniqueId + ViewData.TemplateInfo.HtmlFieldPrefix
    
    %>
    <%=Html.Hidden("", Model, new { rel = propertyUniqueName })%>
    <div id="<%=propertyUniqueName%>_FilePreview" class="FilePreview" style="float:left;">
        <span style="margin:0 10px 0 0;">&nbsp;</span>
    </div>
<%=Html.TextBox("Uploader", Model, new { @class = "uploadify", @type = "file", @style = "display:none;", @rel = propertyUniqueName + "_Uploader" })%>
<div id="<%=propertyUniqueName%>_FileTooltip" class="FileTooltip">  </div>

<script type="text/javascript">
    $('input[rel="<%=propertyUniqueName%>_Uploader"]').uploadify({
        'uploader': '<%=ResolveClientUrl("~/Scripts/Uploadify/uploadify.swf")%>',
        'script': '<%= Url.Action("UploadFile") %>',
        'cancelImg': '<%=ResolveClientUrl("~/Scripts/Uploadify/cancel.png")%>',
        'buttonImg': '<%=ResolveClientUrl("~/Scripts/Uploadify/browse.png")%>',
        'auto': true,
        'scriptAccess': 'always',
        'width': 100,
        onComplete: function (event, queueID, fileObj, response, data) {
            if (response.toString().length > 0) {
                $('input[rel="<%=propertyUniqueName%>"]').val(response.toString());
                CreateFilePreview('input[rel="<%=propertyUniqueName%>"]', true, '<%=ResolveClientUrl("~/")%>');
            }
        },
        onError: function (event, queueID, fileObj, errorObj) {
            alert(errorObj.type + ' ' + errorObj.info);
        }
    });

    CreateFilePreview('input[rel="<%=propertyUniqueName%>"]', false, '<%=ResolveClientUrl("~/")%>');    
</script>