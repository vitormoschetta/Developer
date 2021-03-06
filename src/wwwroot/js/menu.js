﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Atualiza a tabela de itens 
function Select() {
    
    var MenuPai = $("#filtroMenuPai").val();
    var Back = $("#filtroBack").val();
    var Front = $("#filtroFront").val();
    var Layout = $("#filtroLayout").val();
    //var url = "/Menu/ListaItensMenu";
    var url = $("#btn-lista").data("url");    
    var ChekMenuPai = 0;

    if ($("#checkMenusPai").is(':checked'))
        ChekMenuPai = 1;

    $.post(url, {filtroMenuPai: MenuPai, filtroBack: Back, filtroFront: Front, filtroLayout: Layout, filtroCheckMenuPai: ChekMenuPai}, function (data)
    {
        $("#tabelaItensMenu").empty();
        $("#tabelaItensMenu").html(data);
    });        

}

//Mostra detalhes do item da tabela selecionado
function Details(params) {
    //var url = "/Menu/Details";
    var url = $("#btn-details").data("url");   

    $.post(url, {id: params}, function (data)
    {
        $("#detalhesMenu").html(data);
        $('#modalDetails').modal('show');
    });    
    
}

//Mostra Item a ser deletado
function Delete(params) {
    //var url = "/Menu/DeleteMenu";
    var url = $("#btn-delete").data("url");   

    $.post(url, {id: params}, function (data)
    {
        $("#deleteMenu").html(data);
        $('#modalDelete').modal('show');
    });    
}

//Editar Itens
function Edit(params) {
    //var url = "/Menu/EditMenu";
    var url = $("#btn-edit").data("url");   

    $.post(url, {id: params}, function (data)
    {
        $("#editMenu").html(data);
        $('#modalEdit').modal('show');
    });  
}

//Novo Item
function New() {
    //var url = "/Menu/CreateMenu";
    var url = $("#btn-novo").data("url");   

    $.post(url, function (data)
    {
        $("#createMenu").html(data);
        $('#modalCreate').modal('show');
    });  
}


function BuscaDinamica(params) {
    //var url = "/Menu/BuscaDinamica";
    var url = $("#btn-busca-dinamica").data("url");   

    var ChekMenuPai = 0;

    if ($("#checkMenusPai").is(':checked'))
        ChekMenuPai = 1;

    $.post(url, {texto: params, filtroCheckMenuPai: ChekMenuPai}, function (data)
    {
        $("#tabelaItensMenu").empty();
        $("#tabelaItensMenu").html(data);
    });  
}
