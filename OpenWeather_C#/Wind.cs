using OLIO_OHJELMOINTI;
using System.Text.Json;
using System.Text.Json.Serialization;
public class Wind:Location
{
    [JsonPropertyName("speed")]
    public double WindSpeed
    {get;set;}
    [JsonPropertyName("deg")]
    public double WindDirection
    {get;set;}
    
}