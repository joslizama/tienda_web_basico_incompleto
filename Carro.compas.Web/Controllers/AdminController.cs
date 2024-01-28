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
using System.Web.Helpers;

namespace Carro.compas.Web.Controllers
{
    public class AdminController : Controller
    {
        Conexiones dbc = new Conexiones(); 

        // GET: Admin
        public ActionResult Index_admin()
        {
            return View();
        } 
        //Listado de cateogias 
        public ActionResult Lcategorias()
        {
            var lc = dbc.categorias.ToList();

            return View(lc); 

        }
     //Nueva categoria vista 
     public ActionResult Ncategorias()
     {
            return View();
     } 
    //Nueva categoria controlador 
    [HttpPost] 
        public JsonResult Ncategoriasc()
        {
            //Capurar variables e ingresar la categoria 

            var nombre = Convert.ToString(Request.Form["nombre"]);

            categoria c = new categoria
            {
                nombre=nombre
            };
            dbc.categorias.Add(c);
            dbc.SaveChanges();

            return Json(JsonRequestBehavior.AllowGet);
        }
 //Modificar categoria vista 
 public ActionResult Mcategorias(int id)
 {
             
   if(id == null)
   {
    return RedirectToAction("Error_admin", "Admin");

   }else{

   var mcat = dbc.categorias.Find(id);
   TempData["categorias"] = Convert.ToInt32(mcat.id); 
                
   if(mcat == null)
   {
    return RedirectToAction("Error_admin", "Admin");

   }else{

   return View(mcat);


   }

   }        
 } 
//Modificar categorias controlador 
[HttpPost] 
public JsonResult Mcategoriasc()
{
      var i = Convert.ToInt32(TempData["categorias"]);
      var nombre = Convert.ToString(Request.Form["nombre"]);

      var c = dbc.categorias.Where(p => p.id == i).SingleOrDefault();

       if(c == null)
       {

       }else{

        c.nombre = nombre;
        dbc.SaveChanges();


       }
     return Json(JsonRequestBehavior.AllowGet);
}
//Eliminar categoria 
public ActionResult Ecategorias(int id)
{

if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else{

var ec = dbc.categorias.Find(id);

var e = Convert.ToInt32(ec.id);


if(ec == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

//Verificar en producto para modificar el registro 

var c = dbc.productoes.Where(p => p.categoria_id == e).SingleOrDefault(); 


if(c == null)
{

//Eliminar categoria 
dbc.categorias.Remove(ec);
dbc.SaveChanges();

return RedirectToAction("Lcategorias", "Admin");



}else{

//Modificar categoria y eliminar categoria 
c.categoria_id = 1;
dbc.SaveChanges();

//Eliminar categoria 
dbc.categorias.Remove(ec);
dbc.SaveChanges();

return RedirectToAction("Lcategorias", "Admin");


}
}
}
}
//Listado de subcategorias 
public ActionResult Lsubcategorias()
{

var ls = dbc.sub_categorias.ToList();

return View(ls); 

}
//Nueva sub categoria vista 
public ActionResult Nsubcategorias()
{
 return View();

}
//Nueva subcategoria controlador 
[HttpPost] 
public JsonResult Nsubcategoriac()
{

  var nombre = Convert.ToString(Request.Form["nombre"]);


     sub_categorias sc = new sub_categorias
     {
     nombre=nombre
     };
     dbc.sub_categorias.Add(sc);
     dbc.SaveChanges();


     return Json(JsonRequestBehavior.AllowGet);
}
//Modificar subcategorias vista 
public ActionResult Mscategorias(int id)
{

 if(id == null)
 {
  return RedirectToAction("Error_admin", "Admin");

 }else{

  var mscategorias = dbc.sub_categorias.Find(id);
  TempData["mscategorias"] = Convert.ToInt32(mscategorias.id); 

  if(mscategorias == null)
  {
   return RedirectToAction("Error_admin", "Admin");

  }else{

   return View(mscategorias);


  }
}
}
//Modificar subcategorias controlador 
[HttpPost] 
public JsonResult Mscategoriasc()
{

 var i = Convert.ToInt32(TempData["mscategorias"]);
 var nombre = Convert.ToString(Request.Form["nombre"]);

 var c = dbc.sub_categorias.Where(p => p.id == i).SingleOrDefault(); 

 if(c == null)
 {
  Response.Redirect("~/Admin/Error_admin");

 }else{

 c.nombre = nombre;
 dbc.SaveChanges(); 



  }
 return Json(JsonRequestBehavior.AllowGet);
}
//Eliminar subcategorias 
public ActionResult Escategorias(int id)
{
 
if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else{

var escat = dbc.sub_categorias.Find(id);

var e = Convert.ToInt32(escat.id);

//Modificar subcategoria de producto 
var mp = dbc.productoes.Where(p => p.subcategoria_id == e).SingleOrDefault();

if(mp == null)
{

//Eliminar subcategoria 

dbc.sub_categorias.Remove(escat);
dbc.SaveChanges();

return RedirectToAction("Lsubcategorias", "Admin");

}else{

//Modificar subcategoria del producto 
mp.subcategoria_id = 1;
dbc.SaveChanges();

//Eliminar subcategoria 

dbc.sub_categorias.Remove(escat);
dbc.SaveChanges();

return RedirectToAction("Lsubcategorias", "Admin");


}
}
}
//Listado de marcas 
public ActionResult Lmarcas()
{
var lm = dbc.marcas.ToList();

return View(lm);


}
//Nueva marca vista 
public ActionResult Nmarcas()
{
return View(); 

}
//Nueva marca controlador 
[HttpPost]

public JsonResult Nmarcac()
{
var nombre = Convert.ToString(Request.Form["nombre"]);

marca m = new marca
{
nombre = nombre
};

dbc.marcas.Add(m);
dbc.SaveChanges();


return Json(JsonRequestBehavior.AllowGet); 

}
//Modificar marca vista 
public ActionResult Mmarcas(int id)
{
   
 if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else{

var mmarcas = dbc.marcas.Find(id);
TempData["marcas"] = Convert.ToInt32(mmarcas.id); 

if(mmarcas == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

return View(mmarcas); 

}
}
}
//Modiifcar marcas controlador 
[HttpPost] 
public JsonResult Mmarcasc()
{
var nombre = Convert.ToString(Request.Form["nombre"]);
var i = Convert.ToInt32(TempData["marcas"]);

var c = dbc.marcas.Where(p => p.id == i).SingleOrDefault(); 

if(c == null)
{
Response.Redirect("~/Admin/Error_admin");

}else{

c.nombre = nombre;
dbc.SaveChanges(); 


}
return Json(JsonRequestBehavior.AllowGet);
}
//Eliminar marcas 
 public ActionResult Emarcas(int id)
{

if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else{

var emarcas = dbc.marcas.Find(id);
var e = Convert.ToInt32(emarcas.id); 

if(emarcas == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

//Modificar marca del producto 
var c = dbc.productoes.Where(p => p.marca_id == e).SingleOrDefault();

if(c == null)
{
//Eliminar marca 
dbc.marcas.Remove(emarcas);
dbc.SaveChanges();

return RedirectToAction("Lmarcas", "Admin");

}else{

//Modificar marcas de producto 

c.marca_id = 1;
dbc.SaveChanges();

//Eliminar marca 
dbc.marcas.Remove(emarcas);
dbc.SaveChanges();

return RedirectToAction("Lmarcas", "Admin");

}
}
}
}
//Listado de productos 
public ActionResult Lproductos()
{

var lp = dbc.productoes.ToList();

return View(lp); 

}
//Mostrar la imagen del producto 
public ActionResult GetImagen(int id)
{

if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else{

//Obterer los datos de la imagen 
byte[] imagenes = dbc.productoes.Find(id).imagen;

//Obtener el espacio de memoria 
MemoryStream ms = new MemoryStream(imagenes);
//Obtener la imagen fisica 
 Image i = Image.FromStream(ms);

ms = new MemoryStream();

i.Save(ms,ImageFormat.Jpeg);
ms.Position = 0;

return File(ms, "Image/Jpg"); 
                





}
}
//Nuevo producto vista 
public ActionResult Nproductos()
{
var i = Convert.ToInt32(1);

TempData["cat"] = dbc.categorias.SqlQuery("select * from categorias where id !='" + i + "'").ToList(); 

TempData["scatcat"] = dbc.sub_categorias.SqlQuery("select * from sub_categorias where  id !='" + i + "'").ToList();  

TempData["mar"] = dbc.marcas.SqlQuery("select * from marcas where id !='" + i + "'").ToList();

return View(); 



}
//Nuevo producto controlador 
public ActionResult Nproductoc()
{

var nombre = Convert.ToString(Request.Form["nombre"]);
HttpPostedFileBase imagen = Request.Files[0];
var descripcion = Convert.ToString(Request.Form["descripcion"]);
var precio = Convert.ToDouble(Request.Form["precio"]);
var stock = Convert.ToInt32(Request.Form["stock"]);
var categorias = Convert.ToInt32(Request.Form["categorias"]);
var scategorias = Convert.ToInt32(Request.Form["scategorias"]);
var marcas = Convert.ToInt32(Request.Form["marcas"]);

//Convertir la imagen en bytes 
WebImage i = new WebImage(imagen.InputStream);

//Agregar la imagen a la base de datos 
producto p = new producto
{
nombre = nombre,
descripcion = descripcion,
imagen = i.GetBytes(),
precio=precio,
stock=stock,
categoria_id=categorias,
subcategoria_id=scategorias,
marca_id = marcas
};
dbc.productoes.Add(p);
dbc.SaveChanges();

return RedirectToAction("Lproductos", "Admin"); 



} 
//Detalles del producto vista 
public ActionResult Dproducto(int id)
{

if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else{

var dproducto = dbc.productoes.Find(id);

var i = Convert.ToInt32(1);

TempData["cat"] = dbc.categorias.SqlQuery("select * from categorias where id !='" + i + "'").ToList();

TempData["scatcat"] = dbc.sub_categorias.SqlQuery("select * from sub_categorias where  id !='" + i + "'").ToList();

TempData["mar"] = dbc.marcas.SqlQuery("select * from marcas where id !='" + i + "'").ToList();

if (dproducto == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

return View(dproducto); 

}
}
}
//Modificar producto vista 
public ActionResult Mproducto(int id)
{

if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else{

var mp = dbc.productoes.Find(id);
TempData["prod"] = Convert.ToInt32(mp.id); 

if(mp == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

//Tempdata para mostrar los listados 

TempData["cat"] = dbc.categorias.SqlQuery("select * from categorias ").ToList();

TempData["scatcat"] = dbc.sub_categorias.SqlQuery("select * from sub_categorias").ToList();

TempData["mar"] = dbc.marcas.SqlQuery("select * from marcas").ToList();

return View(mp); 

}
}
}
//Modificar producto controlador 
public ActionResult Mproductoc()
{
 var i = Convert.ToInt32(TempData["prod"]);
 var nombre = Convert.ToString(Request.Form["nombre"]);
 HttpPostedFileBase imagen = Request.Files[0];
 var descripcion = Convert.ToString(Request.Form["descripcion"]);
 var precio = Convert.ToDouble(Request.Form["precio"]);
 var stock = Convert.ToInt32(Request.Form["stock"]);
 var categorias = Convert.ToInt32(Request.Form["categorias"]);
 var scategorias = Convert.ToInt32(Request.Form["scategorias"]);
 var marcas = Convert.ToInt32(Request.Form["marcas"]);

 //Verificar si la imagen existe 
 if(imagen == null)
 {
 

 //Hacer la consulta a la base de datos 
 var c = dbc.productoes.Where(p => p.id == i).SingleOrDefault(); 

 if(c == null)
 {

 //Modificar los datos del producto
 c.nombre = nombre;
 c.descripcion = descripcion;
 c.precio = precio;
 c.stock = stock;
 c.categoria_id = categorias;
 c.subcategoria_id = scategorias;
 c.marca_id = marcas;

 dbc.SaveChanges();
 return RedirectToAction("Lproductos", "Admin");



 }else{

 return RedirectToAction("Error_admin", "Admin"); 


 }


 }else if(imagen != null){

//Hacer las consultas a la base de datos 

var c2 = dbc.productoes.Where(p => p.id == i).SingleOrDefault();

//Imagen a bytes 
WebImage im = new WebImage(imagen.InputStream);

//Modificar el producto  
c2.nombre = nombre;
c2.imagen = im.GetBytes();
c2.descripcion = descripcion;
c2.precio = precio;
c2.stock = stock;
c2.categoria_id = categorias;
c2.subcategoria_id = scategorias;
c2.marca_id = marcas;

dbc.SaveChanges();
return RedirectToAction("Lproductos", "Admin");

}
return View();
}
//Eliminar producto 
public ActionResult Eproducto(int id)
{

if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else{

var ep = dbc.productoes.Find(id); 

if(ep == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

dbc.productoes.Remove(ep);
dbc.SaveChanges();

return RedirectToAction("Lproductos", "Admin"); 

}
}
}
//Listado de opiniones 
public ActionResult Lopiniones()
{
var lopinion = dbc.opinions.Include("producto").ToList();

return View(lopinion); 



}
//Detalle de opiniones 
public ActionResult Dopinion(int id)
{

if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else{

var dopinion = dbc.opinions.Find(id);
TempData["productos"] = dbc.productoes.ToList();
TempData["Usuarios"] = dbc.cliente_tipo.ToList();



if(dopinion == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

return View(dopinion); 

}
}
}
//Eliminar opinion 
public ActionResult Eopinion(int id)
{ 

if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else{

var e = dbc.opinions.Find(id); 

if(e == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

dbc.opinions.Remove(e);
dbc.SaveChanges();

return RedirectToAction("Lopiniones", "Admin");



}
}
}
//Listado de clientes 
public ActionResult Lclientes()
{

var r = Convert.ToString("1-1");

var c = dbc.clientes.SqlQuery("SELECT * FROM cliente WHERE rut != '"  + r + "'").ToList();

return View(c); 



}
//Detalles de clientes 
public ActionResult Dcliente(string id)
{

if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else{

var d = dbc.clientes.Find(id); 

if(d == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

return View(d); 

}
}
}
//Eliminar cliente 
public ActionResult Ecliente(string id)
{
if(id == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

var e = dbc.clientes.Find(id);
var etc = Convert.ToString(e.rut);

if(e == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

//Eliminar el cliente_tipo 

var c = dbc.cliente_tipo.Where(p => p.cliente_id.Equals(etc)).SingleOrDefault();

var c2 = Convert.ToInt32(c.id); 

if(c2 == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

var c3 = dbc.cliente_tipo.Find(c2);
//Eliminar cliente tipo 
dbc.cliente_tipo.Remove(c3);
dbc.SaveChanges();
//Eliminar cliente 
dbc.clientes.Remove(e);
dbc.SaveChanges();

return RedirectToAction("Lcientes", "Admin");

}
}
}
} 
//Mostrar usuario cliente 
public ActionResult Ucliente(string id)
{

if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else {

var bc = dbc.clientes.Find(id); 

if(bc == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{
var d = Convert.ToString(bc.rut);
//Hacer consulta a la base de datos de cliente_tipo 

var c = dbc.cliente_tipo.Where(p => p.cliente_id.Equals(d)).SingleOrDefault(); 

if(c == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

var f = dbc.cliente_tipo.Find(c.id); 

if(f == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

TempData["tusuario"] = dbc.tipo_usuario.ToList();


return View(f);

}
}
}
}
}
//Listado de cliente_tipo 
public ActionResult LUcliente()
{

var lusuariosclientes = dbc.cliente_tipo.Include("tipo_usuario").ToList();

return View(lusuariosclientes); 



}
//Detalles del cliente 
public ActionResult Dcliente2(int id)
{

if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else{

var dc2 = dbc.cliente_tipo.Find(id); 

if(dc2 == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

var c = dbc.clientes.Where(p => p.rut.Equals(dc2.cliente_id)).SingleOrDefault();

if(c == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

var d = dbc.clientes.Find(c.rut);

return View(d);



}
}
}
}
//Eliminar usuario cliente 
public ActionResult EUcliente(int id)
{

if(id == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

var euclientes = dbc.cliente_tipo.Find(id);
var e = Convert.ToString(euclientes.cliente_id);

if(euclientes == null || e == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

//Eliminar clientes 

var c = dbc.clientes.Where(p => p.rut.Equals(e)).SingleOrDefault();

var r = Convert.ToString(c.rut);

if(c == null || r == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{


var p = dbc.clientes.Find(r);

dbc.clientes.Remove(p);
dbc.SaveChanges();

//Eliminar clietne_tipo 
dbc.cliente_tipo.Remove(euclientes);
dbc.SaveChanges();

return RedirectToAction("LUcliente", "Admin");



}
}
}
}
//Listado de tipo de usuarios 
public ActionResult LTusuario()
{

var lt = dbc.tipo_usuario.ToList();

return View(lt);

}
//Nuevo tipo de usuario vista 
public ActionResult NTusuario()
{

return View();

}
//Nuevo tipo de usuario controlador 
[HttpPost] 
public JsonResult NTusuarioc()
{

var nombre = Convert.ToString(Request.Form["nombre"]);

tipo_usuario ti = new tipo_usuario
{
nombre=nombre
};
dbc.tipo_usuario.Add(ti);
dbc.SaveChanges();

return Json(JsonRequestBehavior.AllowGet);


}
//Modificar tipo de usuario vista 
public ActionResult Mtusuario(int id)
{

if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else{

var musuario = dbc.tipo_usuario.Find(id);
TempData["musuario"] = Convert.ToInt32(musuario.id);

if(musuario == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

return View(musuario);


}
}
} 
//Modificar tipo de usuario controlador 
[HttpPost]
public JsonResult Mtusuarioc()
{

var i = Convert.ToInt32(TempData["musuario"]);
var nombre = Convert.ToString(Request.Form["nombre"]);

var c = dbc.tipo_usuario.Where(p => p.id == i).SingleOrDefault();

if(c == null)
{
Response.Redirect("~/Admin/Error_admin");

}else{

//Modificar tipo de usuario 
c.nombre = nombre;
dbc.SaveChanges();
}
return Json(JsonRequestBehavior.AllowGet);
}
//Eliminar tipo de usuario 
public ActionResult Etusuario(int id)
{

if(id == null)
{
return RedirectToAction("Error_admin", "Admin");

}else{

var et = dbc.tipo_usuario.Find(id);

var et2 = Convert.ToInt32(et.id);

if(et == null)
{
return RedirectToAction("Error_admin", "Admin");
}else{

var c = dbc.cliente_tipo.Where(p => p.tipo_id == et2).SingleOrDefault();

if(c == null)
{
//Eliminar tipo de usuario 
dbc.tipo_usuario.Remove(et);
dbc.SaveChanges();

return RedirectToAction("LTusuario", "Admin");

}else{

//Modificar el tipo de usuario en usuario_tipo 

c.tipo_id = 3;
dbc.SaveChanges();

//Eliminar tipo de usuario 
dbc.tipo_usuario.Remove(et);
dbc.SaveChanges();

return RedirectToAction("LTusuario", "Admin");

}
}
}
}
//Cerrar sesion 
public ActionResult Csesion()
{

var r = Convert.ToString(Session["rut"] = null);
var t = Convert.ToInt32(Session["tipo"] = null);

if( r == null && t == null)
{

 Session.Abandon();

return RedirectToAction("Index", "Home");


}else{

return RedirectToAction("Error_admin", "Admin");

}
} 
//



    }
}