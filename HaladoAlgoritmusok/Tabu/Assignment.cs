using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaladoAlgoritmusok.Tabu
{
    public class Assignment : IEqualityComparer<Assignment>
    {
        //az osztály egy feladatot hivatott hozzárendelni egy erőforráshoz, és tárolja a kezdeti és befejezési időpontot
        public Job Job { get; set; }
        public Resource Resource { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime => StartTime.AddMinutes(Job.DurationMinutes);

        public bool Equals(Assignment x, Assignment y)
        {
            if (x == null || y == null)
                return false;

            return x.Job.Id == y.Job.Id && x.Resource.Id == y.Resource.Id;
        }

        public int GetHashCode(Assignment obj)
        {
            if (obj == null)
                return 0;

            return obj.Job.Id.GetHashCode() ^ obj.Resource.Id.GetHashCode();
        }
    }
}
