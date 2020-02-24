using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using StarWars.Models;
using Newtonsoft.Json;

namespace StarWars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarWarsAPIController : ControllerBase
    {
        private JsonDocument jDoc;
        Person person = new Person();
        Planet planet = new Planet();
        JsonElement planetUrl;
        JsonElement speciesUrl;
        List<JsonElement> residentUrlList = new List<JsonElement>();
        Random random = new Random();

        public async Task<Person> GetPerson()
        {
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

#warning sometimes there is a null value returned in species causing an error

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

        public async Task<Planet> GetPlanet()
        {
            int num = random.Next(61);
            using (var httpClient = new HttpClient())
            {
                // make a blank list of Person to add residents to later
                List<string> resident = new List<string>();

                using (var response = await httpClient.GetAsync($"https://swapi.co/api/planets/{num}"))
                {

                    var stringResponse = await response.Content.ReadAsStringAsync();

                    jDoc = JsonDocument.Parse(stringResponse);

                    var name = jDoc.RootElement.GetProperty("name");
                    var climate = jDoc.RootElement.GetProperty("climate");
                    var terrain = jDoc.RootElement.GetProperty("terrain");
                    var gravity = jDoc.RootElement.GetProperty("gravity");
                    var population = jDoc.RootElement.GetProperty("population");
                    var jsonList = jDoc.RootElement.GetProperty("residents");

                    var jsonResidentList = jsonList.EnumerateArray();

                    foreach (var residentUrl in jsonResidentList)
                    {
                        residentUrlList.Add(residentUrl);
                    }

                    planet.Name = name.ToString();
                    planet.Climate = climate.ToString();
                    planet.Terrain = terrain.ToString();
                    planet.Gravity = gravity.ToString();
                    planet.Population = population.ToString();
                }

                foreach (var residentUrl in residentUrlList)
                {
                    using (var response = await httpClient.GetAsync($"{residentUrl}"))
                    {
                        var stringResponse = await response.Content.ReadAsStringAsync();

                        jDoc = JsonDocument.Parse(stringResponse);

                        var name = jDoc.RootElement.GetProperty("name");

                        string nameString = name.ToString();

                        if (nameString != null)
                        {
                            resident.Add(nameString);
                        }
                        else
                        {
                            resident.Add("No Resident in Database");
                        }
                    }
                }
                planet.Residents = resident;
            }

            return planet;
        }
    }
}