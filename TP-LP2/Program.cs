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
        cVehiculo camioneta = new cCamioneta(2,80,5770000);
        cVehiculo furgon = new cFurgon(4,90, 3950000);
        cVehiculo furgoneta = new cFurgoneta(9,60, 2800000);

        List<cVehiculo> lista_camiones = new List<cVehiculo>() { camioneta, furgon, furgoneta };

        //List<cVehiculo> lista_de_camiones_diaria = new List<cVehiculo>();
        //DateTime dia_hoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        //// zonaDias = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day, 0, 0, 1);
        //lista_de_camiones_diaria = camiones_disponibles(dia_hoy, lista_camiones_definitiva);


        cElectrodomesticos licuadora = new cPequeños_electrodomesticos(30, 4, 2, objetos.licuadora);
        cElectrodomesticos rallador = new cPequeños_electrodomesticos(10, 1, 2, objetos.rallador);
        cElectrodomesticos exprimidor= new cPequeños_electrodomesticos(10, 3, 2, objetos.exprimidor);
        cElectrodomesticos cafetera = new cPequeños_electrodomesticos(50, 4, 2, objetos.cafetera);
        cElectrodomesticos tostadora = new cPequeños_electrodomesticos(60, 5, 2, objetos.tostadora);
        cElectrodomesticos cocina = new cLineaBlanca(70, 40, 4, objetos.cocinas);
        cElectrodomesticos calefon = new cLineaBlanca(90, 30, 2, objetos.calefon);
        cElectrodomesticos termotanque = new cLineaBlanca(80, 20, 3, objetos.termotanque);
        cElectrodomesticos lavarropas = new cLineaBlanca(40, 60, 1, objetos.lavarropas);
        cElectrodomesticos secarropas = new cLineaBlanca(30, 60, 2, objetos.lavarropas);
        cElectrodomesticos comptadora = new cElectronicos(10, 10, 2, objetos.computadoras);
        cElectrodomesticos impresora = new cElectronicos(20, 20, 3, objetos.impresoras);
        cElectrodomesticos accesorios = new cElectronicos(10, 5, 1, objetos.accesorios);
        cElectrodomesticos televisor = new cTelevisores(10, 30, 2, objetos.telvisores);

        List<cElectrodomesticos> lista_taylor = new List<cElectrodomesticos>();
        lista_taylor.Add(licuadora);
        lista_taylor.Add(televisor);
        lista_taylor.Add(exprimidor);
        List<cElectrodomesticos> lista_sabrina= new List<cElectrodomesticos>();
        lista_sabrina.Add(rallador);
        lista_sabrina.Add(cafetera);
        List<cElectrodomesticos> lista_olivia = new List<cElectrodomesticos>();
        lista_olivia.Add(cocina);
         lista_olivia.Add(termotanque);
        lista_olivia.Add(comptadora);
        lista_olivia.Add(impresora);
        List<cElectrodomesticos> lista_harry = new List<cElectrodomesticos>();
        lista_harry.Add(calefon);
        lista_harry.Add(lavarropas);
        List<cElectrodomesticos> lista_louis = new List<cElectrodomesticos>();
        lista_louis.Add(secarropas);
        lista_louis.Add(accesorios);

        cPedido_por_Cliente pedido1 = new cPedido_por_Cliente("Taylor Swift", eLocalidad.VicenteLopez,lista_taylor , entrega.express);
        cPedido_por_Cliente pedido2 = new cPedido_por_Cliente("Sabrina Carpenter", eLocalidad.LaBoca,lista_sabrina , entrega.express);
        cPedido_por_Cliente pedido3 = new cPedido_por_Cliente("Olivia Rodrigo", eLocalidad.Palermo, lista_olivia, entrega.express);
        cPedido_por_Cliente pedido4 = new cPedido_por_Cliente("Harry Styles", eLocalidad.Caballito,lista_harry , entrega.express);
        cPedido_por_Cliente pedido5 = new cPedido_por_Cliente("Louis Tomlinson", eLocalidad.Chacarita,lista_louis , entrega.express);

        List<cPedido_por_Cliente> lista_pedidos = new List<cPedido_por_Cliente>();
        lista_pedidos.Add(pedido1);
        lista_pedidos.Add(pedido2);
        lista_pedidos.Add(pedido3);
        lista_pedidos.Add(pedido4);
        lista_pedidos.Add(pedido5);

        cCosiMundo cosimundo = new cCosiMundo(lista_pedidos, lista_camiones);

        cosimundo.preparo_y_desapacho_de_productos();

        // System.Threading.Thread.Sleep(5);



        //int dia_hoy_chequeo = DateTime.Now.Day;
        //if(dia_hoy_chequeo==1)//si hoy es 1, es que empezo un nuevo mes
        //{
        //    sumar_mes_camiones(lista_camiones_definitiva);
        //}

    }
}
