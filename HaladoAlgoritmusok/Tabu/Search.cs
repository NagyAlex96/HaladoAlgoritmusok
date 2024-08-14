using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaladoAlgoritmusok.Tabu
{
    public class Search
    {
        private List<Assignment> BestSolution; //jelenlegi legjobb megoldás(oka)t tartalmazza
        private List<Assignment> CurrentSolution;
        private List<List<Assignment>> TabuList; //tárolja a már meglátogatott megoldást

        public Search()
        {
            TabuList = new List<List<Assignment>>();
        }

        public List<Assignment> Solve(List<Job> jobs, List<Resource> resources, int maxIterations, int tabuTenure)
        {
            //generáljunk egy kezdeti megoldást
            CurrentSolution = GenerateInitialSolution(jobs, resources);
            BestSolution = new List<Assignment>(CurrentSolution);

            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                //minden iterációban (egy új) szomszédos megoldásokat generál
                List<Assignment> neighborSolution = GenerateNeighbourSolution(CurrentSolution);

                //tabu lista firssítése, hogy ne kapjuk vissza a korábban már elvégzett megoldást
                if (!IsTabu(neighborSolution))
                {
                    CurrentSolution = neighborSolution;
                    if (CalculateTotalDuration(CurrentSolution) < CalculateTotalDuration(BestSolution))
                    {
                        BestSolution = new List<Assignment>(CurrentSolution);
                    }

                    TabuList.Add(new List<Assignment>(CurrentSolution));
                    if (TabuList.Count > tabuTenure)
                    {
                        TabuList.RemoveAt(0);
                    }
                }
            }

            return BestSolution;
        }

        /// <summary>
        /// Kezdeti megoldás generálása. Szimplán minden feladatot egy erőforráshoz rendelünk
        /// </summary>
        /// <param name="jobs">Munkák</param>
        /// <param name="resources">Erőforrások</param>
        /// <returns>Generált kezdeti megoldás</returns>
        private List<Assignment> GenerateInitialSolution(List<Job> jobs, List<Resource> resources)
        {
            var solution = new List<Assignment>();
            int time = 0;
            foreach (var job in jobs)
            {
                var resource = resources[time % resources.Count];
                solution.Add(new Assignment { Job = job, Resource = resource, StartTime = DateTime.Now });
                time += job.DurationMinutes;
            }
            return solution;
        }
       
        /// <summary>
        /// Szomszédos megoldás generálása.
        /// </summary>
        /// <param name="currentSolution"></param>
        /// <returns></returns>
        private List<Assignment> GenerateNeighbourSolution(List<Assignment> currentSolution)
        {
            var neighbor = new List<Assignment>(currentSolution);
            var random = new Random();
            int i = random.Next(neighbor.Count);
            int j = random.Next(neighbor.Count);
            if (i != j)
            {
                // Feladatokat cseréljük ki
                var tempResource = neighbor[i].Resource;
                neighbor[i].Resource = neighbor[j].Resource;
                neighbor[j].Resource = tempResource;
            }
            return neighbor;
        }

        /// <summary>
        /// Megnézzük, hogy szerepel-e már a tabu listában
        /// </summary>
        /// <param name="solution"></param>
        /// <returns></returns>
        private bool IsTabu(List<Assignment> solution)
        {
            return TabuList.Any(tabuSolution => tabuSolution.SequenceEqual(solution, new Assignment()));
        }

        /// <summary>
        /// Kiszámítja az összes feladat befejezési idejét a jelenlegi megoldás alapján
        /// </summary>
        /// <param name="solution"></param>
        /// <returns></returns>
        private int CalculateTotalDuration(List<Assignment> solution)
        {
            return solution.Max(a => a.EndTime.Minute);
        }
    }
}
