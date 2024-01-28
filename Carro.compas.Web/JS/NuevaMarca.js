$(document).ready(function () {
$("#Enviar").click(function () {

    var nombre = $("#nombre").val(); 

    if (nombre == null || nombre == "") {
        return false;

    } else {

        alert("Agregando marca");  


        $.ajax({

            type: "POST",
            url: "/Admin/Nmarcac",
            data: {

                nombre: nombre


            },
            datatype: "Json",
            success: function (data)
            {
                alert("Agregando marca");  


            }



        });




    }




});
});