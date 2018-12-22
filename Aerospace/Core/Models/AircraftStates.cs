using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    [Serializable]
    public class AircraftStates
    {
        public int Time { get; set; }

        public string[,] States { get; set; }
    }
}
