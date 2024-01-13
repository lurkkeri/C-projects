using OLIO_OHJELMOINTI;
using System.Text.Json;
using System.Text.Json.Serialization;

public class WeatherContent:Location
{   // The other main
    public string ?main
    {
        get;set;
    }
 
    [JsonPropertyName("description")]
    public string ?Describe
    {
        get;set;
    }
}