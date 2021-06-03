//Novo Item
function New() {
    //var url = "/Home/Create";
    var url = $("#btn-novo").data("url");    

    $.post(url, function (data)
    {
        $("#CreateProjeto").html(data);
        $('#modalCreate').modal('show');
    });  
}


//Mostra detalhes do item da tabela selecionado
function Details(params) {
    //var url = "../Home/Details";
    var url = $("#btn-details").data("url");

    $.post(url, {id: params}, function (data)
    {
        $("#detalhesProjeto").html(data);
        $('#modalDetails').modal('show');
    });    
    
}


//Editar Itens
function Edit(params) {
    //var url = "../Home/Edit";
    var url = $("#btn-edit").data("url");

    $.post(url, {id: params}, function (data)
    {
        $("#editProjeto").html(data);
        $('#modalEdit').modal('show');
    });  
}


//Mostra Item a ser deletado
function Delete(params) {
    //var url = "../Home/Delete";
    var url = $("#btn-delete").data("url");

    $.post(url, {id: params}, function (data)
    {
        $("#deleteProjeto").html(data);
        $('#modalDelete').modal('show');
    });    
}
