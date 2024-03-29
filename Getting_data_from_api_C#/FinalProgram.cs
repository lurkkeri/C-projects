namespace OLIO_OHJELMOINTI;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    static async Task Main()
    {   
        bool usingHttp =true;
        // The program fetches weather data of a city chosen by the user from OpenWeather api
        while(usingHttp)
        {
            Console.WriteLine("Anna kaupungin nimi, jolla haluat hakea säätietoja. Paina q lopettaaksesi."); 
            string ?cityName = Console.ReadLine();
            if(cityName!= null && cityName!=string.Empty)
            {   
                // "q" is quit program
                if(cityName.ToLower().Equals("q"))
                {
                    Console.WriteLine("Lopetetaan ohjelma!");
                    usingHttp = false;
                }
                else
                {
                    await FetchData(cityName);
                }
            }
            else
            {
                Console.WriteLine("Syötit väärän!");
            }
        }
    }

    private static async Task FetchData(string cityName)
    {
       
        try
        {   
            // using disposes the handler and the client automatically afterwards
            using(HttpClientHandler handler = new HttpClientHandler())
            {
                using(HttpClient client = new HttpClient(handler))
                {
                    // Fetch json data from the api
                    var stringTask = client.GetStreamAsync($"http://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={Key.GetKey()}&units=metric");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    Stream responseStream = await stringTask;
                    await ProcessData(responseStream);
                }
            }
            
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nJotain meni pieleen!");
            Console.WriteLine("Error :{0} ", e.Message);
        }
        
    }
    // Method to process fetched data
    private static async Task ProcessData(Stream responseStream)
    {
        try
        {
            // Extract data into structure
            var location = await JsonSerializer.DeserializeAsync<Location>(responseStream);
            // Print data
            Console.WriteLine($"Kaupunki: {location?.City ?? "Unknown"}");
            Console.WriteLine($"Maa: {location?.Sys?.Country ?? "Unknown"}");
            Console.WriteLine($"Lokaatio: {location?.Coordinates?.Lon ?? 0} pituusastetta ja {location?.Coordinates?.Lat ?? 0} leveysastetta");
            Console.WriteLine($"Aurinko nousee: {location?.GetSunrise() ?? null}");
            Console.WriteLine($"Aurinko laskee: {location?.GetSunset() ?? null}");
            foreach (var weatherContent in location?.WeatherInfo)
            {
                Console.WriteLine($"Millaista säätä: {weatherContent.main}");
                Console.WriteLine($"Kuvaus säästä: {weatherContent.Describe}");
            }

            Console.WriteLine($"Lämpötila: {location?.main?.Temperature ?? 0} C");
            Console.WriteLine($"Lämpötila tuntuu kuin: {location?.main?.FeelsLike ?? 0} C");
            Console.WriteLine($"Vuorokauden pienin lämpötila: {location?.main?.TemperatureMin ?? 0} C");
            Console.WriteLine($"Vuorokauden suurin lämpötila: {location?.main?.TemperatureMax ?? 0} C");
            Console.WriteLine($"Ilmankosteus: {location?.main?.Humidity ?? 0} %");
            Console.WriteLine($"Ilmanpaine: {location?.main?.Pressure ?? 0} Pa");
            Console.WriteLine($"Tuulen nopeus: {location?.Wind?.WindSpeed ?? 0} m/s");
            Console.WriteLine($"Tuulen suunta: {location?.Wind?.WindDirection ?? 0} astetta kellon suuntaan.");
            Console.WriteLine();   
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JsonException: {ex.Message}");
        }
   
    }
}