using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AircraftStates
    {
        public int Time { get; set; }

        public List<List<AircraftState>> States { get; set; }
    }
}
