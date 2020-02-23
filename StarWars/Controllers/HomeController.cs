using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarWars.Models;

namespace StarWars.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        StarWarsAPIController starwarsAPI = new StarWarsAPIController();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // we make an async method that will call the starwarsController
        // and return to us a Person Object
        public async Task<IActionResult> GetPerson()
        {
            Person person = await starwarsAPI.GetPerson();
                
            return View(person);
        }

        // we make an async method that will call the starwarsController
        // and return to us a Planet
        public async Task<IActionResult> GetPlanet()
        {
            Planet planet = await starwarsAPI.GetPlanet();

            return View(planet);
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
