using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Carro.compas.Web.Models; 

namespace Carro.compas.Web.Controllers
{
    public class HomeController : Controller
    {
        Conexiones dbc = new Conexiones();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }  

        //Login vista 
        public ActionResult Log()
        {
            return View(); 

        }
      //Login controlador 
      public ActionResult Loginc()
      {

            var rut = Convert.ToString(Request.Form["rut"]);
            var contraseña = Convert.ToString(Request.Form["contraseña"]);

            //Hacer consulta al cliente_tipo para hacer las sesiones y redireccionar  

            var c = dbc.cliente_tipo.Where(p => p.cliente_id == rut && p.clave == contraseña).SingleOrDefault();

            if(c == null)
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                //Hacer consulta para redireccionar al tipo de usuario 

                var op = Convert.ToInt32(c.tipo_id);

                switch (op)
                {

                    case 1:

                        //Hacer sesiones para transportar los datos 
                        Session["rut"] = Convert.ToString(c.cliente_id);
                        Session["nombre"] = Convert.ToString(c.nombre_usuario);
                        Session["tipo"] = Convert.ToInt32(c.tipo_id);

                        return RedirectToAction("Index_admin", "Admin");


                        break;

                    case 2:
                        //Hacer sesiones para transportar los datos 
                        Session["r"] = Convert.ToString(c.cliente_id);
                        Session["n"] = Convert.ToString(c.nombre_usuario);
                        Session["t"] = Convert.ToInt32(c.tipo_id);

                        return RedirectToAction("Index_cliente", "Cliente");

                        break;
                    default:
                        return RedirectToAction("Error", "Home");

                        break;


                }



            }
        }
//Registrar usuario al sitio web 
public ActionResult Registro()
{

return View();


}
//Registrar usuario al sitio web controlador y formulario tipo de usuario 

public ActionResult Nusuariosvc()
{

            //Registrar el usuario 

            var rut = Convert.ToString(Request.Form["rut"]);
            var nombre = Convert.ToString(Request.Form["nombre"]);
            var apellido = Convert.ToString(Request.Form["apellido"]);
            var direccion = Convert.ToString(Request.Form["direccion"]);
            var comuna = Convert.ToString(Request.Form["comuna"]);
            var ciudad = Convert.ToString(Request.Form["ciudad"]);
            var correo = Convert.ToString(Request.Form["correo"]);
            var telefono = Convert.ToString(Request.Form["telefono"]);


            //Registrar usuario 

            cliente c = new cliente
            {
                rut=rut,
                nombre=nombre,
                apellido=apellido,
                direccion=direccion,
                comuna=comuna,
                ciudad=ciudad,
                correo=correo,
                telefono=telefono
            };
            dbc.clientes.Add(c);
            dbc.SaveChanges();
            //Hacer tempdata 
            TempData["r"] = Convert.ToString(rut);


            //return RedirectToAction("Ncliente_usuario", "Home");
            return View("Ncliente_usuario");

}

//Registro cliente usuario controlador 
public ActionResult Ntusuariosvc()
{

var nusuario = Convert.ToString(Request.Form["nusuario"]);
var contraseña = Convert.ToString(Request.Form["contraseña"]);
var i = Convert.ToString(TempData["r"]);

//Registrar cliente tipo 

cliente_tipo ct = new cliente_tipo
{

nombre_usuario = nusuario,
clave = contraseña,
tipo_id = 2,
cliente_id = i


};
dbc.cliente_tipo.Add(ct);
dbc.SaveChanges();

//Hacer las sesiones para transportar datos 
Session["r"] = Convert.ToString(i);
Session["n"] = Convert.ToString(nusuario);
Session["t"] = Convert.ToInt32(2);


return RedirectToAction("Index_cliente", "Cliente");


}
//


    }
}