using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1_SistemaGestiónInventarioTienda
{
    class Venta
    {
        
        public int Id { get; set; }
        public Cliente Cliente { get; set; }

        /*
         *Dictionary relaciona los productos comprados con la cantidad del producto
         */
        public Dictionary<Producto, int> ProductosComprados { get; set; }

        public decimal TotalPago { get; set; }

        //Constructor de la clase
        public Venta()
        {
            //El constructor crea un diccionario (Dictionary para almacenar la compra hecha por el cliente
            ProductosComprados = new Dictionary<Producto, int>();
        }

    }
}
