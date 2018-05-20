﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Services;
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
            ViewData["message"] = _flightsService.GetFlights();

            return View();
        }
    }
}