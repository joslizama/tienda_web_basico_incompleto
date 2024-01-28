$(document).ready(function () {
$("#Modificar").click(function () {

    var nombre = $("#nombre").val(); 

    if (nombre == null || nombre == "")
    {
        return false;

    } else{

        alert("Modificando tipo de usuario ");

        $.ajax({

            type: "POST",
            url: "/Admin/Mtusuarioc",
            data: {

                nombre: nombre

            },
            datatype: "Json",
            success: function (data)
            {
                alert("Modificando tipo de usuario ");

            }


        });




    }



});
});