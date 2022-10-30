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

namespace sol_greedy_dinamica
{
    //constantes
    static class Constants
    {
        public const int PESOMAXFURGON = 7000;
        public const int PESOMAXFURGONETA = 3500;
        public const int VOLUMEN_CAMIONETA = 1441;//CAMBIAR POR VALOR REAL
        public const int VOLUMEN_FURGON = 3092; //CAMBIAR POR VALOR REAL
        public const int VOLUMEN_FURGONETA = 1233;//CAMBIAR POR VALOR REAL
        public const int max_index = 10000;
        public const int volumen_electro_pequeños = 50; //cambiar volumen real
        public const int volumen_linea_blanca = 50; //cambiar volumen real
        public const int volumen_electronicos = 50; //cambiar volumen real
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

    //clases
    public class pedido_por_cliente
    {
        //atributos
        string nombre_cliente;
        public string nombre
        {
            get { return nombre_cliente; }
        }
        eTipoProducto tipo_producto_compra;
        public eTipoProducto tipo_producto
        {
            get { return tipo_producto_compra; }
            set { }
        }
        List<electrodomesticos> compra;
        public List<electrodomesticos> compra_objetos
        {
            get { return compra; }
            set { }
        }

        int cant_de_objetos;
        public int cantidad_objetos
        {
            get { return cant_de_objetos; }
            set { }
        }
        eLocalidad barrio_a_entregar;
        public eLocalidad barrio
        {
            get { return barrio_a_entregar; }
        }

        int peso_total;
        public int peso_pedido
        {
            get { return peso_total; }
        }
        public int peso_pedido_set
        {
            set { }
        }
        int volumen_total;
        public int volumen
        {
            get { return volumen_total; }
        }
        public int volumen_pedido_set
        {
            set { }
        }

        

        entrega entrega_compra;
        public entrega tipo_entrega
        {
            get { return entrega_compra; }
            set { }
        }
        //metodos
        public pedido_por_cliente(string nombre_, eLocalidad barrio_, List<electrodomesticos> obj , entrega entrega_compra_)//ver del cuaderno de progra1 cmo era lo de id
        {
            nombre_cliente = nombre_;
            barrio_a_entregar = barrio_;
            compra = obj;
            cantidad_objetos = obj.Count;
            volumen_total = calculo_volumen_total(compra);
            peso_total = calculo_peso_total(compra);
            entrega_compra = entrega_compra_;


        }

        int calculo_volumen_total(List<electrodomesticos> compra)
        {
            int suma = 0;
            for (int i = 0; i < compra.Count; i++)
            {
                suma = suma + compra[i].volumenobjeto; //peso elemento(objeto) le paso un objeto y me devuelve su volumen

            }
            return suma;
        }
        int calculo_peso_total(List<electrodomesticos> compra)
        {
            int suma = 0;
            for (int i = 0; i < compra.Count; i++)
            {
                suma = suma + compra[i].pesoobjeto; //peso elemento(objeto) le paso un objeto y me devuelve cuanto pesa
            }
            return suma;
        }
    }

    public class electrodomesticos
    {
       protected eTipoProducto tipo;
        public eTipoProducto tipoobjeto
        {
            get { return tipo; }
            set { }
        }
        protected string marca;
        public string marcaobjeto
        {
            get { return marca; }
            set { }
        }
        protected int garantia;
        public int garantiaobjeto
        {
            get { return garantia; }
            set { }
        }
        protected int peso;
        public int pesoobjeto
        {
            get { return peso; }
            set { }
        }

       protected int volumen;
        public int volumenobjeto
        {
            get { return volumen; }
            set { }
        }

        

    }


