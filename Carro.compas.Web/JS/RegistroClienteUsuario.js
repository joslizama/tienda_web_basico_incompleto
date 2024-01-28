$(document).ready(function () {
$("#Registro").click(function () {

    var nusuario = $("#nusuario").val(); 
    var contraseña = $("#contraseña").val(); 

    if (nusuario == null || nusuario == "" || contraseña == null || contraseña == "") {
        return false;

    } else {

        alert("Registrando usuario");


    }



});    
});