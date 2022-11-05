using sol_greedy_dinamica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sol_greedy_dinamica
{
    internal class cElectrodomesticos
    {
        protected objetos tipo;
        public objetos tipoobjeto
        {
            get { return tipo; }
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

        public cElectrodomesticos(int volumen_, int peso_,int garantia_,objetos obj)
        {
            this.volumen=volumen_;
            this.tipo = obj;
            this.peso = peso_;
            this.garantia = garantia_;

        }

        ~cElectrodomesticos() { }
    }
}
