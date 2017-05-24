using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baigiamasis_d
{
    // Klasė, skirta klaidų valdymui
    class Klaida : Exception
    {
        public Klaida(string message) : base(message)
        {

        }
    }
}
