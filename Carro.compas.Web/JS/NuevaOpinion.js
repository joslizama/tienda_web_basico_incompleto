$(document).ready(function () {
$("#Enviar").click(function () {


    var tipoopinion = $("#tipoopinion").val(); 
    var descripcion = $("#descripcion").val();
    var prod = $("#prod").val(); 


    if (tipoopinion == null || tipoopinion == "" || descripcion == null || descripcion == "" || prod == null || prod == "") {
        return false;

    } else {

        alert("Agregando opinion "); 

        $.ajax({

            type: "POST",
            url: "/Cliente/Nopinionc",
            data: {

                tipoopinion: tipoopinion,
                descripcion: descripcion,
                prod: prod

            },
            datatype: "Json",
            success: function (data)
            {
                alert("Agregando opinion "); 

            }



        });



    }



});

});