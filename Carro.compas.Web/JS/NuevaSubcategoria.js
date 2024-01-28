$(document).ready(function () {
$("#Enviar").click(function () {

    var nombre = $("#nombre").val(); 

    if (nombre == null || nombre == "") {
        return false;
    } else {


        alert("Agregando subcategoria"); 


        $.ajax({

            type: "POST",
            url: "/Admin/Nsubcategoriac",
            data: {

                nombre: nombre

            },
            datatype: "Json",
            success: function (data)
            {
                alert("Agregando subcategoria"); 

            }


        });



    }


});
});



