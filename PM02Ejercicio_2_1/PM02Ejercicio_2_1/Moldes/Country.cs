using System;
using System.Collections.Generic;
using System.Text;

namespace PM02Ejercicio_2_1.Moldes
{
    public class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Population { get; set; }
        public List<string> Languages { get; set; }
        public List<string> Currencies { get; set; }
    }
}
