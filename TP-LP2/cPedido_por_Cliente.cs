using sol_greedy_dinamica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sol_greedy_dinamica
{
    internal class cPedido_por_Cliente
    {
    
        //atributos
        string nombre_cliente;
        public string nombre
        {
            get { return nombre_cliente; }
        }
       
        List<cElectrodomesticos> compra;
        public List<cElectrodomesticos> compra_objetos
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
        public cPedido_por_Cliente(string nombre_, eLocalidad barrio_, List<cElectrodomesticos> obj, entrega entrega_compra_)//ver del cuaderno de progra1 cmo era lo de id
        {
            nombre_cliente = nombre_;
            barrio_a_entregar = barrio_;
            compra = obj;
            cant_de_objetos = obj.Count;
            volumen_total = calculo_volumen_total(compra);
            peso_total = calculo_peso_total(compra);
            entrega_compra = entrega_compra_;


        }


        int calculo_volumen_total(List<cElectrodomesticos> compra)
        {
            int suma = 0;
            for (int i = 0; i < compra.Count; i++)
            {
                suma = suma + compra[i].volumenobjeto; //peso elemento(objeto) le paso un objeto y me devuelve su volumen

            }
            return suma;
        }
        int calculo_peso_total(List<cElectrodomesticos> compra)
        {
            int suma = 0;
            for (int i = 0; i < compra.Count; i++)
            {
                suma = suma + compra[i].pesoobjeto; //peso elemento(objeto) le paso un objeto y me devuelve cuanto pesa
            }
            return suma;
        }

        static int[] ValorDeCadaPedido(List<cPedido_por_Cliente> lista)
        {
            int[] valores=new int[lista.Count];
            for(int i; i < lista.Count; i++)
            {
                if (lista[i].tipo_entrega=entrega.express)
                    valores[i]=1000;
                if (lista[i].tipo_entrega=entrega.normal)
                    valores[i]=50;
                if (lista[i].tipo_entrega=entrega.diferido)
                    valores[i]=0;
            }
            return valores;
        }

         static void LLenadoDinamicoDelCamion(cVehiculo camion, List<cPedido_por_Cliente> pedidos_del_dia, List<cPedido_por_Cliente> pedido_a_entregar)
        {
            int i, w;

            int[] valor= ValorDeCadaPedido(pedidos_del_dia);
             int [,]K = new int[pedidos_del_dia.Count + 1,camion.peso_max + 1];

            for (i = 0; i <= pedidos_del_dia.Count; i++) {
            for (w = 0; w <= camion.peso_max; w++) {
                if (i == 0 || w == 0)
                    K[i,w] = 0;
                else if (pedidos_del_dia[i-1].peso <= w)
                    K[i,w] = Math.Max(valor[i-1] + K[i - 1,w - pedidos_del_dia[i-1].peso], K[i - 1,w]);
                else
                    K[i,w] = K[i - 1,w];
            }
            }

             int res = K[n,W];
 
        w = camion.peso_max;
        for (i = n; i > 0 && res > 0; i--) {
 
            if (res == K[i - 1,w])
                continue;
            else {
                    
               pedido_a_entregar.Add(pedidos_del_dia[i-1]);
              pedidos_del_dia.removeAT(i-1);

            }
        }

        }





        ~cPedido_por_Cliente() { }
    }
}