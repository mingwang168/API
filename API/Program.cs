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
            string baseUrl = "https://swapi.co/api/";
            string planets = "planets/";
            string people = "people";
            try
            {
                Uri uri = new Uri(baseUrl+planets);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader responseStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                Console.WriteLine(JObject.Parse(responseStream.ReadToEnd()));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
