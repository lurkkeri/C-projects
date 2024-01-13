namespace OLIO_OHJELMOINTI;
using System.Text.Json;
using System.Text.Json.Serialization;
public class Sys:Location
{
    [JsonPropertyName("country")]
    public string? Country { get; set; }
    [JsonPropertyName("sunrise")]
    public int Sunrise {get;set;}
    [JsonPropertyName("sunset")]
    public int Sunset {get;set;}

}