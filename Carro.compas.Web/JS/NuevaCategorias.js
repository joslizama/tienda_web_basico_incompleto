$(document).ready(function () {
$("#Enviar").click(function () {

    var nombre = $("#nombre").val(); 

    if (nombre == null || nombre == "") {
        return false; 

    } else {

        alert("Ingresando categoria"); 

        $.ajax({

            type: "POST",
            url: "/Admin/Ncategoriasc",
            data: {

                nombre: nombre


            },
            datatype: "Json",
            success: function (data)
            {
                alert("Ingresando categoria"); 

            }



        });

        window.location.href = "~/Admin/Lcategorias";


    }




});
});