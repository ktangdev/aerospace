using Core.Models;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IFlightsService
    {
        IEnumerable<AircraftState> GetStates();
    }
}
