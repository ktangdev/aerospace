using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var test = _flightsService.GetStates();
            var test2 = _flightsService.GetFlightsByTime(1517227200, 1517230800);

            ViewData["message"] = test.FirstOrDefault().Icao24;

            return View(test);
        }
    }
}