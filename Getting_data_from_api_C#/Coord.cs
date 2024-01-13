using OLIO_OHJELMOINTI;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Coord:Location
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }
    [JsonPropertyName("lon")]
    public double Lon { get; set; }
}