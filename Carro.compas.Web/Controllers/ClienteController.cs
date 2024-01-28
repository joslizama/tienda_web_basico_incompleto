using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Carro.compas.Web.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Carro.compas.Web.Controllers
{
    public class ClienteController : Controller
    {

        Conexiones dbc = new Conexiones();

        // GET: Cliente
        public ActionResult Index_cliente()
        {
            
            return View();
        } 
    //Listado de productos
    public ActionResult Lprod()
    {
            var lp = dbc.productoes.ToList();

            return View(lp); 

    } 
    //Mostrar la imagen del producto 
    public ActionResult GetImagen(int id)
    {

     if(id == null)
     {
      return RedirectToAction("Error_cliente", "Cliente");

     }else{

      byte[] i = dbc.productoes.Find(id).imagen;

       if(i == null)
       {
        return RedirectToAction("Error_cliente", "Cliente");

       }else{

       //Crear el espacio de memoria 
       MemoryStream ms = new MemoryStream(i);
       //Crear la imagen fisica 
       Image im = Image.FromStream(ms);
       //Inicializar la imagen fisica 
       ms = new MemoryStream();
       //Guardar el formato de imagen 
       im.Save(ms,ImageFormat.Jpeg);
       //Inicializar la posicion del arreglo 
       ms.Position = 0;
       //Devolver la imagen 
       return File(ms,"Image.Jpg");

}
}
}
//Detalle del producto 
public ActionResult Dproducto(int id)
{

if(id == null)
{
return RedirectToAction("Error_cliente", "Cliente");

}else{

var dp = dbc.productoes.Find(id);

var i = Convert.ToInt32(1);

TempData["cat"] = dbc.categorias.SqlQuery("select * from categorias where id !='" + i + "'").ToList();

TempData["scatcat"] = dbc.sub_categorias.SqlQuery("select * from sub_categorias where  id !='" + i + "'").ToList();

TempData["mar"] = dbc.marcas.SqlQuery("select * from marcas where id !='" + i + "'").ToList();


if (dp == null)
{
return RedirectToAction("Error_cliente", "Cliente");
}else{

return View(dp);

}
}
}
//Agregar al carro de compras 
public ActionResult Acarro(int id)
{

if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else{

var bprod = dbc.productoes.Find(id);

if(bprod == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

//Crear una sesion para transportar los datos 

if(Session["carro"] == null)
{

//Crear un listado 
List<Carroitem> compras = new List<Carroitem>();

//Agregar el producto al listado 

compras.Add(new Carroitem(dbc.productoes.Find(bprod.id), 1));


Session["carro"] = compras;



}else{

//para agregar mas productos
//Llamar al  carro de compras  
List<Carroitem> compras = (List<Carroitem>)Session["carro"];
//Crear una variable existente
int existe = Existente(id);
//Verificar si existe una id 
if(existe == -1)
{
//Agregar un nuevo producto al carro de compras 
compras.Add(new Carroitem(dbc.productoes.Find(bprod.id), 1));
Session["carro"] = compras;


}else{
//Sumar un producto con la misma id 
//Arreglo 
compras[existe].Cantidad++;
//Agregar al carro 
Session["carro"] = compras;

}
}
}
}
return View("Acarro");

}
//Verificar si hay producto existente 

private int Existente(int id)
{

//Llamar al carro de compras
List<Carroitem> compras = (List<Carroitem>)Session["carro"];
//Hacer un recorrido al carro de compras para verificar su contenido 

for(int i = 0; i < compras.Count; i++)
{
//Verificar si se repite una id

if(compras[i].Producto.id == id)
 {
 return i;

 }


}
return -1;
}
//Eliminar producto del carro de compras 
public ActionResult EliminarProducto(int id)
{
//Llamar al carro de compras 
List<Carroitem> compras = (List<Carroitem>)Session["carro"];

if (id == null)
{
return RedirectToAction("Error_cliente", "Cliente");

}else{

var ep = dbc.productoes.Find(id);
var ep2 = dbc.productoes.Where(p => p.id == ep.id).SingleOrDefault();

if(ep == null || ep2 == null)
{
return RedirectToAction("Error_cliente", "Cliente");
}else{
                    //Recorrer el carro de compras 
                    for (int j = 0; j < compras.Count; j++)
                    {
                        //Comparar si el producto existe en el listado 
                        if (compras[j].Producto.id == ep2.id)
                        {
                            //Si existe comparar si el producto tiene mas de 1 elemento 

                            if (compras[j].Cantidad > 1)
                            {
                                compras[j].Cantidad = compras[j].Cantidad - 1;
                            }
                            else
                            {
                                //Si el producto tiene 1 elemento 

                                if (compras[j].Cantidad == 1)
                                {
                                    compras.RemoveAt(Existente(id));
                                }
                                else
                                {
                                    return RedirectToAction("Error_cliente", "Cliente");
                                }
                            }

                        }
                    }

                    return View("Acarro");
                }
            }
return View();
}
//Listado de opiniones al producto
public ActionResult Lopinion()
{

var lop = dbc.opinions.Include("producto").ToList();



return View(lop);


}
//Nueva opinion vista 
public ActionResult Nopinion()
{
TempData["Prod"] = dbc.productoes.ToList();

return View(); 


}
//Nueva opinion controlador 
[HttpPost] 
public JsonResult Nopinionc()
{

var i = Convert.ToString(Session["r"]);
var tipoopinion = Convert.ToString(Request.Form["tipoopinion"]);
var descripcion = Convert.ToString(Request.Form["descripcion"]);
var prod = Convert.ToInt32(Request.Form["prod"]);

//Hacer consulta a cliente tipo para tener la id 

var c = dbc.cliente_tipo.Where(p => p.cliente_id.Equals(i)).SingleOrDefault();

if(c == null)
{
Response.Redirect("~/Cliente/Error_cliente");

}else{

//Obtener la id del cliente tipo 
var c2 = Convert.ToInt32(c.id);

//Registrar los datos en la base de datos 

opinion op = new opinion
{
fecha=DateTime.Today,
tipo_opinion=tipoopinion,
descripcion=descripcion,
producto_id = prod,
cliente_tipo_id=c2
};
dbc.opinions.Add(op);
dbc.SaveChanges(); 
}
return Json(JsonRequestBehavior.AllowGet);
}
//Listar por tipo de opinion 
public ActionResult LTopinion()
{

return View();

}
//Listar por opinion controlador 
public ActionResult LTopinionc()
{

var p = Convert.ToString(Request.Form["topinion"]);

var l = dbc.opinions.Where(x => x.tipo_opinion.Equals(p)).SingleOrDefault();
var l2 = dbc.opinions.Where(x => x.tipo_opinion.Equals(p)).Include("producto").ToList();


if (l == null)
{
return RedirectToAction("Error_cliente", "Cliente");

}else{


//var l2 = dbc.opinions.SqlQuery("SELECT * FROM opinion WHERE tipo_opinion='" + p + "'").ToList();

return View(l2);


}
} 
//Terminar compra 
public ActionResult TerminarCompra()
{
//Llamar al carro de compras 
List<Carroitem> compras = (List<Carroitem>)Session["carro"];

//Capturar la sesion del cliente 
var i = Convert.ToString(Session["r"]);

//Verificar que el carro de compras tenga elementos 

if(compras != null && compras.Count>0)
{

////Registrar la venta  

venta v = new venta();

v.fecha = DateTime.Today;
v.iva = 0.19;
v.sub_total = compras.Sum(x => x.Producto.precio * x.Cantidad);
v.total = v.iva + v.sub_total;
v.cliente_id = i;



                //Registrar detalle de la venta 

                v.detalle_ventas = (from item in compras
                                    select new detalle_ventas
                                    {

                                        cantidad = item.Cantidad, 
                                        producto_id=item.Producto.id,
                                        venta_id=v.id



                                    }).ToList();

                dbc.ventas.Add(v);
                dbc.SaveChanges();


return RedirectToAction("Lventascliente", "Cliente");


}else{

return RedirectToAction("Error_cliente", "Cliente");
}
}
//Listado de ventas cliente 
public ActionResult Lventascliente()
{

var i = Convert.ToString(Session["r"]);

var lv = dbc.ventas.SqlQuery("SELECT * FROM ventas WHERE cliente_id = '" + i + "'").ToList();

return View(lv);



}
//Detalle de las ventas 
public ActionResult Dventas(int id)
{

if(id == null)
{
return RedirectToAction("Error_cliente", "Cliente");

}else{

var dv = dbc.ventas.Find(id);

if(dv == null)
{
return RedirectToAction("Error_cliente", "Cliente");
}else{ 

//Obtener la id del detalle de la venta 

var dventa = Convert.ToInt32(dv.id); 

var c = dbc.detalle_ventas.Where(p => p.venta_id == dventa).SingleOrDefault();

if(c == null)
{
return RedirectToAction("Error_cliente", "Cliente");

}else{


var c2 = dbc.detalle_ventas.Find(c.id);

if(c2 == null)
{
return RedirectToAction("Error_cliente", "Cliente");
}else{

var c3 = dbc.detalle_ventas.Where(p => p.id == c2.id).Include("producto").ToList();

return View(c3);

}
}
}
}
}
//


    }
}