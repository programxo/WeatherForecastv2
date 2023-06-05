using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Models
{
    public class GeoModel
    {
        public class Coordinates
        {
            public string Category { get; set; }
            public string Name { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }
        public class Place
        {
            public int place_id { get; set; }
            public string licence { get; set; }
            public string osm_type { get; set; }
            public long osm_id { get; set; }
            public List<string> boundingbox { get; set; }
            public string lat { get; set; }
            public string lon { get; set; }
            public string display_name { get; set; }
            public int place_rank { get; set; }
            public string category { get; set; }
            public string type { get; set; }
            public double importance { get; set; }
            public string icon { get; set; }
        }
    }
}
