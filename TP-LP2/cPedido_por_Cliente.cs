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
       

        ~cPedido_por_Cliente() { }
    }
}