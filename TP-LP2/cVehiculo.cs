using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sol_greedy_dinamica
{
    internal class cVehiculo
    {
        protected int peso;
        public int peso_max
        {
            get { return peso; }
            set { }

        }

        protected int volumen;
        public int volumen_max
        {
            get { return volumen; }
            set { }
        }

        protected float precio_actualventa;
        public float precio_vehiculo
        {
            get { return precio_actualventa; }
            set { }
        }

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

        private bool elevador_;
        public bool elevador
        {
            get { return elevador_; }
            set { }
        }

        public cVehiculo(int meses_,int nafta,float precio)
        {
            this.nafta = nafta;
            this.cant_meses_uso = meses_;
            this.precio_actualventa = precio;
            elevador_ = false;
            
        }

       

        ~cVehiculo() { }
}
}
