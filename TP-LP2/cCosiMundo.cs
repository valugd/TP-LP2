using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sol_greedy_dinamica
{
    internal class cCosiMundo
    {
        private List<cPedido_por_Cliente> lista_pedidos;
        public List<cPedido_por_Cliente> listaPedidos
        {
            get { return lista_pedidos; }
            set { }
        }

        private List<cVehiculo> lista_camiones;
        public List<cVehiculo> listaCamiones
        {
            get { return lista_camiones; }
            set { }
        }

        public cCosiMundo(List<cPedido_por_Cliente> listpedidos, List<cVehiculo> listcamiones)
        {
            this.listaPedidos = listpedidos;
            this.listaCamiones = listcamiones;
        }

        public void preparo_y_desapacho_de_productos()
        {
            //filtramos la lista completa pasada por parametro en listas de las localidades con pedidos express y normales, y los pedidos express y normales
            List<cPedido_por_Cliente> pedidos_del_dia_express = Filtrar_por_pedido(this.lista_pedidos, entrega.express);
            List<eLocalidad> lista_localidades_express = Lista_Barrios_Ordenada(pedidos_del_dia_express);

            List<cPedido_por_Cliente> pedidos_del_dia_normales = Filtrar_por_pedido(this.lista_pedidos, entrega.normal);
            List<eLocalidad> lista_localidades_normal = Lista_Barrios_Ordenada(pedidos_del_dia_normales);

            List<cPedido_por_Cliente> pedido_a_entregar = new List<cPedido_por_Cliente>();


            cVehiculo camion = this.lista_camiones.ElementAt(0); //siempre empezamos con la camioneta
            int cont_camiones = 0;
            int max_viajes = max_viajes_por_dia();
            while (cont_camiones < max_viajes && (lista_localidades_express != null || lista_localidades_normal != null))
            {//hasta que no haya mas camiones o haya despachado todos los productos
                despacho_de_productos(lista_localidades_normal, pedidos_del_dia_express, pedidos_del_dia_normales, lista_localidades_express, pedido_a_entregar, camion); //calculo el mejor camino, y despacho todos los paquetes posibles, dandole prioridad a los express
                cont_camiones++;//se lleno el camion anterior, uso el siguiente
                if (cont_camiones == 4) //seguimos con el furgon
                    camion = this.lista_camiones.ElementAt(1);
                if (cont_camiones == 5)//seguimos con la furgoneta
                    camion = this.lista_camiones.ElementAt(2);
                pedido_a_entregar.RemoveRange(0, pedido_a_entregar.Count); //como los vamos entregando, borro la lista porque ya salio el camion

            }

        }

        public int max_viajes_por_dia()
        {
            int viajes = 4;  //la camioneta siemrpe sale por lo que siempre voy a tener 4 viajes minimo
            if (this.lista_camiones.Count == 2) //si solo sale el furgon o la furgoneta mas la camioneta
                viajes++;
            if (this.lista_camiones.Count == 2) //si salen los 3 vehiculos
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

        static int Barrios_en_pedido_del_dia(List<cPedido_por_Cliente> lista_pedido)
        {
            List<cPedido_por_Cliente> aux = new List<cPedido_por_Cliente>();
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

            if (chequeo_verificacion_barrios(verificacion_barrios) == -2)//si todavia no se recorrio ningun barrio, voy al que mas cerca este de liniers primero
            {
                for (int h = 0; h < barrios; h++)
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
                    {
                        orden_clientes.Add(lista_localidad[h]);
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

        static void despacho_de_productos(List<eLocalidad> lista_localidades_normal, List<cPedido_por_Cliente> pedidos_del_dia_express, List<cPedido_por_Cliente> pedidos_del_dia_normales, List<eLocalidad> lista_localidades_express, List<cPedido_por_Cliente> pedido_a_entregar, cVehiculo camion)
        {//las listas de localidades estan ordenadas por orden de menor distancia a liniers a mayor
            eOpcion chequeo_camion_lleno = eOpcion.no_se_lleno;
            List<eLocalidad> camino_mas_corto = new List<eLocalidad>();

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
            Ordenar_por_pedidio(camino_mas_corto, pedido_a_entregar);

            llenado_despacho_productos(camion, pedido_a_entregar); //esta funcion me va a llenar el camión que yo le pase por parámetro, con los pedidos de las localidades seleccionadas



        }
        static void Ordenar_por_pedidio(List<eLocalidad> camino_mas_corto, List<cPedido_por_Cliente> pedido_a_entregar)
        {
            List<cPedido_por_Cliente> pedidos_ordenados = new List<cPedido_por_Cliente>();
            List<cPedido_por_Cliente> aux = new List<cPedido_por_Cliente>();

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
            pedido_a_entregar = pedidos_ordenados;

        }

        static void Ordenar_por_prioridad_pedido(List<cPedido_por_Cliente> lista)
        {
            //primero ordena por express y luego normales y luego diferidos
            List<cPedido_por_Cliente> aux = new List<cPedido_por_Cliente>();
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
        static void llenado_despacho_productos(cVehiculo camion, List<cPedido_por_Cliente> lista_completa_pedidos)
        {
            //lista pedidos esta fltrada por solo las zonas que estan en el recorrido y primero estan puestos los del envio express, esto lo hacemos desde las funciones en donde llamamos a esta funcion, es por esto que siempre agarramos la posicion 0, porque vamos eliminando al primero despues de meterlo al camion -> siempre el primero de la lista es el que tenemos que meter primero
            Queue<cPedido_por_Cliente> pedidos_a_entregar = new Queue<cPedido_por_Cliente>(); //cola donde vamos a ir poniendo los pedidos FIRST IN -> FIRST OUT
            int volumen_aux = 0;
            int peso_aux = 0;

            if (camion.GetType() == typeof(cCamioneta))//si el camion seleccionado es la camioneta
            {//las camionetas no tiene limite de peso, solo de volumen
                while (volumen_aux < camion.volumen_max) //mientras que el volumen que tiene sea menor que el max de la camioneta
                {
                    if (volumen_aux + lista_completa_pedidos[0].volumen <= camion.volumen_max) //cheque que si le sumo el paquete siguiente no supere el volumen total.
                    {
                        pedidos_a_entregar.Enqueue(lista_completa_pedidos[0]); //lo agregamos a la cola
                        lista_completa_pedidos.RemoveAt(0); //sacamos a ese pedido de la lista
                        volumen_aux = volumen_aux + lista_completa_pedidos[0].volumen; //sumamos el volumen
                    }
                    else
                        volumen_aux = camion.volumen_max;//si ya sumandole el prox paquete, supero el peso del camion, le impongo que el peso es el max para que salga del while

                }
            }
            else if (camion.GetType() == typeof(cFurgon)) //el camion seleccionado es el furgon
            {//en este caso chequeamos tanto el volumen total y el peso total 
                while (volumen_aux < camion.volumen_max && peso_aux < camion.peso_max)
                {
                    if (volumen_aux + lista_completa_pedidos[0].volumen < camion.volumen_max && (peso_aux + lista_completa_pedidos[0].peso_pedido) < camion.peso_max)
                    {
                        pedidos_a_entregar.Enqueue(lista_completa_pedidos[0]);
                        lista_completa_pedidos.RemoveAt(0);
                        volumen_aux = volumen_aux + lista_completa_pedidos[0].volumen;
                        peso_aux = peso_aux + lista_completa_pedidos[0].peso_pedido;
                    }
                    else
                    {
                        volumen_aux = camion.volumen_max;
                        peso_aux = camion.peso_max;
                    }


                }
            }
            else //cont es igual a 5 -> furgoneta
            {//en este caso chequeamos tanto el volumen total y el peso total
                while (volumen_aux < camion.volumen_max && peso_aux < camion.peso_max)
                {
                    if (volumen_aux + lista_completa_pedidos[0].volumen < camion.volumen_max && (peso_aux + lista_completa_pedidos[0].peso_pedido) < camion.peso_max)
                    {
                        pedidos_a_entregar.Enqueue(lista_completa_pedidos[0]);
                        lista_completa_pedidos.RemoveAt(0);
                        volumen_aux = volumen_aux + lista_completa_pedidos[0].volumen;
                        peso_aux = peso_aux + lista_completa_pedidos[0].peso_pedido;
                    }
                    else
                    {
                        volumen_aux = camion.volumen_max;
                        peso_aux = camion.peso_max;
                    }


                }
            }
        }

        static List<cPedido_por_Cliente> Filtrar_por_pedido(List<cPedido_por_Cliente> pedidos_del_dia_, entrega tipo_entrega)
        {
            List<cPedido_por_Cliente> aux = new List<cPedido_por_Cliente>();

            for (int i = 0; i < pedidos_del_dia_.Count; i++)
            {
                if (pedidos_del_dia_[i].tipo_entrega == tipo_entrega)
                    aux.Add(pedidos_del_dia_[i]);
            }
            return aux;
        }

        static List<eLocalidad> Lista_Barrios_Ordenada(List<cPedido_por_Cliente> pedidos_del_dia_tipo_de_pedido)
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

        static int[,] llenar_matriz_con_distancias(List<eLocalidad> lista_localidades, int barrios)
        {

            //esta es la matriz que ya esta predeterminada, tiene todas las distancias de cada localidad a localidad
            int[,] matriz_definitiva = new int[25, 25];
            List<int> list_liniers = new List<int>() { 6, 11, 17, 5, 18, 19, 21, 3, 2, 4, 7, 8, 9, 10, 12, 13, 20, 14, 27, 15, 16, 18, 25, 23 };
            List<int> list_tresfebrero = new List<int>() { 7, 14, 26, 21, 19, 23, 3, 6, 9, 5, 8, 10, 13, 4, 11, 15, 22, 23, 12, 17, 2524, 16 };
            List<int> list_sanmartin = new List<int>() { 12, 30, 26, 24, 28, 8, 13, 15, 9, 16, 17, 18, 5, 6, 14, 16, 20, 19, 12, 27, 29, 8 };
            List<int> list_vicentelopez = new List<int>() { 40, 33, 31, 20, 15, 18, 21, 16, 19, 22, 25, 12, 8, 6, 10, 12, 16, 30, 15, 24, 23 };
            List<int> list_lamatanza = new List<int>() { 31, 30, 24, 5, 22, 20, 7, 28, 8, 10, 9, 14, 23, 27, 29, 13, 11, 21, 24, 17 };
            List<int> list_lomasdezamora = new List<int>() { 9, 30, 20, 21, 15, 23, 19, 16, 13, 24, 28, 36, 25, 43, 20, 17, 27, 28, 24 };
            List<int> list_lanus = new List<int>() { 6, 17, 14, 15, 16, 1, 13, 12, 21, 26, 2, 18, 15, 18, 12, 13, 10, 20 };
            List<int> list_avellaneda = new List<int>() { 20, 17, 18, 19, 3, 16, 15, 25, 29, 37, 15, 8, 12, 16, 7, 4, 26 };
            List<int> list_versalles = new List<int>() { 2, 5, 3, 4, 8, 9, 5, 11, 18, 12, 24, 8, 11, 20, 19, 10 };
            List<int> list_villaluro = new List<int>() { 4, 3, 2, 4, 6, 8, 11, 10, 12, 27, 13, 5, 15, 16, 9 };
            List<int> list_mataderos = new List<int>() { 6, 4, 2, 3, 9, 14, 22, 13, 30, 8, 5, 17, 18, 11 };
            List<int> list_montecastro = new List<int>() { 2, 5, 10, 3, 8, 10, 9, 24, 7, 5, 18, 20, 7 };
            List<int> list_velez = new List<int>() { 2, 4, 3, 10, 11, 8, 14, 5, 3, 15, 16, 6 };
            List<int> list_parqueavellaneda = new List<int>() { 4, 6, 13, 12, 10, 16, 6, 3, 14, 15, 9 };
            List<int> list_villadevoto = new List<int>() { 12, 20, 27, 15, 35, 10, 7, 16, 17, 16 };
            List<int> list_villaurquiza = new List<int>() { 5, 8, 10, 21, 9, 8, 23, 24, 7 };
            List<int> list_belgrano = new List<int>() { 4, 7, 18, 8, 19, 27, 29, 5 };
            List<int> list_palermo = new List<int>() { 4, 8, 7, 11, 3, 16, 4 };
            List<int> list_retiro = new List<int>() { 5, 4, 10, 9, 14, 2 };
            List<int> list_caballito = new List<int>() { 8, 12, 4, 7, 8 };
            List<int> list_flores = new List<int>() { 4, 8, 10, 5 };
            List<int> list_puertomadero = new List<int>() { 10, 15, 8 };
            List<int> list_laboca = new List<int>() { 3, 13 };
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
            for (int i = 0; i < barrios; i++)
            {
                barrio_fijo = num_asignado_barrio(lista_localidades.ElementAt(0));
                num_barrios_en_matriz[i] = barrio_fijo;
            }
            //tengo un vector con los numeros de los barrios que necesito correspondiente a la matriz ya definida


            for (int i = 0; i < lista_localidades.Count; i++)
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
            for (int j = 0; j < barrios_a_recorrer; j++)
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

        static eOpcion intento_llenar_camion(eLocalidad localidad, List<cPedido_por_Cliente> pedidos_del_dia, cVehiculo camion, List<cPedido_por_Cliente> pedido_a_entregar)
        {

            //llena el camion con los pedidos de la locacalidad pasada como paremetro, si logra llenar con todos los pedidos de la localidad y sigue habiendo espacio, devuelve que no se lleno. Si logra poner TODOS los pedidos de la localidad y no queda espacio, es decir, el volumen que ya lleno el camion es igual o mayor que el volumen total sin la caja mas pequeña de entrega entonces devuelvo que se llenó completo
            //si mientras lo lleno hay un pedido que no puedo meter de la locaclidad porque ya superaria el peso o volumen total del camion, devuelvo que se lleno pero que quedaron cosas de la locacalidad sin poner. Igualmente, auqnue no va a poner el pedido que agregandolo superaria el volumen/peso maximo va a seguir buscando a ver si puede meter otro pedido de menor volumen o peso al camion
            //de esta manera, el camion siempre va a salir lo mas lleno posible
            eOpcion estado = eOpcion.no_se_lleno;
            bool flag = false; //para saber si se pudo meter todo lo de la localidad o no, false es que si, true es que no
            int volumen_aux = 0;
            int peso_aux = 0;


            //camion elegido -> camioneta
            if (camion.GetType() == typeof(cCamioneta))
            {

                for (int i = 0; i < pedidos_del_dia.Count; i++)//recorro toda la lista 
                {
                    if (volumen_aux < camion.volumen_max) //si el volumen que voy sumando de los productos que voy metiendo al camion no supera al volumen total, mismo pensamiento con el peso
                    {
                        if ((volumen_aux + pedidos_del_dia[i].volumen) < camion.volumen_max && pedidos_del_dia[i].barrio == localidad)
                        {
                            if (Verifical_electro_pequeño(pedidos_del_dia[i]) == true && camion.elevador == false)
                            {
                                volumen_aux = volumen_aux + pedidos_del_dia[i].volumen + Constants.volumen_elevador;
                                peso_aux = peso_aux + pedidos_del_dia[i].peso_pedido + Constants.peso_elevador;
                                pedido_a_entregar.Add(pedidos_del_dia[i]);
                                pedidos_del_dia.RemoveAt(i);
                                camion.elevador = true;
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
                if (volumen_aux >= (camion.volumen_max - Constants.volumen_electro_pequeños) && flag == false) //si no queda mucho espacio , ni siquiera para que entre una pedido chico mas y aparte nunca estuvo en la situcion de que no entro un pedido de la localidad
                    estado = eOpcion.se_lleno_completo_;

                return estado;
            }
            else if (camion.GetType() == typeof(cFurgon)) // camion elegido->furgon
            {

                for (int i = 0; i < pedidos_del_dia.Count; i++)
                {
                    if (volumen_aux < camion.volumen_max && peso_aux < camion.peso_max)
                    {
                        if ((volumen_aux + pedidos_del_dia[i].volumen) < camion.volumen_max && (peso_aux + pedidos_del_dia[i].peso_pedido) < camion.peso_max && pedidos_del_dia[i].barrio == localidad)
                        {
                            if (Cantidad_Televisores(pedidos_del_dia[i]) > 0)
                            {
                                pedidos_del_dia[i].volumen_pedido_set = pedidos_del_dia[i].volumen - Constants.televisores; //le restamos el volumen del televisor al volumen total del pedido
                                int nuevo_volumen = 2 * 1 * 4; //ancho y profundidad del televisor pero alto del furgon
                                pedidos_del_dia[i].volumen_pedido_set = pedidos_del_dia[i].volumen + nuevo_volumen;
                                //una vez que ya esta actualizado el nuevo volumen, sigo como el resto de los pedidos sin televisor
                            }

                            if (Verifical_electro_pequeño(pedidos_del_dia[i]) == true && camion.elevador == false)
                            {
                                volumen_aux = volumen_aux + pedidos_del_dia[i].volumen + Constants.volumen_elevador;
                                peso_aux = peso_aux + pedidos_del_dia[i].peso_pedido + Constants.peso_elevador;
                                pedido_a_entregar.Add(pedidos_del_dia[i]);
                                pedidos_del_dia.RemoveAt(i);
                                camion.elevador = true;
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



                    if (volumen_aux >= (camion.volumen_max - Constants.volumen_electro_pequeños) && flag == false) //si el camion se lleno y no tuvo problema de que no entro algo de la localidad
                        estado = eOpcion.se_lleno_completo_;

                    return estado;
                }
            }
            else //se elegio la furgoneta
            {

                for (int i = 0; i < pedidos_del_dia.Count; i++)
                {
                    if (volumen_aux < camion.volumen_max && peso_aux < camion.peso_max)
                    {
                        if ((volumen_aux + pedidos_del_dia[i].volumen) < camion.volumen_max && (peso_aux + pedidos_del_dia[i].peso_pedido) < camion.peso_max && pedidos_del_dia[i].barrio == localidad)
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
                if (volumen_aux >= (camion.volumen_max - Constants.volumen_electro_pequeños) && flag == false) //si el camion se lleno y no tuvo problema de que no entro algo de la localidad
                    estado = eOpcion.se_lleno_completo_;

                return estado;
            }
            return eOpcion.se_lleno_completo_; //porque sino tira error, la idea es q nunca llegue aca

        }

        static void rellenar_camion(List<eLocalidad> lista_localidades_normal, List<cPedido_por_Cliente> pedido_a_entregar, List<cPedido_por_Cliente> pedidos_del_dia_normales, cVehiculo camion)
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

        static int Cantidad_Televisores(cPedido_por_Cliente pedido_del_dia)
        {
            int cont = 0;
            for (int i = 0; i < pedido_del_dia.cantidad_objetos; i++)
            {
                if (pedido_del_dia.compra_objetos[i].GetType() == typeof(cTelevisores))//si el objeto es un televisor
                    cont++;
            }
            return cont;
        }

        static bool Verifical_electro_pequeño(cPedido_por_Cliente pedido_del_dia)
        {
            int cont = 0;
            for (int i = 0; i < pedido_del_dia.cantidad_objetos; i++)
            {
                //si es un electrodomestico pequeño
                if (pedido_del_dia.compra_objetos[i].GetType() == typeof(cPequeños_electrodomesticos))
                    cont++;
            }
            if (cont > 0)
                return true;
            else
                return false;
        }

         static void verificar_devaluacion_camiones(List<cVehiculo> lista_camiones_def)
        {
            //sabemos en el pos0 esta la camioneta, en la pos1 el furgon y en la pos2 la furgoneta
            for (int i = 0; i < lista_camiones_def.Count; i++)
            {
                //si el vehiculo tiene 1 año de uso, se desprecia un 25% de su precio
                if (lista_camiones_def.ElementAt(i).meses_uso == 12)//si el vehiculo se uso por un año
                {
                    lista_camiones_def.ElementAt(i).precio_vehiculo = (float)(lista_camiones_def.ElementAt(i).precio_vehiculo * 0.75);
                    lista_camiones_def.ElementAt(i).meses_uso = 0;
                }
            }
        }


        public List<cVehiculo> camiones_disponibles(DateTime dia_hoy, List<cVehiculo> camiones_empresa)
        {
            List<cVehiculo> vector_camiones = new List<cVehiculo>();
            byte dia = (byte)dia_hoy.DayOfWeek;
            switch (dia)
            {
                case 1: //lunes
                    vector_camiones.Add(camiones_empresa.ElementAt(0)); //la camioneta
                    vector_camiones.Add(camiones_empresa.ElementAt(1)); //el furgon
                    vector_camiones.Add(camiones_empresa.ElementAt(2)); //la furgoneta
                    break;
                case 2: //martes
                    vector_camiones.Add(camiones_empresa.ElementAt(0));
                    vector_camiones.Add(camiones_empresa.ElementAt(1));
                    break;
                case 3: //miercoles
                    vector_camiones.Add(camiones_empresa.ElementAt(0));
                    vector_camiones.Add(camiones_empresa.ElementAt(2));
                    break;
                case 4: //jueves
                    vector_camiones.Add(camiones_empresa.ElementAt(0));
                    vector_camiones.Add(camiones_empresa.ElementAt(1));
                    break;
                case 5: //viernes
                    vector_camiones.Add(camiones_empresa.ElementAt(0));
                    vector_camiones.Add(camiones_empresa.ElementAt(1));
                    vector_camiones.Add(camiones_empresa.ElementAt(2));
                    break;
                case 6: //sabado
                    vector_camiones.Add(camiones_empresa.ElementAt(0));
                    break;
            }
            return vector_camiones;
        }

        static void sumar_mes_camiones(List<cVehiculo> lista_camiones)
        {
            for (int i = 0; i < 3; i++)
            {
                lista_camiones.ElementAt(i).meses_uso = lista_camiones.ElementAt(0).meses_uso + 1;
            }
            verificar_devaluacion_camiones(lista_camiones);
        }


        ~cCosiMundo() { }



    }
}
