using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaladoAlgoritmusok
{
    public class BasicHillClimbing
    {
        //Generáljuk le x szomszédait
        private static List<int> GenerateNeighbors(int x)
        {
            //Pl: egyszerűen x körüli számok, pl. x-1 és x+1
            return new List<int> { x - 1, x + 1 };
        }

        public static int HillClimbing(Func<int, int> f, int x0)
        {
            int x = x0; // Kezdeti megoldás
            while (true)
            {
                List<int> neighbors = GenerateNeighbors(x); // Szomszédok generálása
                int bestNeighbor = neighbors.MaxBy(f); // Szomszédok közül a legjobb kiválasztása

                if (f(bestNeighbor) <= f(x)) // Ha a legjobb szomszéd nem jobb, mint x
                    return x;

                x = bestNeighbor; // Ellenkező esetben folytasd a legjobb szomszéddal
            }
        }
    }
}
