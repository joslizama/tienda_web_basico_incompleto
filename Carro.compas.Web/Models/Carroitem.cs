using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Carro.compas.Web.Models; 


namespace Carro.compas.Web.Models
{
    public class Carroitem
    {

        private producto _producto;

        private int _cantidad;




        //Primero constructor sin parametros y luego constructor con parametros

        public Carroitem()
        {

        }



        public producto Producto
        {
            get
            {
                return _producto;
            }

            set
            {
                _producto = value;
            }
        }

        public int Cantidad
        {
            get
            {
                return _cantidad;
            }

            set
            {
                _cantidad = value;
            }
        }


        public Carroitem(producto producto, int cantidad)
        {
            this._producto = producto;
            this._cantidad = cantidad;

        }















    }
}