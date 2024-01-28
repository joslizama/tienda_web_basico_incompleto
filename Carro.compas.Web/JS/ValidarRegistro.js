$(document).ready(function () {
$("#Registro").click(function () {

    var rut = $("#rut").val(); 
    var nombre = $("#nombre").val(); 
    var apellido = $("#apellido").val(); 
    var direccion = $("#direccion").val(); 
    var comuna = $("#comuna").val(); 
    var ciudad = $("#ciudad").val(); 
    var correo = $("#correo").val(); 
    var telefono = $("#telefono").val();

    if (rut == null || rut == "" || nombre == "" || nombre == null ||| apellido == null || apellido == "" || direccion == null || direccion == "" || comuna == null || comuna == "" || ciudad == null || ciudad == "" || correo == null || correo == "" || telefono == null || telefono == "") {
        return false;
    } else {


        alert("Registrando usuario"); 



    }
});
});