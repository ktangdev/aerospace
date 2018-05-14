using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Aircraft
    {
        /// <summary>
        /// The state of an aircraft is a summary of all tracking information (mainly position, velocity, and identity) at a certain point in time. These states of aircraft can be retrieved as state vectors in the form of a JSON object.
        /// </summary>
        public AircraftState AircraftState { get; set; }
    }
}
