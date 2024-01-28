$(document).ready(function () {
$("#Modificar").click(function () {


    var nombre = $("#nombre").val(); 

    if (nombre == null || nombre == "") {
        return false;


    } else {

        alert("Modificando marca");

        $.ajax({

            type: "POST",
            url: "/Admin/Mmarcasc",
            data: {

                nombre: nombre


            },
            datatype: "Json",
            success: function (data)
            {
                alert("Modificando marca");

            }




        });




    }





});
});