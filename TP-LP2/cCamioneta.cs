using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sol_greedy_dinamica
{
    internal class cCamioneta : cVehiculo
    {
       
      
        public cCamioneta(int nafta_, int meses, float precio) : base(meses, nafta_, precio) 
        {
         
            peso = 0;
            volumen = 1441;
        }
        ~cCamioneta() { }
    }
}
