/*
Copyright 2010 Inspira Tecnologia.
All Rights Reserved.

Contact: Thiago Alves <thiago.alves@inspira.com.br>

This file is part of Web.Generics

Web.Generics is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Web.Generics is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with Web.Generics.  If not, see <http://www.gnu.org/licenses/>.
*/

﻿using System;
using System.Collections.Generic;
using System.Collections;

namespace Web.Generics.UserInterface.JqGrid
{
    public static class JqGridTemplate
    {
        #region DivGridTemplate
        private readonly static string DivGrid = @"
<div id='{GRIDNAME}'>
    <div class='GridFilter'>
        <div class='GridFiltersHeader ui-widget-header ui-button ui-corner-all'><span>Filtros</span><span class='filtericon ui-icon ui-icon ui-icon-carat-1-e fr'></span></div>
        <div class='GridFilters ui-widget-content ui-state-default ui-corner-bottom' style='display:none;'>
            <form class='FormFilters' method='post'>
                <fieldset>
                    <legend class='ui-widget'>Filtrar</legend>
                    {FILTERS}
                    <div class='Filter'>
            	        <input type='submit' name='Ok' id='btnFilterSubmit' value='Filtrar' class='ui-button ui-corner-all ui-button-text-only' />
         	        </div>
                </fieldset>
            </form>
        </div> 
    </div>         
    <div>&nbsp;</div>
    <div class='Grid'>
        <div class='GridTools ui-state-default' style='display:none;'>
            <a href='#' class='ui-icon ui-icon-search btnDetalhes' title='Detalhes'> </a>
            <a href='#' class='ui-icon ui-icon-pencil btnEditar' title='Editar' rel='#FormOverlay'> </a>
            <a href='#' class='ui-icon ui-icon-trash btnApagar' title='Apagar'> </a>
        </div>
        <table class='GridTable'></table>
    </div>
    <div class='GridPager'></div>          
</div>
<div id='dialog-confirm' class='hide ui-state-error' title='Apagar item selecionado?'>
	<p><span class='ui-icon ui-icon-alert' style='float:left; margin:0 7px 20px 0;'></span>Você realmente deseja apagar o ítem selecionado? Esta ação não poderá ser desfeita.</p>
</div>
<script type='text/javascript'>
    $(function() {
        $('#{GRIDNAME} table.GridTable').inspiraGrid('{URL}', '{GRIDNAME}', 'Projeto {Nome} do cliente {Cliente} selecionado.', {COLNAMES}, {COLMODEL});
    });
</script>
            ";
        #endregion

        #region Filter Templates
        private readonly static string TextFilter = @"
                    <div class='Filter'>
            	        <label for='{PROPERTY}'>{PROPERTY}</label>
            	        <input type='hidden' name='Properties[{INDEX}]' id='Properties[{INDEX}]' value='{PROPERTY}' />
            	        <input type='hidden' name='Comparers[{INDEX}]' id='Comparers[{INDEX}]' value='{COMPARER}' />
            	        <input type='text' class='text' name='Values[{INDEX}]' id='Values[{INDEX}]' />
         	        </div>";

        private readonly static string CheckBoxFilter = @"
                    <div class='Filter'>
            	        <label for='{PROPERTY}'>{PROPERTY}</label>
            	        <input type='hidden' name='Properties[{INDEX}]' id='Properties[{INDEX}]' value='{PROPERTY}' />
            	        <input type='hidden' name='Comparers[{INDEX}]' id='Comparers[{INDEX}]' value='{COMPARER}' />             
            	        <input type='checkbox' class='text' name='Values[{INDEX}]' id='Values[{INDEX}]' value='true' />
                        <input type='hidden' name='Values[{INDEX}]' id='Values[{INDEX}]' value='' />
         	        </div>";

        private readonly static string DatePickerFilter = @"
                    <div class='Filter'>
            	        <label for='{PROPERTY}'>{PROPERTY}</label>
            	        <input type='hidden' name='Properties[{INDEX}]' id='Properties[{INDEX}]' value='{PROPERTY}' />
            	        <input type='hidden' name='Comparers[{INDEX}]' id='Comparers[{INDEX}]' value='{COMPARER}' />
            	        <input type='text' class='datepicker' name='Values[{INDEX}]' id='Values[{INDEX}]' />
         	        </div>";

        private readonly static string SelectFilter = @"
                    <div class='Filter'>
            	        <label for='{PROPERTY}'>{PROPERTY}</label>
            	        <input type='hidden' name='Properties[{INDEX}]' id='Properties[{INDEX}]' value='{PROPERTY}' />
            	        <input type='hidden' name='Comparers[{INDEX}]' id='Comparers[{INDEX}]' value='{COMPARER}' />
            	        <select name='Values[{INDEX}]' id='Values[{INDEX}]' class='text'>
                            <option value='' selected='selected'>-- selecione --</option>
                            {OPTIONS}
                        </select>
         	        </div>";
        #endregion

        #region Get/Append templates methods
        internal static string GetDivGridTemplate()
        {
            return DivGrid;
        }

        internal static void AppendTextFilter(int index, JqGridFilter filter, ref string filters)
        {
            filters += TextFilter.
                Replace("{INDEX}", index.ToString()).
                Replace("{PROPERTY}", filter.Property).
                Replace("{COMPARER}", filter.Comparer.ToString());
        }

        internal static void AppendDatePickerFilter(int index, JqGridFilter filter, ref string filters)
        {
            filters += DatePickerFilter.
                Replace("{INDEX}", index.ToString()).
                Replace("{PROPERTY}", filter.Property).
                Replace("{COMPARER}", filter.Comparer.ToString());
        }

        internal static void AppendSelectFilter(int index, JqGridFilter filter, IList options, ref string filters)
        {            
            filters += SelectFilter.
                Replace("{INDEX}", index.ToString()).
                Replace("{PROPERTY}", filter.Property).
                Replace("{OPTIONS}", CreateOptions(options)).
                Replace("{COMPARER}", filter.Comparer.ToString());
        }

        private static string CreateOptions(IList options)
        {
            string optionsTag = string.Empty;

            if (options == null)
                return optionsTag;

            //string idPropertyName = ((IdPropertyAttribute)options.GetType().GetCustomAttributes(typeof(IdPropertyAttribute), true)[0]).PropertyName;

            foreach (var item in options)
            {
            }

            return optionsTag;
        }

        internal static void AppendCheckBoxFilter(int index, JqGridFilter filter, ref string filters)
        {
            filters += CheckBoxFilter.
                Replace("{INDEX}", index.ToString()).
                Replace("{PROPERTY}", filter.Property).
                Replace("{COMPARER}", filter.Comparer.ToString());
        }
        #endregion
    }
}
