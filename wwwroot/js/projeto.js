//Novo Item
function New() {
    var url = "/Home/Create";

    $.post(url, function (data)
    {
        $("#CreateProjeto").html(data);
        $('#modalCreate').modal('show');
    });  
}


//Mostra detalhes do item da tabela selecionado
function Details(params) {
    var url = "/Home/Details";

    $.post(url, {id: params}, function (data)
    {
        $("#detalhesProjeto").html(data);
        $('#modalDetails').modal('show');
    });    
    
}


//Editar Itens
function Edit(params) {
    var url = "/Home/Edit";

    $.post(url, {id: params}, function (data)
    {
        $("#editProjeto").html(data);
        $('#modalEdit').modal('show');
    });  
}


//Mostra Item a ser deletado
function Delete(params) {
    var url = "/Home/Delete";

    $.post(url, {id: params}, function (data)
    {
        $("#deleteProjeto").html(data);
        $('#modalDelete').modal('show');
    });    
}
