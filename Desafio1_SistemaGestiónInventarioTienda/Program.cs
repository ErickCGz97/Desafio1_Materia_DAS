

using Desafio1_SistemaGestiónInventarioTienda;

public class Program
{
    public static void Main(string[] args)
    {
        Procesos procesos = new Procesos();
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. Registrar Producto");
            Console.WriteLine("2. Registrar Cliente");
            Console.WriteLine("3. Mostrar Stock");
            Console.WriteLine("4. Realizar Venta");
            Console.WriteLine("5. Consultar Ventas");
            Console.WriteLine("6. Eliminar Venta");
            Console.WriteLine("7. Salir");

            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    procesos.RegistrarProducto();
                    break;
                case 2:
                    procesos.RegistrarCliente();
                    break;
                case 3:
                    procesos.MostrarProductos();
                    break;
                case 4:
                    procesos.RealizarVenta();
                    break;
                case 5:
                    procesos.ConsultarVentas();
                    break;
                case 6:
                    procesos.EliminarVenta();
                    break;
                case 7:
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }
}