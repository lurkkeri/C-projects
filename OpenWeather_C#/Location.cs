namespace OLIO_OHJELMOINTI;
using System.Text.Json;
using System.Text.Json.Serialization;
 // The parent folder 
public class Location
{  
    [JsonPropertyName("name")]
    public string ?City{ get;set; }
    
    [JsonPropertyName("timezone")]
    public int Timezone { get; set; }

    [JsonPropertyName("sys")]
    public Sys? Sys { get; set; }

    [JsonPropertyName("coord")]
    public Coord? Coordinates { get; set; }
    
    [JsonPropertyName("weather")]
    public List<WeatherContent> ?WeatherInfo { get; set; }
    [JsonPropertyName("wind")]
    public Wind ?Wind { get; set; }
    // There are two folders named "main"
    public WeatherMain ?main {get;set;}

    // Timestamp conversion
    public string GetSunset()
    {
        // Sunset as unix timestamp
        long unixTimestamp = Sys?.Sunset ?? 0;
        // Convert sunset into UTC
        DateTime utcDateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).UtcDateTime;
        // Fetch timezone
        int timeZoneOffsetSeconds = Timezone;
        // Check the time difference 
        TimeSpan offset = TimeSpan.FromSeconds(timeZoneOffsetSeconds);
        // Add the time difference into the utc time
        DateTime localDateTime = utcDateTime + offset;

        return localDateTime.ToString("dd.MM.yyyy HH:mm:ss");
    }
    public string GetSunrise()
    {  
        long unixTimestamp = Sys?.Sunrise ?? 0;

        DateTime utcDateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).UtcDateTime;
        int timeZoneOffsetSeconds = Timezone;
        TimeSpan offset = TimeSpan.FromSeconds(timeZoneOffsetSeconds);
        DateTime localDateTime = utcDateTime + offset;

        return localDateTime.ToString("dd.MM.yyyy HH:mm:ss");
    }

}


