namespace OLIO_OHJELMOINTI;
using System.Text.Json;
using System.Text.Json.Serialization;
 // The root folder
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
    // There are two folders named 'main' in json dataset
    public WeatherMain ?main {get;set;}

    // Convert the unix timestamp into UTC for sunset
    public string GetSunset()
    {
        // Sunset as unix timestamp
        long unixTimestamp = Sys?.Sunset ?? 0;
        // Convert to UTC
        DateTime utcDateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).UtcDateTime;
        // Fetch timezone
        int timeZoneOffsetSeconds = Timezone;
        // Calculate timedifference
        TimeSpan offset = TimeSpan.FromSeconds(timeZoneOffsetSeconds);
        DateTime localDateTime = utcDateTime + offset;
       
        return localDateTime.ToString("dd.MM.yyyy HH:mm:ss");
    }

    // Convert the unix timestamp into UTC for sunrise, essentially the same as above, but i think it made sense in this case
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


