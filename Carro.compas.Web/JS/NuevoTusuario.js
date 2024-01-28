$(document).ready(function () {
$("#Enviar").click(function () {

    var nombre = $("#nombre").val(); 

    if (nombre == null || nombre == "") {
        return false;

    } else {

    alert("Agregando tipo de usuario");

        $.ajax({

            type: "POST",
            url: "/Admin/NTusuarioc",
            data: {

                nombre: nombre

            },
            datatype: "Json",
            success: function (data)
            {
                alert("Agregando tipo de usuario");

            }




        });




    }
});

});