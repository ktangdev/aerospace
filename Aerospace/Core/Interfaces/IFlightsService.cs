using Core.Models;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IFlightsService
    {
        IEnumerable<AircraftState> GetStates();
        IEnumerable<Flight> GetFlightsByTime(int beginTime, int endTime);
        IEnumerable<Flight> GetFlightsByAircraft(string icao, int beginTime, int endTime);
    }
}
