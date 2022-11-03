using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sol_greedy_dinamica
{
    internal class cFurgoneta:cVehiculo
    {
        
        public cFurgoneta(int nafta_, int meses, float precio) : base(meses, nafta_, precio)
        {
            this.peso = 3500;
            volumen = 1233;
        }
        ~cFurgoneta() { }
    }
}
