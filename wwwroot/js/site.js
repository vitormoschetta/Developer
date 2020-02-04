// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Atualiza a tabela de itens 
function Atualiza() {
    
    var MenuPai = $("#filtroMenuPai").val();
    var Back = $("#filtroBack").val();
    var Front = $("#filtroFront").val();
    var Layout = $("#filtroLayout").val();
    var url = "/Menu/ListaItensMenu";

    $.post(url, {filtroMenuPai: MenuPai, filtroBack: Back, filtroFront: Front, filtroLayout: Layout}, function (data)
    {
        $("#tabelaItensMenu").empty();
        $("#tabelaItensMenu").html(data);
    });        

}

//Mostra detalhes do item da tabela selecionado
function Detalhes(parameter) {

    var id = parameter;
    var url = "/Menu/Details";

    $.post(url, {id: id}, function (data)
    {
        $("#detalhesMenu").empty();
        $("#detalhesMenu").html(data);
    });    
    
    $(document).ready(function() {
        $('#detalhesMenu').modal('show');
    })
}

