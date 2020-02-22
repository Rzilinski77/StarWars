using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using StarWars.Models;

namespace StarWars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarWarsAPIController : ControllerBase
    {
        private JsonDocument jDoc;
        Person person = new Person();
        JsonElement planetUrl;
        JsonElement speciesUrl;

        public async Task<Person> GetData()
        {
            Random random = new Random();
            int num = random.Next(87);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://swapi.co/api/people/{num}"))
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    jDoc = JsonDocument.Parse(stringResponse);

                    var name = jDoc.RootElement.GetProperty("name");
                    var gender = jDoc.RootElement.GetProperty("gender");
                    planetUrl = jDoc.RootElement.GetProperty("homeworld");
                    var speciesArray = jDoc.RootElement.GetProperty("species");
                    speciesUrl = speciesArray[0];

                    person.Name = name.ToString();
                    person.Gender = gender.ToString();
                }

                using (var response = await httpClient.GetAsync($"{planetUrl}"))
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    jDoc = JsonDocument.Parse(stringResponse);

                    var homePlanet = jDoc.RootElement.GetProperty("name");

                    person.HomePlanet = homePlanet.ToString();
                }

                using (var response = await httpClient.GetAsync($"{speciesUrl}"))
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    jDoc = JsonDocument.Parse(stringResponse);

                    var species = jDoc.RootElement.GetProperty("name");

                    person.Species = species.ToString();
                }
            }

            return person;
        }
    }
}