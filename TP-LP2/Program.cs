using sol_greedy_dinamica;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Timers;

namespace sol_greedy_dinamica
{
    //constantes
    static class Constants
    {
        public const int max_index = 10000;
        public const int volumen_electro_pequeños = 50; //cambiar volumen real
        public const int televisores =2*1*2; //cambiar volumen real
        public const int volumen_elevador = 50; //cambiar volumen real
        public const int peso_elevador = 50; //cambiar volumen real

    }

    //enums 
    public enum eLocalidad { Liniers, TresdeFebrero, SanMartin, VicenteLopez, LaMatanza, LomasdeZamora, Lanus, Avellaneda, Versalles, VillaLuro, Mataderos, MonteCastro, VelezSarsfield, ParqueAvellaneda, VillaLugano, VillaDevoto, VillaUrquiza, Belgrano, Palermo, Retiro, Caballito, Flores, PuertoMadero, LaBoca, Chacarita };
    public enum eOpcion { se_lleno_completo_, se_lleno_pero_quedaron_cosas_de_la_localidad, no_se_lleno };
    public enum entrega { express, normal, diferido };
    public enum objetos { licuadora, exprimidor, rallador, tostadora, cafetera, molinillos, cocinas, calefon, termotanque, lavarropas, secarropas, heladera, microondas, freezer, computadoras, impresoras, accesorios, telvisores };
    public enum eTipoProducto { linea_blanca, pequeños_electrodomesticos, electronicos, televisores }


}

internal class Program
{
 

    static void Main(string[] args)
    {
        //cVehiculo camioneta = new cCamioneta(80, 2,5770000);
        //cVehiculo furgon = new cFurgon(90, 4,3950000);
        //cVehiculo furgoneta = new cFurgoneta(60, 9,2800000);

        //List<cVehiculo> lista_camiones_definitiva = new List<cVehiculo>() { camioneta, furgon, furgoneta };

        //List<cVehiculo> lista_de_camiones_diaria = new List<cVehiculo>();
        //DateTime dia_hoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        //// zonaDias = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day, 0, 0, 1);
        //lista_de_camiones_diaria = camiones_disponibles(dia_hoy, lista_camiones_definitiva);




        //electrodomesticos cafetera = new cafetera(2, 4, "philips", 2, eTipoProducto.linea_blanca);
        //List<electrodomesticos> lista_taylor = new List<electrodomesticos>();
        //lista_taylor.Add(cafetera(12,1,"marca",7));
        //lista_taylor.Add(objetos.computadoras);
        //lista_taylor.Add(objetos.exprimidor);
        //List<objetos> lista_sabrina= new List<objetos>();
        //lista_sabrina.Add(objetos.licuadora);
        //lista_sabrina.Add(objetos.secarropas);
        //List<objetos> lista_olivia = new List<objetos>();
        //lista_olivia.Add(objetos.tostadora);
        //lista_olivia.Add(objetos.cafetera);
        //lista_olivia.Add(objetos.cocinas);
        //lista_olivia.Add(objetos.heladera);
        //List<objetos> lista_harry = new List<objetos>();
        //lista_harry.Add(objetos.computadoras);
        //lista_harry.Add(objetos.accesorios);
        //List<objetos> lista_louis = new List<objetos>();
        //lista_louis.Add(objetos.impresoras);
        //lista_louis.Add(objetos.accesorios);
        //lista_louis.Add(objetos.cafetera);
        //List<objetos> lista_ricardo = new List<objetos>();
        //lista_ricardo.Add(objetos.cocinas);
        //lista_ricardo.Add(objetos.rallador);
        //lista_ricardo.Add(objetos.telvisores);
        //lista_ricardo.Add(objetos.telvisores);
        //pedido_por_cliente pedido1 = new pedido_por_cliente("Taylor Swift", eLocalidad.VicenteLopez,lista_taylor , entrega.express);
        //pedido_por_cliente pedido2 = new pedido_por_cliente("Sabrina Carpenter", eLocalidad.LaBoca,lista_sabrina , entrega.diferido);
        //pedido_por_cliente pedido3 = new pedido_por_cliente("Olivia Rodrigo", eLocalidad.Palermo, lista_olivia, entrega.normal);
        //pedido_por_cliente pedido4 = new pedido_por_cliente("Harry Styles", eLocalidad.Caballito,lista_harry , entrega.express);
        //pedido_por_cliente pedido5 = new pedido_por_cliente("Louis Tomlinson", eLocalidad.Chacarita,lista_louis , entrega.normal);
        //pedido_por_cliente pedido6 = new pedido_por_cliente("Ricardo Fort", eLocalidad.PuertoMadero,lista_ricardo , entrega.express);
        // System.Threading.Thread.Sleep(5);


        
        //int dia_hoy_chequeo = DateTime.Now.Day;
        //if(dia_hoy_chequeo==1)//si hoy es 1, es que empezo un nuevo mes
        //{
        //    sumar_mes_camiones(lista_camiones_definitiva);
        //}

    }
}
