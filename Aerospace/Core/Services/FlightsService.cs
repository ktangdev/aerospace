using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FlightsService : IFlightsService
    {
        readonly HttpClient httpClient = new HttpClient();

        public string GetFlights()
        {
            return "Hello World";
        }
    }
}