    //hijas de electrodomestico
    public class licuadora : electrodomesticos
    {
        public licuadora(int peso_, int volumen_,string marca_,int garan,eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class televisor : electrodomesticos
    {
        public televisor(int peso_, int volumen_, string marca_, int garan,eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class exprimidor : electrodomesticos
    {
        public exprimidor(int peso_, int volumen_, string marca_, int garan,eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class rallador : electrodomesticos
    {
        public rallador(int peso_, int volumen_, string marca_, int garan, eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class tostadora : electrodomesticos
    {
        public tostadora(int peso_, int volumen_, string marca_, int garan, eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class cafetera : electrodomesticos
    {
        public cafetera(int peso_, int volumen_, string marca_, int garan, eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class molinillos : electrodomesticos
    {
        public molinillos(int peso_, int volumen_, string marca_, int garan, eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class cocina: electrodomesticos
    {
        public cocina(int peso_, int volumen_, string marca_, int garan, eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class calefon : electrodomesticos
    {
        public calefon(int peso_, int volumen_, string marca_, int garan, eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class termotanque : electrodomesticos
    {
        public termotanque(int peso_, int volumen_, string marca_, int garan, eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class lavaropa: electrodomesticos
    {
        public lavaropa(int peso_, int volumen_, string marca_, int garan, eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class secarropa : electrodomesticos
    {
        public secarropa(int peso_, int volumen_, string marca_, int garan, eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class heladera : electrodomesticos
    {
        public heladera(int peso_, int volumen_, string marca_, int garan, eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class microondas : electrodomesticos
    {
        public microondas(int peso_, int volumen_, string marca_, int garan, eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class freezer : electrodomesticos
    {
        public freezer(int peso_, int volumen_, string marca_, int garan, eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class computadora : electrodomesticos
    {
        public computadora(int peso_, int volumen_, string marca_, int garan, eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class impresora: electrodomesticos
    {
        public impresora(int peso_, int volumen_, string marca_, int garan, eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    public class accesorio : electrodomesticos
    {
        public accesorio(int peso_, int volumen_, string marca_, int garan, eTipoProducto tipo_)
        {
            peso = peso_;
            volumen = volumen_;
            marca = marca_;
            garantia = garan;
            tipo = tipo_;
        }
    }

    //clase vehiculo
    public class eVehiculo
    {
        protected int nafta;
        public int cant_nafta
        {
            get { return nafta; }
            set { }
        }
        protected int cant_meses_uso;
        public int meses_uso
        {
            get { return cant_meses_uso; }
            set { }
        }

    }

    public class Camioneta:eVehiculo
    {
        public Camioneta(int nafta_,int meses)
        {
            cant_nafta = nafta_;
            cant_meses_uso = meses_uso;
        }
    }

    public class Furgon : eVehiculo
    {
        public Furgon(int nafta_, int meses)
        {
            cant_nafta = nafta_;
            cant_meses_uso = meses_uso;
        }
    }

    public class Furgoneta : eVehiculo
    {
        public Furgoneta(int nafta_, int meses)
        {
            cant_nafta = nafta_;
            cant_meses_uso = meses_uso;
        }
    }

}


internal class Program
{
    //no era consigna
    //GREEDY
    //static void reparto_greedy(List<pedido_por_cliente> pedidos_del_dia, int[] camiones_del_dia)
    //{

    //    List<pedido_por_cliente> pedidos_del_dia_tipo_de_pedido = Filtrar_por_pedido(pedidos_del_dia, entrega.express); //filtramos la lista de pedidos total para obtener solo la lista de clientes qeu tienen envio express
    //    List<eLocalidad> lista_localidad = Lista_Barrios_Ordenada(pedidos_del_dia_tipo_de_pedido); //me va a devolver una lista con los barrios ordenados del mas cercano a liniers al mas alejado
    //    int barrios_a_recorrer = Barrios_en_pedido_del_dia(pedidos_del_dia_tipo_de_pedido); //la cantidad de barrios que tenemos que recorrer para entregar esos pedidos express
    //    List<eLocalidad> orden_clientes = new List<eLocalidad>(); //donde vamos a guardar la lista de los barios en orden de ls barrios que tenemos que recorrer

    //    int[,] matriz = new int[barrios_a_recorrer, barrios_a_recorrer];
    //    matriz = llenar_matriz_con_distancias(lista_localidad, barrios_a_recorrer); //me va a llenar la matriz con las distancias entre cada pueblo que voy a recorrer, esto es para poder hacer el agoritmo de dkjistra
    //                                                                                               //basicamente me calcula la distancia entre cada nodo (ya que cada nodo (localidad) tiene direccion bidireccional y conexion con todos los demas nodos

    //    bool[] verificacion_barrios = new bool[barrios_a_recorrer]; //vector de bool que vamos a usar para saber si un barrio fue recorrido o no, si esta en true (ya lo recorrimos)
    //    int h = 1; //para ir llenando orden a clientes
    //    int i = 0;

    //    //para calcular el camino -> el mejor camino ¡¡en el momento!! -> algortimo de djkistra
    //    while (chequeo_verificacion_barrios(verificacion_barrios) != 1) //funcion que me devuelve si ya todos los barrios fueron recorridos o no -> 1 si llegaste al final, 0 si no llegaste al final y -1 si falta un barrio
    //    {
    //        int min = min_distancia(matriz, i, verificacion_barrios, barrios_a_recorrer, orden_clientes, lista_localidad);//la funcion me devuelve la distancia minima que va a hacer el camion para ir de un barrio a otro (con este algoritmo va a ir eligiendo siempre la dist minima entre barrio y barrio)
    //        // tambien la funcion min_distancia me llena la lista orden_clientes poniendome en la pos h el barrio a recorrer, eLocalidad
    //        i = numero_de_posicion_barrio(orden_clientes.ElementAt(h), lista_localidad); //ahora i va a ser de donde sale el camion, es por eso que apenas entramos al while, es 0, porque siempre sale de liniers, y por eso agarra la ult pos de la lista que va guardando los barrios en orden, pero como orden cliente es eLocalidad, la funcion numero de posicion barrrio te pasa en que posicion de la matriz esta el barrio que recien se recorrio
    //        h++;
    //    }

    //    List<pedido_por_cliente> lista_clientes_filtrada_ordenada = new List<pedido_por_cliente>();

    //    lista_clientes_filtrada_ordenada = Ordenar_por_pedidio(orden_clientes, pedidos_del_dia); //me pone los clientes que pertenecen a los barrios a recorrer en orden en la lista, tambien por barrio estan organizados primero los express y despues los normales

    //    int cant_camiones = 0;

    //    //meto todo lo que pueda en una camioneta, y despues sigo con la otra camioneta a partir del otro barrio y asi

    //    while (cant_camiones < camiones_del_dia.Length)
    //    {
    //        llenado_despacho_productos(camiones_del_dia[cant_camiones], lista_clientes_filtrada_ordenada); //llena los pedidos en el camion elegido, mientras elimina el barrio recorrido y tambien elimina a los pedidos que se metieron al camion de la lista
    //        cant_camiones++; //una vez lleno el camion anterior, sigo con el siguiente
    //    }


    //}

    
   static void preparo_y_desapacho_de_productos(List<pedido_por_cliente> pedidos_del_dia_, List<eVehiculo> camiones_del_dia)
    {
        //filtramos la lista completa pasada por parametro en listas de las localidades con pedidos express y normales, y los pedidos express y normales
        List<pedido_por_cliente> pedidos_del_dia_express = Filtrar_por_pedido(pedidos_del_dia_, entrega.express);
        List<eLocalidad> lista_localidades_express = Lista_Barrios_Ordenada(pedidos_del_dia_express);

        List<pedido_por_cliente> pedidos_del_dia_normales = Filtrar_por_pedido(pedidos_del_dia_, entrega.normal);
        List<eLocalidad> lista_localidades_normal = Lista_Barrios_Ordenada(pedidos_del_dia_normales);

        List<pedido_por_cliente> pedido_a_entregar = new List<pedido_por_cliente>();


        eVehiculo camion = camiones_del_dia.ElementAt(0); //siempre empezamos con la camioneta
        int cont_camiones = 0;
        int max_viajes = max_viajes_por_dia(camiones_del_dia);
        while (cont_camiones < max_viajes && (lista_localidades_express != null || lista_localidades_normal != null))
        {//hasta que no haya mas camiones o haya despachado todos los productos
            despacho_de_productos(lista_localidades_normal, pedidos_del_dia_express, pedidos_del_dia_normales, lista_localidades_express, pedido_a_entregar,camion); //calculo el mejor camino, y despacho todos los paquetes posibles, dandole prioridad a los express
            cont_camiones++;//se lleno el camion anterior, uso el siguiente
            if (cont_camiones == 4) //seguimos con el furgon
                camion = camiones_del_dia.ElementAt(1);
            if (cont_camiones == 5)//seguimos con la furgoneta
                camion = camiones_del_dia.ElementAt(2);
            pedido_a_entregar.RemoveRange(0, pedido_a_entregar.Count); //como los vamos entregando, borro la lista porque ya salio el camion

        }

    }

    //funciones 
   static int max_viajes_por_dia(List<eVehiculo>camiones_del_dia)
    {
        int viajes = 4;  //la camioneta siemrpe sale por lo que siempre voy a tener 4 viajes minimo
        if (camiones_del_dia.Count == 2) //si solo sale el furgon o la furgoneta mas la camioneta
            viajes++;
        if (camiones_del_dia.Count == 2) //si salen los 3 vehiculos
            viajes = viajes + 2;
        return viajes;
    }
    static int numero_de_posicion_barrio(eLocalidad barrio_elegido, List<eLocalidad> lista_localidad)
    {
        int pos = 0;
        for (int i = 0; i < lista_localidad.Count; i++)
        {
            if (lista_localidad[i] == barrio_elegido)
                pos = i;
        }
        return pos;
    }
    static int chequeo_verificacion_barrios(bool[] verificacion_barrios)
    {
        // devuelve -> 1 si llegaste al final (todos los barrios fueron recorridos), 0 si no llegaste al final (falta mas de un barrio por recorrer) y -1 si falta un barrio (solo falta un barrio por recorrer), -2 si no se recorrio ningun barrio todavia

        int cont = 0;
        for (int i = 0; i < verificacion_barrios.Length; i++)
        {
            if (verificacion_barrios[i] == false)
                cont++;
        }
        if (cont == 0)
            return 1; //todos los barrios fueron recorridos
        else if (cont == 1)
            return -1; //solo falta un barrio
        else if (cont == verificacion_barrios.Length)
            return -2;
        else
            return 0; //falta mas de un barrio por recorrer
    }

    static int Barrios_en_pedido_del_dia(List<pedido_por_cliente> lista_pedido)
    {
        List<pedido_por_cliente> aux = new List<pedido_por_cliente>();
        for (int i = 0; i < lista_pedido.Count; i++)
        {
            aux.Add(lista_pedido.ElementAt(0));
        }

        int cont = 0;
        bool flag = false;

        for (int i = 0; i < lista_pedido.Count; i++)
        {
            flag = false;
            for (int h = i + 1; h < lista_pedido.Count; h++)
            {//cuento solo una vez (con ayuda del flag) al barrio y todos los clientes que encuentre que van al mismo barrio los elimino de la lista auxiliar
                //notar que la lista original no se modifica, solo la auxiliar
                if (aux[i].barrio == aux[h].barrio && flag == false)
                {
                    flag = true;
                    aux.RemoveAt(h);
                    cont++;
                }
                else if (aux[i].barrio == aux[h].barrio)
                    aux.RemoveAt(h);
            }
        }
        return cont;
    }

    static int calcular_distancia_barrio_a_liniers(eLocalidad barrioo)
    {
        int dist = 0;
        switch (barrioo)
        {
            case eLocalidad.Liniers: dist = 0; break;
            case eLocalidad.TresdeFebrero: dist = 6; break;
            case eLocalidad.SanMartin: dist = 11; break;
            case eLocalidad.VicenteLopez: dist = 17; break;
            case eLocalidad.LaMatanza: dist = 5; break;
            case eLocalidad.LomasdeZamora: dist = 18; break;
            case eLocalidad.Lanus: dist = 19; break;
            case eLocalidad.Avellaneda: dist = 21; break;
            case eLocalidad.Versalles: dist = 3; break;
            case eLocalidad.VillaLuro: dist = 2; break;
            case eLocalidad.Mataderos: dist = 4; break;
            case eLocalidad.MonteCastro: dist = 7; break;
            case eLocalidad.VelezSarsfield: dist = 8; break;
            case eLocalidad.ParqueAvellaneda: dist = 9; break;
            case eLocalidad.VillaLugano: dist = 10; break;
            case eLocalidad.VillaDevoto: dist = 12; break;
            case eLocalidad.VillaUrquiza: dist = 13; break;
            case eLocalidad.Belgrano: dist = 20; break;
            case eLocalidad.Palermo: dist = 14; break;
            case eLocalidad.Retiro: dist = 27; break;
            case eLocalidad.Caballito: dist = 15; break;
            case eLocalidad.Flores: dist = 16; break;
            case eLocalidad.PuertoMadero: dist = 18; break;
            case eLocalidad.LaBoca: dist = 25; break;
            case eLocalidad.Chacarita: dist = 23; break;
        }
        return dist;
    }

    static int min_distancia(int[,] matriz, int pos, bool[] verificacion_barrios, int barrios, List<eLocalidad> orden_clientes, List<eLocalidad> lista_localidad)
    {
        int min = Constants.max_index; //le pongo un valor muy alto, para que la primera vez que compare con la distancia de una localidad 
        int i = 0;

        if(chequeo_verificacion_barrios(verificacion_barrios) == -2)//si todavia no se recorrio ningun barrio, voy al que mas cerca este de liniers primero
        {
            for(int h = 0; h < barrios; h++)
            {
                if (calcular_distancia_barrio_a_liniers(lista_localidad[h]) < min)
                {
                    i = h;//i=barrio que fui
                    min = calcular_distancia_barrio_a_liniers(lista_localidad[h]);
                }
            }
            verificacion_barrios[i] = true; //ya recorridomo
            orden_clientes.Add(lista_localidad[i]);//sumo el barrio a la lista
        }
        if (chequeo_verificacion_barrios(verificacion_barrios) == -1)//solo falta un barrio, calculo la distancia de ese barrio a liniers, para que vuelva el camion
        {
            for (int h = 0; h < barrios; h++)
            {
                if (verificacion_barrios[h] == false)//al primero que entre va a ser el unico que no recorri
                { orden_clientes.Add(lista_localidad[h]);
                    min = calcular_distancia_barrio_a_liniers(lista_localidad[h]);
                    verificacion_barrios[h] = true;
                }
            }
        }
        else
        {

            for (int v = 0; v < barrios; v++) //voy recorriendo todos los nodos
            {
                if (verificacion_barrios[v] == false && matriz[pos, v] < min && matriz[pos, v] != 0)//si el barrio no fue recorrido y es la pos minima de esa fila de la matriz, es decir, del camino que tengo desde ese barrio a los demas que tengo que ir, entro
                {
                    i = v;//i=barrio que fui
                    min = matriz[pos, v]; //distancia que recorri
                    
                }

            }


            verificacion_barrios[i] = true; //ya recorridomo
            orden_clientes.Add(lista_localidad[i]);//sumo el barrio a la lista
        }
        //explicacion conexion lista localidades y matriz, lista localidades esta ordenada de menor a mayor distancia con respecto a liniers. Ejemplo en la posicion 0 de la lista de localidades esta mataderos, entonces en la fila y columna 0 va a estar las distancia de mataderos con los otros barrios

        return min;
    }
    
    static void despacho_de_productos(List<eLocalidad> lista_localidades_normal, List<pedido_por_cliente> pedidos_del_dia_express, List<pedido_por_cliente> pedidos_del_dia_normales, List<eLocalidad> lista_localidades_express, List<pedido_por_cliente> pedido_a_entregar, eVehiculo camion)
    {//las listas de localidades estan ordenadas por orden de menor distancia a liniers a mayor
        eOpcion chequeo_camion_lleno = eOpcion.no_se_lleno;
        List<eLocalidad> camino_mas_corto=new List<eLocalidad>();

        if (lista_localidades_express == null) //si no hay mas localidades express, es decir ya recorri todas las express, sigo con los pedidos normales
        {
            while (chequeo_camion_lleno == eOpcion.no_se_lleno && lista_localidades_normal != null)
            { //mientras que el camion no este lleno o mientras siga habiendo locaclidades que recorrer
                chequeo_camion_lleno = intento_llenar_camion(lista_localidades_normal[0], pedidos_del_dia_normales, camion, pedido_a_entregar); //mete los pedidos que van entrando en pedido_a_entregar

                if (chequeo_camion_lleno != eOpcion.se_lleno_pero_quedaron_cosas_de_la_localidad) //si se pudo meter todos los pedidos de esa locaclidad, la elimino porque ya no la van a tenr que recorrer 
                    lista_localidades_normal.RemoveAt(0);

            }


            int barrios_a_recorrer = Barrios_en_pedido_del_dia(pedido_a_entregar); //la cantidad de barrios que se lograron meter en el camion
            List<eLocalidad> lista_localidades = Lista_Barrios_Ordenada(pedido_a_entregar);
             int[,] matriz = new int[barrios_a_recorrer, barrios_a_recorrer];
            matriz = llenar_matriz_con_distancias(lista_localidades, barrios_a_recorrer); //me va a llenar la matriz con las distancias entre cada pueblo que voy a recorrer

             camino_mas_corto = encontrar_camino_mas_corto_greedy(matriz, barrios_a_recorrer, lista_localidades);


        }
        else
        {
            //si todavia hay pedidos express para entregar
            while (chequeo_camion_lleno == eOpcion.no_se_lleno && lista_localidades_express != null)
            { //mientras haya locaclidades express que recorrer y el camion este vacio

                chequeo_camion_lleno = intento_llenar_camion(lista_localidades_normal[0], pedidos_del_dia_normales, camion, pedido_a_entregar);

                if (chequeo_camion_lleno != eOpcion.se_lleno_pero_quedaron_cosas_de_la_localidad) //si se pudo meter todos los pedidos de esa locaclidad, la elimino porque ya no la van a tenr que recorrer 
                    lista_localidades_normal.RemoveAt(0);

                //si despues de poner lo ultimo en el camion de lo express este no se lleno, lo relleno con productos de tipo entrega normal
                if (lista_localidades_express == null && chequeo_camion_lleno == eOpcion.no_se_lleno)
                    rellenar_camion(lista_localidades_normal, pedido_a_entregar, pedidos_del_dia_normales, camion);
                //llenamos el camion con los pedidos normales de las zonas mas cercanas a liniers -> eliminamos esas localidades de la lista ya directamente adentro de la funcion, al igual que los pedidos incluidos
            }

            int barrios_a_recorrer = Barrios_en_pedido_del_dia(pedido_a_entregar);
            int[,] matriz = new int[barrios_a_recorrer, barrios_a_recorrer];
            List<eLocalidad> lista_localidades = Lista_Barrios_Ordenada(pedido_a_entregar);
            matriz = llenar_matriz_con_distancias(lista_localidades, barrios_a_recorrer); //me va a llenar la matriz con las distancias entre cada pueblo que voy a recorrer

            camino_mas_corto = encontrar_camino_mas_corto_greedy(matriz, barrios_a_recorrer, lista_localidades); //encontramos el camino mas corto para recorrer todos los barrios de los pedidos que entraron en el camion



        }


        //despues de pasar por los if, ya tengo la lista de pedidos a entregar y la lista de barrios a recorrer en orden 
        //primero ordena por barrio, poniendo primero a los del primer barrio a recorrer y después los del último barrio. 
        pedido_a_entregar = Ordenar_por_pedidio(camino_mas_corto, pedido_a_entregar);

        llenado_despacho_productos(camion, pedido_a_entregar); //esta funcion me va a llenar el camión que yo le pase por parámetro, con los pedidos de las localidades seleccionadas



    }

    static void llenado_despacho_productos(eVehiculo camion, List<pedido_por_cliente> lista_completa_pedidos)
    {
        //lista pedidos esta fltrada por solo las zonas que estan en el recorrido y primero estan puestos los del envio express, esto lo hacemos desde las funciones en donde llamamos a esta funcion, es por esto que siempre agarramos la posicion 0, porque vamos eliminando al primero despues de meterlo al camion -> siempre el primero de la lista es el que tenemos que meter primero
        Queue<pedido_por_cliente> pedidos_a_entregar = new Queue<pedido_por_cliente>(); //cola donde vamos a ir poniendo los pedidos FIRST IN -> FIRST OUT
        int volumen_aux = 0;
        int peso_aux = 0;

        if (camion.GetType()==typeof(Camioneta) )//si el camion seleccionado es la camioneta
        {//las camionetas no tiene limite de peso, solo de volumen
            while (volumen_aux < Constants.VOLUMEN_CAMIONETA) //mientras que el volumen que tiene sea menor que el max de la camioneta
            {
                if (volumen_aux + lista_completa_pedidos[0].volumen <= Constants.VOLUMEN_CAMIONETA) //cheque que si le sumo el paquete siguiente no supere el volumen total.
                {
                    pedidos_a_entregar.Enqueue(lista_completa_pedidos[0]); //lo agregamos a la cola
                    lista_completa_pedidos.RemoveAt(0); //sacamos a ese pedido de la lista
                    volumen_aux = volumen_aux + lista_completa_pedidos[0].volumen; //sumamos el volumen
                }
                else
                    volumen_aux = Constants.VOLUMEN_CAMIONETA;//si ya sumandole el prox paquete, supero el peso del camion, le impongo que el peso es el max para que salga del while

            }
        }
        else if (camion.GetType()==typeof(Furgon)) //el camion seleccionado es el furgon
        {//en este caso chequeamos tanto el volumen total y el peso total 
            while (volumen_aux < Constants.VOLUMEN_FURGON && peso_aux < Constants.PESOMAXFURGON)
            {
                if (volumen_aux + lista_completa_pedidos[0].volumen < Constants.VOLUMEN_FURGON && (peso_aux + lista_completa_pedidos[0].peso_pedido) < Constants.PESOMAXFURGON)
                {
                    pedidos_a_entregar.Enqueue(lista_completa_pedidos[0]);
                    lista_completa_pedidos.RemoveAt(0);
                    volumen_aux = volumen_aux + lista_completa_pedidos[0].volumen;
                    peso_aux = peso_aux + lista_completa_pedidos[0].peso_pedido;
                }
                else
                {
                    volumen_aux = Constants.VOLUMEN_FURGON;
                    peso_aux = Constants.PESOMAXFURGON;
                }


            }
        }
        else //cont es igual a 5 -> furgoneta
        {//en este caso chequeamos tanto el volumen total y el peso total
            while (volumen_aux < Constants.VOLUMEN_FURGONETA && peso_aux < Constants.PESOMAXFURGONETA)
            {
                if (volumen_aux + lista_completa_pedidos[0].volumen < Constants.VOLUMEN_FURGONETA && (peso_aux + lista_completa_pedidos[0].peso_pedido) < Constants.PESOMAXFURGONETA)
                {
                    pedidos_a_entregar.Enqueue(lista_completa_pedidos[0]);
                    lista_completa_pedidos.RemoveAt(0);
                    volumen_aux = volumen_aux + lista_completa_pedidos[0].volumen;
                    peso_aux = peso_aux + lista_completa_pedidos[0].peso_pedido;
                }
                else
                {
                    volumen_aux = Constants.VOLUMEN_FURGONETA;
                    peso_aux = Constants.PESOMAXFURGONETA;
                }


            }
        }
    }

    static List<pedido_por_cliente> Filtrar_por_pedido(List<pedido_por_cliente> pedidos_del_dia_, entrega tipo_entrega)
    {
        List<pedido_por_cliente> aux = new List<pedido_por_cliente>();

        for (int i = 0; i < pedidos_del_dia_.Count; i++)
        {
            if (pedidos_del_dia_[i].tipo_entrega == tipo_entrega)
                aux.Add(pedidos_del_dia_[i]);
        }
        return aux;
    }

    static List<eLocalidad> Lista_Barrios_Ordenada(List<pedido_por_cliente> pedidos_del_dia_tipo_de_pedido)
    {

        List<eLocalidad> aux = new List<eLocalidad>();
        //primero meto todos los barrios de todos los pedidos en una lista, no importa si ya puse ese barrio antes
        for (int i = 0; i < pedidos_del_dia_tipo_de_pedido.Count; i++)
        {
            aux.Add(pedidos_del_dia_tipo_de_pedido[i].barrio);
        }

        //ahora saco los repetidos
        for (int i = 0; i < pedidos_del_dia_tipo_de_pedido.Count; i++)
        {
            for (int j = 1; j < pedidos_del_dia_tipo_de_pedido.Count; j++)
            {
                if (aux.ElementAt(i) == aux.ElementAt(j))
                    aux.RemoveAt(j);
            }
        }

        //ahora aux tiene la lista de localidades 

        for (int x = 0; x < aux.Count; x++)
        {

            for (int y = 0; y < aux.Count - 1; y++)
            {

                // Si el actual es mayor que el que le sigue a la derecha...
                if (calcular_distancia_barrio_a_liniers(aux.ElementAt(y)) > calcular_distancia_barrio_a_liniers(aux.ElementAt(y + 1)))
                { //distancia liniers es una funcion que le pasas una localidad y te pasa la distancia de esa localidad a liniers
                    eLocalidad temporal = aux.ElementAt(y);
                    aux.Insert(y, aux.ElementAt(y + 1));
                    aux.Insert(y + 1, temporal);
                }
            }
        }
        //ahora aux esta ordenado, con el barrio con la distancia minima a liniers en la primer posicion y el barrio mas alejado en la ultima posicion
        return aux;
    }

    static List<pedido_por_cliente> Ordenar_por_pedidio(List<eLocalidad> camino_mas_corto, List<pedido_por_cliente> pedido_a_entregar)
    {
        List<pedido_por_cliente> pedidos_ordenados = new List<pedido_por_cliente>();
        List<pedido_por_cliente> aux = new List<pedido_por_cliente>();

        for (int i = 0; i < camino_mas_corto.Count; i++)
        {
            aux.RemoveRange(0, aux.Count); //elimino lo que habia en la lista de otras operaciones

            for (int h = 0; h < pedido_a_entregar.Count; h++)
            {
                if (pedido_a_entregar[h].barrio == camino_mas_corto[i])//si la localidad del barrio es igual a la localidad que se debe seleccionar en el momento
                    aux.Add(pedido_a_entregar[h]);
            }

            Ordenar_por_prioridad_pedido(aux); //esta funcion me pone primero los pedidos express y despues los normales de la localidad que acabo de llenar

            for (int n = 0; n < aux.Count; n++)
            {
                pedidos_ordenados.Add(aux.ElementAt(n)); //agrego los pedidos de la localidad recien ordenada en la lista que vamos a devolver
            }
        }
        return pedidos_ordenados;

    }

    
    static int[,] llenar_matriz_con_distancias(List<eLocalidad> lista_localidades, int barrios)
    {
        
        //esta es la matriz que ya esta predeterminada, tiene todas las distancias de cada localidad a localidad
        int[,] matriz_definitiva = new int[25, 25];
        List<int> list_liniers = new List<int>() { 6, 11, 17, 5, 18, 19, 21, 3, 2, 4, 7, 8, 9, 10, 12, 13, 20, 14, 27, 15, 16, 18, 25, 23 };
        List<int> list_tresfebrero = new List<int>() { 7,14,26,21,19,23,3,6,9,5,8,10,13,4,11,15,22,23,12,17,2524,16 };
        List<int> list_sanmartin = new List<int>() {12,30,26,24,28,8,13,15,9,16,17,18,5,6,14,16,20,19,12,27,29,8 };
        List<int> list_vicentelopez = new List<int>() {40,33,31,20,15,18,21,16,19,22,25,12,8,6,10,12,16,30,15,24,23};
        List<int> list_lamatanza = new List<int>() {31,30,24,5,22,20,7,28,8,10,9,14,23,27,29,13,11,21,24,17 };
        List<int> list_lomasdezamora = new List<int>() { 9,30,20,21,15,23,19,16,13,24,28,36,25,43,20,17,27,28,24};
        List<int> list_lanus = new List<int>() {6,17,14,15,16,1,13,12,21,26,2,18,15,18,12,13,10,20 };
        List<int> list_avellaneda = new List<int>() {20,17,18,19,3,16,15,25,29,37,15,8,12,16,7,4,26 };
        List<int> list_versalles = new List<int>() {2,5,3,4,8,9,5,11,18,12,24,8,11,20,19,10 };
        List<int> list_villaluro = new List<int>() {4,3,2,4,6,8,11,10,12,27,13,5,15,16,9 };
        List<int> list_mataderos = new List<int>() { 6,4,2,3,9,14,22,13,30,8,5,17,18,11};
        List<int> list_montecastro = new List<int>() {2,5,10,3,8,10,9,24,7,5,18,20,7 };
        List<int> list_velez = new List<int>() { 2,4,3,10,11,8,14,5,3,15,16,6};
        List<int> list_parqueavellaneda = new List<int>() { 4,6,13,12,10,16,6,3,14,15,9};
        List<int> list_villadevoto = new List<int>() {12,20,27,15,35,10,7,16,17,16 };
        List<int> list_villaurquiza = new List<int>() { 5,8,10,21,9,8,23,24,7};
        List<int> list_belgrano = new List<int>() {4,7,18,8,19,27,29,5 };
        List<int> list_palermo = new List<int>() { 4,8,7,11,3,16,4};
        List<int> list_retiro = new List<int>() { 5,4,10,9,14,2};
        List<int> list_caballito = new List<int>() {8,12,4,7,8 };
        List<int> list_flores = new List<int>() {4,8,10,5 };
        List<int> list_puertomadero = new List<int>() {10,15,8 };
        List<int> list_laboca = new List<int>() { 3,13};
        //charcarita ->14

        int pos = 0;
        for (int i = 0; i < 25; i++)
        {
            matriz_definitiva[i, i] = 0;//pongo a la diagonal en 0
                                        //lleno matriz
            pos = i;
            switch (i)
            {
                case 0:

                    for (int h = 0; h < list_liniers.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_liniers.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_liniers.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_liniers.ElementAt(h);
                    }
                    break;

                case 1:

                    for (int h = 0; h < list_tresfebrero.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_tresfebrero.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_tresfebrero.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_tresfebrero.ElementAt(h);
                    }
                    break;

                case 2:

                    for (int h = 0; h < list_sanmartin.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_sanmartin.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_sanmartin.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_sanmartin.ElementAt(h);
                    }

                    break;
                case 3:

                    for (int h = 0; h < list_vicentelopez.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_vicentelopez.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_vicentelopez.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_vicentelopez.ElementAt(h);
                    }
                    break;
                case 4:
                    for (int h = 0; h < list_lamatanza.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_lamatanza.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_lamatanza.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_lamatanza.ElementAt(h);
                    }
                    break;
                case 5:
                    for (int h = 0; h < list_lomasdezamora.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_lomasdezamora.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_lomasdezamora.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_lomasdezamora.ElementAt(h);
                    }
                    break;
                case 6:

                    for (int h = 0; h < list_lanus.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_lanus.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_lanus.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_lanus.ElementAt(h);
                    }
                    break;
                case 7:
                    for (int h = 0; h < list_avellaneda.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_avellaneda.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_avellaneda.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_avellaneda.ElementAt(h);
                    }
                    break;
                case 8:
                    for (int h = 0; h < list_versalles.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_versalles.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_versalles.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_versalles.ElementAt(h);
                    }
                    break;
                case 9:
                    for (int h = 0; h < list_villaluro.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_villaluro.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_villaluro.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_villaluro.ElementAt(h);
                    }
                    break;
                case 10:
                    for (int h = 0; h < list_mataderos.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_mataderos.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_mataderos.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_mataderos.ElementAt(h);
                    }
                    break;
                case 11:
                    for (int h = 0; h < list_montecastro.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_montecastro.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_montecastro.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_montecastro.ElementAt(h);
                    }
                    break;
                case 12:
                    for (int h = 0; h < list_velez.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_velez.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_velez.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_velez.ElementAt(h);
                    }
                    break;
                case 13:
                    for (int h = 0; h < list_parqueavellaneda.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_parqueavellaneda.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_parqueavellaneda.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_parqueavellaneda.ElementAt(h);
                    }
                    break;
                case 14:
                    for (int h = 0; h < list_villadevoto.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_villadevoto.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_villadevoto.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_villadevoto.ElementAt(h);
                    }
                    break;
                case 15:
                    for (int h = 0; h < list_villaurquiza.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_villaurquiza.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_villaurquiza.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_villaurquiza.ElementAt(h);
                    }
                    break;
                case 16:
                    for (int h = 0; h < list_belgrano.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_belgrano.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_belgrano.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_belgrano.ElementAt(h);
                    }
                    break;
                case 17:
                    for (int h = 0; h < list_palermo.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_palermo.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_palermo.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_palermo.ElementAt(h);
                    }
                    break;
                case 18:
                    for (int h = 0; h < list_retiro.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_retiro.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_retiro.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_retiro.ElementAt(h);
                    }
                    break;
                case 19:
                    for (int h = 0; h < list_caballito.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_caballito.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_caballito.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_caballito.ElementAt(h);
                    }
                    break;
                case 20:
                    for (int h = 0; h < list_flores.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_flores.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_flores.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_flores.ElementAt(h);
                    }
                    break;
                case 21:
                    for (int h = 0; h < list_puertomadero.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_puertomadero.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_puertomadero.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_puertomadero.ElementAt(h);
                    }
                    break;
                case 22:
                    for (int h = 0; h < list_laboca.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[i, pos] = list_laboca.ElementAt(h);
                    }
                    pos = i;
                    for (int h = 0; h < list_laboca.Count; h++)
                    {
                        pos++;
                        matriz_definitiva[pos, i] = list_laboca.ElementAt(h);
                    }
                    break;
                case 23:
                    pos++;
                    matriz_definitiva[i, pos] = 14;
                    matriz_definitiva[pos, i] = 14;

                    break;
            }
        }

        //llenarla


        //llenamos la matriz
        int[] num_barrios_en_matriz = new int[barrios];
        int[,] matriz_distancias = new int[barrios, barrios];


        int barrio_fijo = 0;
        for(int i=0;i<barrios; i++)
        {
            barrio_fijo = num_asignado_barrio(lista_localidades.ElementAt(0));
            num_barrios_en_matriz[i] = barrio_fijo;
        }
        //tengo un vector con los numeros de los barrios que necesito correspondiente a la matriz ya definida
   
        
        for(int i = 0; i < lista_localidades.Count; i++)
        {
           for (int pos_barrios = 0; pos_barrios < barrios; pos_barrios++)
                {
                matriz_distancias[i, pos_barrios] = matriz_definitiva[num_barrios_en_matriz[i], num_barrios_en_matriz[pos_barrios]];
                }
        }


        return matriz_distancias;

    }

    static int num_asignado_barrio(eLocalidad localidad)
    {
        switch (localidad)
        {
            case eLocalidad.Liniers: return 0;
            case eLocalidad.TresdeFebrero: return 1;
            case eLocalidad.SanMartin: return 2;
            case eLocalidad.VicenteLopez: return 3;
            case eLocalidad.LaMatanza: return 4;
            case eLocalidad.LomasdeZamora: return 5;
            case eLocalidad.Lanus: return 6;
            case eLocalidad.Avellaneda: return 7;
            case eLocalidad.Versalles: return 8;
            case eLocalidad.VillaLuro: return 9;
            case eLocalidad.Mataderos: return 10;
            case eLocalidad.MonteCastro: return 11;
            case eLocalidad.VelezSarsfield: return 12;
            case eLocalidad.ParqueAvellaneda: return 13;
            case eLocalidad.VillaLugano: return 14;
            case eLocalidad.VillaDevoto: return 15;
            case eLocalidad.VillaUrquiza: return 16;
            case eLocalidad.Belgrano: return 17;
            case eLocalidad.Palermo: return 18;
            case eLocalidad.Retiro: return 19;
            case eLocalidad.Caballito: return 20;
            case eLocalidad.Flores: return 21;
            case eLocalidad.PuertoMadero: return 22;
            case eLocalidad.LaBoca: return 23;
            case eLocalidad.Chacarita: return 24;
        }
        return -1;
    }

    static List<eLocalidad> encontrar_camino_mas_corto_greedy(int[,] matriz, int barrios_a_recorrer, List<eLocalidad> localidades_en_orden_matriz)
    {
        List<eLocalidad> orden_clientes = new List<eLocalidad>(); //donde vamos a guardar la lista de los barios en orden de ls barrios que tenemos que recorrer
        bool[] verificacion_barrios = new bool[barrios_a_recorrer]; //vector de bool que vamos a usar para saber si un barrio fue recorrido o no, si esta en true (ya lo recorrimos)
        int h = 0; //para ir llenando orden a clientes
        int i = 0;
        for(int j = 0; j < barrios_a_recorrer; j++)
        {
            verificacion_barrios[j] = false;
        }
        //para calcular el camino -> el mejor camino ¡¡en el momento!! -> algortimo de djkistra
        while (chequeo_verificacion_barrios(verificacion_barrios) != 1) //funcion que me devuelve si ya todos los barrios fueron recorridos o no -> 1 si llegaste al final, 0 si no llegaste al final y -1 si falta un barrio
        {
            int min = min_distancia(matriz, i, verificacion_barrios, barrios_a_recorrer, orden_clientes, localidades_en_orden_matriz);//la funcion me devuelve la distancia minima que va a hacer el camion para ir de un barrio a otro (con este algoritmo va a ir eligiendo siempre la dist minima entre barrio y barrio)
                                                                                                                          // tambien la funcion min_distancia me llena la lista orden_clientes poniendome en la pos h el barrio a recorrer, eLocalidad
            i = numero_de_posicion_barrio(orden_clientes.ElementAt(h), localidades_en_orden_matriz); //ahora i va a ser de donde sale el camion, es por eso que apenas entramos al while, es 0, porque siempre sale de liniers, y por eso agarra la ult pos de la lista que va guardando los barrios en orden, pero como orden cliente es eLocalidad, la funcion numero de posicion barrrio te pasa en que posicion de la matriz esta el barrio que recien se recorrio
            h++;
        }
        return orden_clientes;
    }

    static eOpcion intento_llenar_camion(eLocalidad localidad, List<pedido_por_cliente> pedidos_del_dia, eVehiculo camion, List<pedido_por_cliente> pedido_a_entregar)
    {
        bool flag_elevador = false;
        //llena el camion con los pedidos de la locacalidad pasada como paremetro, si logra llenar con todos los pedidos de la localidad y sigue habiendo espacio, devuelve que no se lleno. Si logra poner TODOS los pedidos de la localidad y no queda espacio, es decir, el volumen que ya lleno el camion es igual o mayor que el volumen total sin la caja mas pequeña de entrega entonces devuelvo que se llenó completo
        //si mientras lo lleno hay un pedido que no puedo meter de la locaclidad porque ya superaria el peso o volumen total del camion, devuelvo que se lleno pero que quedaron cosas de la locacalidad sin poner. Igualmente, auqnue no va a poner el pedido que agregandolo superaria el volumen/peso maximo va a seguir buscando a ver si puede meter otro pedido de menor volumen o peso al camion
        //de esta manera, el camion siempre va a salir lo mas lleno posible
        eOpcion estado = eOpcion.no_se_lleno;
        bool flag = false; //para saber si se pudo meter todo lo de la localidad o no, false es que si, true es que no
        int volumen_aux = 0;
        int peso_aux = 0;

        //camion elegido -> camioneta
        if (camion.GetType() == typeof(Camioneta))
        {


            for (int i = 0; i < pedidos_del_dia.Count; i++)//recorro toda la lista 
            {
                if (volumen_aux < Constants.VOLUMEN_CAMIONETA) //si el volumen que voy sumando de los productos que voy metiendo al camion no supera al volumen total, mismo pensamiento con el peso
                {
                    if ((volumen_aux + pedidos_del_dia[i].volumen) < Constants.VOLUMEN_CAMIONETA && pedidos_del_dia[i].barrio == localidad)
                    {
                        if (Verifical_electro_pequeño(pedidos_del_dia[i]) == true && flag_elevador == false)
                        {
                            volumen_aux = volumen_aux + pedidos_del_dia[i].volumen + Constants.volumen_elevador;
                            peso_aux = peso_aux + pedidos_del_dia[i].peso_pedido + Constants.peso_elevador;
                            pedido_a_entregar.Add(pedidos_del_dia[i]);
                            pedidos_del_dia.RemoveAt(i);
                            flag_elevador = true;
                        }
                        else
                        {
                            volumen_aux = volumen_aux + pedidos_del_dia[i].volumen;
                            peso_aux = peso_aux + pedidos_del_dia[i].peso_pedido;
                            pedido_a_entregar.Add(pedidos_del_dia[i]);
                            pedidos_del_dia.RemoveAt(i);
                        }
                    }
                    else if (pedidos_del_dia[i].barrio == localidad) //si no entra en el camion pero es de la localidad, siginifica que ya va ahaber un paquete que no voy a meter que va a estra en el prox camion 
                    {
                        flag = true;
                        estado = eOpcion.se_lleno_pero_quedaron_cosas_de_la_localidad;
                    }
                }
            }
            if (volumen_aux >= (Constants.VOLUMEN_CAMIONETA - Constants.volumen_electro_pequeños) && flag == false) //si no queda mucho espacio , ni siquiera para que entre una pedido chico mas y aparte nunca estuvo en la situcion de que no entro un pedido de la localidad
                estado = eOpcion.se_lleno_completo_;

            return estado;
        }
        else if (camion.GetType() == typeof(Furgon)) // camion elegido->furgon
        {

            for (int i = 0; i < pedidos_del_dia.Count; i++)
            {
                if (volumen_aux < Constants.VOLUMEN_FURGON && peso_aux < Constants.PESOMAXFURGON)
                {
                    if ((volumen_aux + pedidos_del_dia[i].volumen) < Constants.VOLUMEN_FURGON && (peso_aux + pedidos_del_dia[i].peso_pedido) < Constants.PESOMAXFURGON && pedidos_del_dia[i].barrio == localidad)
                    {
                        if (Cantidad_Televisores(pedidos_del_dia[i]) > 0)
                        {
                            pedidos_del_dia[i].volumen_pedido_set = pedidos_del_dia[i].volumen - Constants.televisores; //le restamos el volumen del televisor al volumen total del pedido
                            int nuevo_volumen = 2 * 1 * 4; //ancho y profundidad del televisor pero alto del furgon
                            pedidos_del_dia[i].volumen_pedido_set = pedidos_del_dia[i].volumen + nuevo_volumen;
                            //una vez que ya esta actualizado el nuevo volumen, sigo como el resto de los pedidos sin televisor
                        }

                        if (Verifical_electro_pequeño(pedidos_del_dia[i]) == true && flag_elevador == false)
                        {
                            volumen_aux = volumen_aux + pedidos_del_dia[i].volumen + Constants.volumen_elevador;
                            peso_aux = peso_aux + pedidos_del_dia[i].peso_pedido + Constants.peso_elevador;
                            pedido_a_entregar.Add(pedidos_del_dia[i]);
                            pedidos_del_dia.RemoveAt(i);
                            flag_elevador = true;
                        }
                        else
                        {
                            volumen_aux = volumen_aux + pedidos_del_dia[i].volumen;
                            peso_aux = peso_aux + pedidos_del_dia[i].peso_pedido;
                            pedido_a_entregar.Add(pedidos_del_dia[i]);
                            pedidos_del_dia.RemoveAt(i);
                        }
                    }


                }
                else if (pedidos_del_dia[i].barrio == localidad) //si no entra en el camion pero es de la localidad 
                {
                    flag = true;
                    estado = eOpcion.se_lleno_pero_quedaron_cosas_de_la_localidad;
                }



                if (volumen_aux >= (Constants.VOLUMEN_CAMIONETA - Constants.volumen_electro_pequeños) && flag == false) //si el camion se lleno y no tuvo problema de que no entro algo de la localidad
                    estado = eOpcion.se_lleno_completo_;

                return estado;
            }
        }
        else //se elegio la furgoneta
        {

            for (int i = 0; i < pedidos_del_dia.Count; i++)
            {
                if (volumen_aux < Constants.VOLUMEN_FURGONETA && peso_aux < Constants.PESOMAXFURGONETA)
                {
                    if ((volumen_aux + pedidos_del_dia[i].volumen) < Constants.VOLUMEN_FURGONETA && (peso_aux + pedidos_del_dia[i].peso_pedido) < Constants.PESOMAXFURGONETA && pedidos_del_dia[i].barrio == localidad)
                    {
                        if (Cantidad_Televisores(pedidos_del_dia[i]) > 0)
                        {
                            pedidos_del_dia[i].volumen_pedido_set = pedidos_del_dia[i].volumen - Constants.televisores; //le restamos el volumen del televisor al volumen total del pedido
                            int nuevo_volumen = 2 * 1 * 3; //ancho y profundidad del televisor pero alto del furgoneta
                            pedidos_del_dia[i].volumen_pedido_set = pedidos_del_dia[i].volumen + nuevo_volumen;
                            //una vez que ya esta actualizado el nuevo volumen, sigo como el resto de los pedidos sin televisor
                        }

                        volumen_aux = volumen_aux + pedidos_del_dia[i].volumen;
                        peso_aux = peso_aux + pedidos_del_dia[i].peso_pedido;
                        pedido_a_entregar.Add(pedidos_del_dia[i]);
                        pedidos_del_dia.RemoveAt(i);

                    }
                    else if (pedidos_del_dia[i].barrio == localidad) //si no entra en el camion pero es de la localidad 
                    {
                        flag = true;
                        estado = eOpcion.se_lleno_pero_quedaron_cosas_de_la_localidad;
                    }
                }
            }
            if (volumen_aux >= (Constants.VOLUMEN_CAMIONETA - Constants.volumen_electro_pequeños) && flag == false) //si el camion se lleno y no tuvo problema de que no entro algo de la localidad
                estado = eOpcion.se_lleno_completo_;

            return estado;
        }
        return eOpcion.se_lleno_completo_; //porque sino tira error, la idea es q nunca llegue aca

        }

    static void Ordenar_por_prioridad_pedido(List<pedido_por_cliente> lista)
        {
            //primero ordena por express y luego normales y luego diferidos
            List<pedido_por_cliente> aux = new List<pedido_por_cliente>();
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].tipo_entrega == entrega.express)
                {
                    //meto el pedido express a la lista auxiliar y la elimino de la original
                    aux.Add(lista[i]);
                    lista.RemoveAt(i);
                }
            }
            //para cuando salga del for, voy a tener en la lista aux solo los pedidos express y en la lista original los normales, ahora agrego los normales a la lista auxiliar (al final)
            for (int i = 0; i < lista.Count; i++)
            {
                aux.Add(lista[i]);
            }

            lista = aux; //ahora la lista esta ordenada por primero express y despues normales
        }

    static void rellenar_camion(List<eLocalidad> lista_localidades_normal, List<pedido_por_cliente> pedido_a_entregar, List<pedido_por_cliente> pedidos_del_dia_normales, eVehiculo camion)
        {
            eOpcion chequeo_camion_lleno = eOpcion.no_se_lleno;

            while (chequeo_camion_lleno == eOpcion.no_se_lleno && lista_localidades_normal != null)
            { //mientras que el camion no este lleno o mientras siga habiendo locaclidades que recorrer
                chequeo_camion_lleno = intento_llenar_camion(lista_localidades_normal[0], pedidos_del_dia_normales, camion, pedido_a_entregar); //mete los pedidos que van entrando en pedido_a_entregar

                if (chequeo_camion_lleno != eOpcion.se_lleno_pero_quedaron_cosas_de_la_localidad) //si se pudo meter todos los pedidos de esa locaclidad, la elimino porque ya no la van a tenr que recorrer 
                    lista_localidades_normal.RemoveAt(0);

            }
            //va a voler al main con la lista de pedidos a entregar agregando algunos pedidos normales, y si se lograron poner localidades enteras de pedidos normales, tamb los saca de la lista y va a voler al main la lista actualizada
        }

     static int Cantidad_Televisores(pedido_por_cliente pedido_del_dia)
    {
        int cont = 0;
        for(int i = 0; i < pedido_del_dia.cantidad_objetos; i++)
        {
            if (pedido_del_dia.compra_objetos[i].GetType() == typeof(televisor))//si el objeto es un televisor
                cont++;
        }
        return cont;
    }

    static bool Verifical_electro_pequeño(pedido_por_cliente pedido_del_dia)
    {
        int cont = 0;
        for (int i = 0; i < pedido_del_dia.cantidad_objetos; i++)
        {
            //si es un electrodomestico pequeño
            if (pedido_del_dia.compra_objetos[i].GetType() == typeof(licuadora )|| pedido_del_dia.compra_objetos[i].GetType() == typeof(exprimidor) || pedido_del_dia.compra_objetos[i].GetType() == typeof(rallador) || pedido_del_dia.compra_objetos[i].GetType() == typeof(tostadora) || pedido_del_dia.compra_objetos[i].GetType() == typeof(cafetera) || pedido_del_dia.compra_objetos[i].GetType() == typeof(molinillos))
                cont++;
        }
        if (cont > 0)
            return true;
        else
            return false;
    }

    static List<eVehiculo> camiones_disponibles(DateTime dia_hoy,List<eVehiculo> camiones_empresa)
    {
        List<eVehiculo> vector_camiones=new List<eVehiculo>();
        byte dia = (byte)dia_hoy.DayOfWeek;
        string dia_string = dia.ToString("dddd", new CultureInfo("es-ES"));//lo convierto en string
        switch (dia_string)
        {
            case "lunes":
                vector_camiones.Add(camiones_empresa.ElementAt(0)); //la camioneta
                vector_camiones.Add(camiones_empresa.ElementAt(1)); //el furgon
                vector_camiones.Add(camiones_empresa.ElementAt(2)); //la furgoneta
                break;
           case "martes":
                vector_camiones.Add(camiones_empresa.ElementAt(0)); 
                vector_camiones.Add(camiones_empresa.ElementAt(1));
                break;
            case "miercoles":
                vector_camiones.Add(camiones_empresa.ElementAt(0));
                vector_camiones.Add(camiones_empresa.ElementAt(2));
                break;
            case "jueves":
                vector_camiones.Add(camiones_empresa.ElementAt(0)); 
                vector_camiones.Add(camiones_empresa.ElementAt(1));
                break;
            case "viernes":
                vector_camiones.Add(camiones_empresa.ElementAt(0)); 
                vector_camiones.Add(camiones_empresa.ElementAt(1));
                vector_camiones.Add(camiones_empresa.ElementAt(2));
                break;
            case "sabado":
                vector_camiones.Add(camiones_empresa.ElementAt(0)); 
                break;
        }
        return vector_camiones;
    }
    static void Main(string[] args)
    {

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

    }
}
