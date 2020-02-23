using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Models
{
    public class Planet
    {
        public string Name { get; set; }
        public string Climate { get; set; }
        public string Terrain { get; set; }
        public string Gravity { get; set; }
        public string Population { get; set; }
        public List<Person> Residents { get; set; }
    }
}
