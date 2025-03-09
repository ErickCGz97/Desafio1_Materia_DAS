using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1_SistemaGestiónInventarioTienda
{
    class Procesos
    {
        private Producto[,] stockProductos = new Producto[5, 10];
        private List<Cliente> clientes = new List<Cliente>();
        private Dictionary<int, Venta> dictionaryVentas = new Dictionary<int, Venta>();

        //Registrar un nuevo producto
        public void RegistrarProducto()
        {
            Console.WriteLine("Ingrese el Id del producto: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el nombre del producto: ");
            string nombre = Console.ReadLine();

            Console.WriteLine("Ingrese la categoria del producto: ");
            string categoria = Console.ReadLine();

            Console.WriteLine("Ingrese el precio del producto: ");
            decimal precio = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese la cantidad en stock del producto: ");
            int cantidadStock = int.Parse(Console.ReadLine());

            Producto producto = new Producto
            {
                Id = id,
                Nombre = nombre,
                Categoria = categoria,
                Precio = precio,
                CantidadStock = cantidadStock
            };

            Console.WriteLine("Ingrese la categoria (0-4) y la posicion (0-9) para almacenar el producto: ");
            int categoriaProducto = int.Parse(Console.ReadLine());
            int posicionProducto = int.Parse(Console.ReadLine());

            stockProductos[categoriaProducto, posicionProducto] = producto;
        }

        //Registrar un nuevo cliente
        public void RegistrarCliente(Cliente cliente)
        {
            clientes.Add(cliente);
        }

        //Mostrar el stock de productos disponibles
        public void MostrarProductos()
        {
           
        }

        //Realizar venta
        public void RealizarVenta(int idVenta, Cliente cliente, Dictionary<Producto, int> productosComprados)
        {
            //Objeto tipo clase Venta
            Venta venta = new Venta
            {
                Id = idVenta,
                Cliente = cliente,
                ProductosComprados = productosComprados,
                //Expresion lambda
                TotalPago = productosComprados.Sum(p => p.Key.Precio * p.Value)
                /*
                 * productosComprados.Sum(p => p.Key.Precio * p.Value) : Sum permite sumar los valores que contendra la expresion
                    en este caso, accede a obtener el precio del objeto Producto y multitplicarlo por la cantidad del producto
                 */
            };

        }
    }
}
