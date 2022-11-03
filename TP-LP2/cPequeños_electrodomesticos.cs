using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sol_greedy_dinamica
{
    internal class cPequeños_electrodomesticos:cElectrodomesticos
    {
        public cPequeños_electrodomesticos(int volumen_, int peso_, int garantia_, objetos obj) : base(volumen_, peso_, garantia_, obj) { }
        ~cPequeños_electrodomesticos() { }
    }
}
