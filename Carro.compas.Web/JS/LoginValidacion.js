$(document).ready(function () {
    $("#Ingresar").click(function () {


        var rut = $("#rut").val();
        var contraseña = $("#contraseña").val(); 

        if (rut == null || rut == "" || contraseña == null || contraseña == "") {
            return false;
        } else {

            alert("Validando usuario");


        }




    });

}); 