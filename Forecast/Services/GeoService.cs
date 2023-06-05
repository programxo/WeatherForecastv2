using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Forecast.Models.GeoModel;

namespace Forecast.Services
{
    public class GeoService
    {
        public Coordinates GetCoordinates(string city)
        {
            string query = String.Format("https://nominatim.openstreetmap.org/search.php?q={0}&format=jsonv2", city);
            string result = GetRequest(query); //returns a stringified array of js objects

            var list = JsonConvert.DeserializeObject<List<Place>>(result);

            var place = list.FirstOrDefault();

            if (place == null)
            {
                return null;
            }

            var coordinates = GetCoordinatesFromPlace(place);

            return coordinates;

        }

        public string GetRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public Coordinates GetCoordinatesFromPlace(Place place)
        {
            var coord = new Coordinates();

            coord.Longitude = Convert.ToDouble(place.lon);
            coord.Latitude = Convert.ToDouble(place.lat);
            coord.Name = place.display_name;
            coord.Category = place.category;

            return coord;
        }
    }
}
