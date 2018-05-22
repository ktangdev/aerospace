using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Aerospace.Controllers
{
    public class FlightsController : Controller
    {
        private IFlightsService _flightsService;

        /// <summary>
        /// Constructor which uses dependency injection to instantiate services
        /// </summary>
        /// <param name="flightsService"></param>
        public FlightsController(IFlightsService flightsService)
        {
            _flightsService = flightsService;
        }

        public IActionResult Index()
        {
            var test = _flightsService.GetFlights();

            List<AircraftState> testObj = JsonConvert.DeserializeObject<List<AircraftState>>(test);

            var k = testObj[0].Origin_Country;

            ViewData["message"] = k;

            return View();
        }
    }
}