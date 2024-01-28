$(document).ready(function () {
$("#Modificar").click(function () {

    var nombre = $("#nombre").val(); 

    if (nombre == null || nombre == "") {
        return false;

    } else {

        alert("Modificando subcategoria"); 


        $.ajax({

            type: "POST",
            url: "/Admin/Mscategoriasc",
            data: {

                nombre: nombre

            },
            datatype: "Json",
            success: function (data)
            {
                alert("Modificando subcategoria"); 

            }


        }); 

        window.location.href = "~/Admin/Lsubcategorias";

    }
});
});