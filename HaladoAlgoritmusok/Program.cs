using HaladoAlgoritmusok.Assets;
using HaladoAlgoritmusok.HillClimbing;
using HaladoAlgoritmusok.PhysicalMethod;
using HaladoAlgoritmusok.Tabu;

namespace HaladoAlgoritmusok
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BHillClimbing();
            //MazeHill();
            //Tabu();
            PhysicalMethod();




            Console.ReadKey();
        }

        static void Tabu()
        {
            List<Job> jobs = new List<Job>
            {
                new Job { Id = 1, DurationMinutes = 3 },
                new Job { Id = 2, DurationMinutes = 5 },
                new Job { Id = 3, DurationMinutes = 2 }
            };

            List<Resource> resources = new List<Resource>
            {
                new Resource { Id = 1, Capacity = 10 },
                new Resource { Id = 2, Capacity = 10 }
            };

            Search tabuSearch = new Search();
            List<Assignment> solution = tabuSearch.Solve(jobs, resources, 100, 5);

            Console.WriteLine("Legjobb megoldás:");
            foreach (var assignment in solution)
            {
                Console.WriteLine($"Feladat {assignment.Job.Id} hozzárendelve Erőforrás {assignment.Resource.Id}-hoz/hez. Kezdés: {assignment.StartTime}, Befejezés: {assignment.EndTime}");
            }
        }

        static void MazeHill()
        {
            //0 a járható út -- 1: fal/akadály
            //x(oszlopok száma) és y(sorok száma)
            int[,] maze =
            {
                {0,0,0},
                {1,1,0 },
                {0,0,0 },
                {0,1,1 },
                {0,0,0, }
            };


            int2 startPos = new int2(0, 0); //x és y pozíció
            int2 goalPos = new int2(1, 2); //x és y pozíció

            //!!!!!!!!!
            //összegzés: mindig az aktuálisan legjobb esetet nézi, és nem feltétlen találja meg a legjobb megoldást. Lehet, hogy vezetni fog út a a célhoz (pl: 0,0-tól indul és 2,4), közben mégsem találja meg, mivel a pillanatnyi (lokálisan) legjobb megoldást választja. Ellenben a 0,0 kiinduló pozíciótól eltalál a 2,0
            ///!!!

            Console.WriteLine($"{(MazeHillClimbing.MazeSearch(maze, startPos, goalPos) ? "van" : "nincs")} megoldás a célhoz eljutáshoz.");
        }

        static void BHillClimbing() //alap hegymászó algoritmus
        {
            // Példa használat
            //BasicHillClimbing bHill = new BasicHillClimbing();
            int x0 = 1;
            int xOut = BasicHillClimbing.HillClimbing(y => y * x0, x0);
            Console.WriteLine("Eredmény: " + xOut);
        }

        static void PhysicalMethod()
        {
            List<Point> points = new List<Point>
            {
                new Point(0, 0),
                new Point(2, 1),
                new Point(1, 3),
                new Point(5, 0),
                new Point(4, 4),
                new Point(6, 3)
            };

            double initialTemperature = 1000;
            double coolingRate = 0.01;

            List<Point> bestPolygon = SimulatedAnnealing.SimulatedAnnealingOptimization(points, initialTemperature, coolingRate);

            Console.WriteLine("Optimális Poligon Pontok:");
            foreach (var point in bestPolygon)
            {
                Console.WriteLine($"({point.X}, {point.Y})");
            }
        }
    }
}
