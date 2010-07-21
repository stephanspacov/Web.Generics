<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Object>" %>

<script runat="server">
    bool ShouldShow(ModelMetadata metadata) {
        return metadata.ShowForEdit
            //&& metadata.ModelType != typeof(System.Data.EntityState)
            //&& !metadata.IsComplexType
            && !ViewData.TemplateInfo.Visited(metadata);
    }
</script>
<% if (ViewData.TemplateInfo.TemplateDepth > 1) { %>
    <% if (Model == null) { %>
        <%= ViewData.ModelMetadata.NullDisplayText %>
    <% } else { %>
        <%= ViewData.ModelMetadata.SimpleDisplayText %>
    <% } %>
<% } else { %>
    <% foreach (var prop in ViewData.ModelMetadata.Properties.Where(pm => ShouldShow(pm))){ %>
        <% if (prop.HideSurroundingHtml) { %>
            <%= Html.Editor(prop.PropertyName) %>
        <% } else { %>
                    <% if (!String.IsNullOrEmpty(Html.Label(prop.PropertyName).ToHtmlString())) { %>
                         <div class="editor-label cb">
                            <%= prop.IsRequired ? "*" : "" %>
                            <%= Html.Label(prop.PropertyName) %>
                        </div> 
                    <% } %>
                             
                    <div class="editor-field">
                    <%if (!prop.IsComplexType){%>
                    
                        <%= Html.Editor(prop.PropertyName)%>
                    
                    <%}else{%>
                        <%var DropDownListAttr = ViewData.ModelMetadata.ModelType.GetProperty(prop.PropertyName).GetCustomAttributes(typeof(Web.Generics.DataAnnotations.DropDownListAttribute), false);
                          if (DropDownListAttr.Length == 1)
                          {
                              var attr = (Web.Generics.DataAnnotations.DropDownListAttribute)DropDownListAttr[0];
                          %>
                            <%= Html.DropDownList(prop.PropertyName + "." + attr.DataValueField, new SelectList(ViewData[attr.ViewDataKey] as IEnumerable, attr.DataValueField, attr.DataTextField, attr.GetSelectedValue(Model)), attr.OptionLabel, attr.HtmlAttributes)%>
                            <%= Html.ValidationMessage(prop.PropertyName, "*")%>
                          <%}
                          else
                          {%>
                            <%= Html.Editor(prop.PropertyName)%>
                            <%= Html.ValidationMessage(prop.PropertyName, "*") %>
                          <%}
                        %>                      
                    
                    <%}%>
                        
                    </div>
        <% } %>
    <% } %>
<% } %>