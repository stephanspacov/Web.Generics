$(function () {
    $.mask.definitions['~'] = '[-]?';

    $("input.cpf").mask("999.999.999-99");
    $("input.cnpj").mask("99.999.999/9999-99");
    $("input.cep").mask("99999-999");
    $("input.date").mask("99/99/9999");
    $("input.time").mask("99:99");
    $("input.phone").mask("(99) 9999-9999");
    $("input.idade").mask("99");
    $("input.unsigned-integer").mask("999999999");
    $("input.integer").mask("9?999999999");

    $("input.date").datepicker();

    $("input.money").priceFormat({ prefix: '', centsSeparator: ',', thousandsSeparator: '.' });

    // foca o primeiro elemento do formulário
    $('form:not(.filter) :input:visible:first').focus();

    // um checkbox seleciona todos
    var tog = false; // or true if they are checked on load
    $('.select-all-activator').click(function () {
        $("input[type=checkbox].select-all").attr("checked", !tog);
        tog = !tog;
    });

    $('.delete-all').click(function (event) { if (!confirm('Deseja apagar os itens selecionados?')) event.preventDefault(); });

    // Set Datepickers
    //$('input.datepicker').datepicker({ showOn: 'button', buttonImageOnly: true, buttonImage: '/assets/img/icons/calendar.png' });

    //TinyMCE
    tinyMCE.init({
        mode: "textareas",
        theme: "simple",
        editor_selector: "richtext",
        editor_deselector: "mceNoEditor"
    });
});