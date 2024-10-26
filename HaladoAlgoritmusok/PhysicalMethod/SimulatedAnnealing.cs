using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaladoAlgoritmusok.PhysicalMethod
{
    public class SimulatedAnnealing
    {
        // Energiafüggvény (poligon kerülete vagy területe)
        private static double EnergyFunction(List<Point> points)
        {
            double area = 0;
            for (int i = 0; i < points.Count; i++)
            {
                int j = (i + 1) % points.Count;
                area += points[i].X * points[j].Y - points[j].X * points[i].Y;
            }
            return Math.Abs(area / 2.0);
        }

        // Pontok cseréje
        private static List<Point> Perturb(List<Point> points)
        {
            Random rnd = new Random();
            int i = rnd.Next(points.Count);
            int j = rnd.Next(points.Count);

            Point temp = points[i];
            points[i] = points[j];
            points[j] = temp;

            return points;
        }

        // Elfogadási valószínűség számítása
        private static double AcceptanceProbability(double currentEnergy, double newEnergy, double temperature)
        {
            if (newEnergy < currentEnergy)
            {
                return 1.0;
            }
            return Math.Exp((currentEnergy - newEnergy) / temperature);
        }

        // Szimulált hűtés algoritmus
        public static List<Point> SimulatedAnnealingOptimization(List<Point> initialPoints, double initialTemperature, double coolingRate)
        {
            List<Point> currentSolution = new List<Point>(initialPoints);
            List<Point> bestSolution = new List<Point>(initialPoints);
            double currentTemperature = initialTemperature;

            while (currentTemperature > 1)
            {
                List<Point> newSolution = Perturb(new List<Point>(currentSolution));
                double currentEnergy = EnergyFunction(currentSolution);
                double newEnergy = EnergyFunction(newSolution);

                // Ha az új megoldás jobb a valószínűség alapján
                if (AcceptanceProbability(currentEnergy, newEnergy, currentTemperature) > new Random().NextDouble())
                {
                    currentSolution = newSolution;
                }

                // Ha az új megoldás jobb, mint a legjobb (amit ismerünk)
                if (EnergyFunction(currentSolution) < EnergyFunction(bestSolution))
                {
                    bestSolution = new List<Point>(currentSolution);
                }

                // Hőmérséklet csökkentése
                currentTemperature *= 1 - coolingRate;
            }

            return bestSolution;
        }
    }
}
