using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HaladoAlgoritmusok.Assets
{
    public struct int2
    {
        public int x;
        public int y;

        public int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator !=(int2 i1, int2 i2)
        {
            return i1.x != i2.x || i1.y != i2.y;
        }

        public static bool operator ==(int2 i1, int2 i2)
        {
            return i1.x == i2.x && i1.y == i2.y;
        }
    }
}
