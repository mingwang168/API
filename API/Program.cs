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
            const string PLANETS = "planets/";
            //string people = "people/";

            Console.WriteLine("\nSWAPI PLANETS");
            JObject planetResult = CallRestMethod(BASE_URL + PLANETS);
            JArray planets = (JArray)planetResult["results"];

            foreach(JObject planet in planets)
            {
                Console.WriteLine("******************************************************************");
                Console.WriteLine("The planet name : "+planet["name"]);
                JArray films = (JArray)planet["films"];
                Console.WriteLine("films are :");
                foreach(JValue film in films)
                {
                    if (CallRestMethod(film.ToString())["title"] != null)
                    {
                    Console.WriteLine(CallRestMethod(film.ToString())["title"]);
                    }
                    else
                    {
                        Console.WriteLine("Not in any films");
                    }
                    
                }
            }

            Console.ReadLine();

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

