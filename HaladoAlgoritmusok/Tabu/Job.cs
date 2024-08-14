using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaladoAlgoritmusok.Tabu
{
    public class Job
    {
        //feladatokat szimulálja, minden feladatnak van egy azonosítója és egy végrehajtási ideje

        public int Id { get; set; }
        public int DurationMinutes { get; set; }
    }
}
