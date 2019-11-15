using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace API
{
    class Program
    {

        static void Main(string[] args)
        {
            const string BASE_URL = "https://swapi.co/api/";
            string PLANETS = "planets/";
            //string people = "people/";
             Console.WriteLine("\nSWAPI PLANETS");
            string uri = BASE_URL + PLANETS;
            while (true)
            {
            JObject planetResult = CallRestMethod(uri);
            JArray planets = (JArray)planetResult["results"];
                if (planets == null) { break; }
                foreach (JObject planet in planets)
                {
                    Console.WriteLine("******************************************************************");
                    Console.WriteLine("The planet name : " + planet["name"]);
                    JArray films = (JArray)planet["films"];
                    if (films.Count != 0) {
                        Console.WriteLine("films are :");
                        foreach (JValue film in films)
                        {
                            Console.WriteLine(CallRestMethod(film.ToString())["title"]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not in any films");
                    }
                }
                if (!string.IsNullOrEmpty(planetResult.GetValue("next").ToString()))
                {
                uri = (string)planetResult["next"];
                }
                else { break; }
            }
        }
        static JObject CallRestMethod(string uri)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader responseStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                return JObject.Parse(responseStream.ReadToEnd());
                response.Close();
                responseStream.Close();
            }
            catch (Exception e)
            {
                return JObject.Parse($"{{'Error':'{e.Message}'}}");
            }
        }

    }
}

