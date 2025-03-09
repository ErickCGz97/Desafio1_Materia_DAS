using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;


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
        public void RegistrarCliente()
        {
            Console.WriteLine("Ingrese el ID del cliente: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el nombre del cliente: ");
            string nombre = Console.ReadLine();

            Console.WriteLine("Ingrese la direccion del cliente: ");
            string direccion = Console.ReadLine();

            Console.WriteLine("Ingrese el numero de telefono del cliente: ");
            string numeroTelefono = Console.ReadLine();

            Cliente cliente = new Cliente
            { Id = id, Nombre = nombre, Direccion = direccion, NumeroTelefono = numeroTelefono };

            clientes.Add(cliente);
        }

        //Mostrar el stock de productos disponibles
        public void MostrarProductos()
        {
           for (int i = 0; i < stockProductos.GetLength(0); i++)
            {
                for (int j = 0; j < stockProductos.GetLength(1); j++)
                {
                    if (stockProductos[i,j] != null)
                    {
                        Console.WriteLine("Categoria: " + i + ", Posicion: " + j + ", Producto: " + stockProductos[i, j].Nombre + ", Cantidad: " + stockProductos[i, j].CantidadStock);
                    }
                }
            }
        }

        //Realizar venta
        public void RealizarVenta()
        {
            Console.WriteLine("Ingrese el Id de la venta: ");
            int idVenta = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el Id del cliente: ");
            int idCliente = int.Parse(Console.ReadLine());
            Cliente cliente = clientes.FirstOrDefault(c => c.Id == idCliente);

            //Verificar si el cliente con el ID si existe o no
            if(cliente == null)
            {
                Console.WriteLine("Cliente no encontrado con el Id");
                return;
            }
            //Si el cliente existe, proceder a realizar la venta

            Dictionary<Producto, int> productosComprados = new Dictionary<Producto, int>();

            while(true)
            {
                Console.WriteLine("Ingrese el ID del producto (Opcion -1 para detener la venta): ");
                int idProducto = int.Parse(Console.ReadLine());
                //Cerrar ciclo
                if (idProducto == -1) break;

                //Inciar objeto producto por defecto con valor null
                Producto producto = null;

                for(int i = 0; i<stockProductos.GetLength(0); i++)
                {
                    for(int j = 0; j<stockProductos.GetLength(1); j++)
                    {
                        if (stockProductos[i,j] != null && stockProductos[i,j].Id == idProducto)
                        {
                            producto = stockProductos[i, j];
                            break;
                        }
                    }
                }
                //Verificar si el producto esta en stock
                if(producto == null)
                {
                    Console.WriteLine("Producto no encontrado");
                    continue;
                }
                //Si el producto existe, permitir hacer la venta
                Console.WriteLine("Ingrese la cantidad a comprar: ");
                int cantidad = int.Parse(Console.ReadLine());

                //Verificar que hay suficiente cantidad de producto para la venta
                if(cantidad > producto.CantidadStock)
                {
                    Console.WriteLine("Cantidad en stock insuficiente para la venta");
                    continue;
                }
                productosComprados[producto] = cantidad;
            }

            //Proceder a registrar la venta
            Venta venta = new Venta
            {
                Id = idVenta,
                Cliente = cliente,
                ProductosComprados = productosComprados,
                //Sum permite sumar todos los resultados de multiplicar el precio por la cantidad de producto
                TotalPago = productosComprados.Sum(p => p.Key.Precio * p.Value)
            };
            dictionaryVentas.Add(idVenta, venta);

            //Ciclo para actualizar el inventario despues de realizar una venta
            foreach (var item in productosComprados)
            {
                item.Key.CantidadStock -= item.Value;
            }
        }

        //Consultar ventas
        public void ConsultarVentas()
        {
            foreach(var venta in dictionaryVentas.Values)
            {
                Console.WriteLine("Venta Id: " + venta.Id + ", Cliente: " + venta.Cliente.Nombre + ", Total a pagar: " + venta.TotalPago);
            }
        }

        //  Eliminar una venta y devolver productos al stock
        public void EliminarVenta()
        {
            Console.WriteLine("Ingrese el ID de la venta a eliminar:");
            int idVenta = int.Parse(Console.ReadLine());

            if (dictionaryVentas.TryGetValue(idVenta, out Venta venta))
            {
                foreach (var item in venta.ProductosComprados)
                {
                    item.Key.CantidadStock += item.Value;
                }
                dictionaryVentas.Remove(idVenta);
            }
            else
            {
                Console.WriteLine("Venta no encontrada.");
            }
        }

        public void CrearGraficoStock()
        {
            Chart chart = new Chart();
            chart.ChartAreas.Add(new ChartArea("MainArea"));

            Series series = new Series("Stock");
            series.ChartType = SeriesChartType.Bar;

            for (int i = 0; i < stockProductos.GetLength(0); i++)
            {
                for (int j = 0; j < stockProductos.GetLength(1); j++)
                {
                    if (stockProductos[i, j] != null)
                    {
                        series.Points.AddXY(stockProductos[i, j].Nombre, stockProductos[i, j].CantidadEnStock);
                    }
                }
            }

            chart.Series.Add(series);
            chart.SaveImage("grafico_stock.png", ChartImageFormat.Png);
            Console.WriteLine("Gráfico de stock guardado como 'grafico_stock.png'.");
        }
    }
}
