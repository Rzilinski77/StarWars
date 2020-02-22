using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public string Gender { get; set; }
        public string HomePlanet { get; set; }

        public Person() { }

        public Person(string name, string species, string gender, string homePlanet)
        {
            Name = name;
            Species = species;
            Gender = gender;
            HomePlanet = homePlanet;
        }

    }
}
