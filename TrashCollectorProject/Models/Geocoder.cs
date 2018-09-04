using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.Net;

namespace TrashCollectorProject.Models
{
    public class Location
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }
    }

    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }

  public class Geometry
    {
        public Bounds bounds { get; set; }
        public Location location { get; set; }
        public string location_type { get; set; }
        public Bounds viewport { get; set; }
    } 
    
    public class Bounds
    {
        public Location northeast { get; set; }
        public Location southwest { get; set; }

    }
    

  
    

    public class Result 
    {
        public AddressComponent[] address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }

       
    }

    public class GeoResponse
    {
        public string status { get; set; }
        public Result[] results { get; set; }
    }

    
    






    public class Geocoder
    {
        private static string GetApiKey()
        {
            return File.ReadAllText("C:\\Users\\niels\\Documents\\Projects\\TrashCollectorProject\\TrashCollectorProject\\ApiKeys.txt");
        }
        public static void RunGeocoder(string address, string city, string state)
        {
            address = address.Trim().Replace(" ", "+");
            city = city.Trim().Replace(" ", "+");
            state = state.Trim();
            string apiKey = GetApiKey();
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address},+{city},+{state}&key={apiKey}";
            WebResponse response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                response = request.GetResponse();
                if (response != null)
                {
                    string responseString = null;
                    Stream stream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(stream);
                    responseString = streamReader.ReadToEnd();
                    GeoResponse geoResponse = JsonConvert.DeserializeObject<GeoResponse>(responseString);
                    if (geoResponse.status == "OK")
                    {
                        decimal custLat = geoResponse.results[0].geometry.location.lat;
                        decimal custLong = geoResponse.results[0].geometry.location.lng;
                    }
                    
                }
            }
            catch
            {
                throw new Exception("Google maps was unable to find address");
            }
        }
    }
}