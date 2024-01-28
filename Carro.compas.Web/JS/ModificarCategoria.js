$(document).ready(function () {
$("#Modificar").click(function () {

    var nombre = $("#nombre").val(); 

    if (nombre == null || nombre == "") {

        return false;

    } else {

        alert("Modificando categoria"); 

        $.ajax({

            type: "POST",
            url: "/Admin/Mcategoriasc",
            data: {

                nombre: nombre
            
            },
            datatype: "Json",
            success: function (data)
            {
                alert("Modificando categoria"); 

            }


        });

        window.location.href = "~/Admin/Lcategorias";

    }
});
});