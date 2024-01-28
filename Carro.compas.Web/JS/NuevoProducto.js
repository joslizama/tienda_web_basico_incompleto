$(document).ready(function () {
$("#Enviar").click(function () {

    var nombre = $("#nombre").val(); 
    var imagen = $("#imagen").val(); 
    var descripcion = $("#descripcion").val();
    var precio = $("#precio").val(); 
    var stock = $("#stock").val(); 
    var categorias = $("#categorias").val(); 
    var scategorias = $("#scategorias").val(); 
    var marcas = $("#marcas").val();

    if (nombre == null || nombre == "" || imagen == null || imagen == "" || descripcion == null || descripcion == "" || precio == null || precio == "" || stock == null || stock == "" || categorias == null || categorias == "" || scategorias == null || scategorias == "" || marcas == null || marcas == "") {
        return false;
    } else {

        alert("Agregando producto"); 




    }



});
});