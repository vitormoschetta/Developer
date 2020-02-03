// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//http://www.macoratti.net/15/05/mvc_ajax.htm
function Atualiza() {
    
    var MenuPai = $("#filtroMenuPai").val();
    var Back = $("#filtroBack").val();

    var url = "/Menu/ListaTodos";
    $.post(url, {filtroMenuPai: MenuPai, filtroBack: Back}, function (data)
    {
        $("#frm").empty();
        $("#frm").html(data);
    });        

}

