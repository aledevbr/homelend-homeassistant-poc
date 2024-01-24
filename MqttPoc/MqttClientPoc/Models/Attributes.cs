using System.Text.Json.Serialization;

namespace MqttClientPoc.Models;

public class Attributes
{
    [JsonPropertyName("state_class")]
    public string? StateClass { get; set; }

    [JsonPropertyName("unit_of_measurement")]
    public string? UnitOfMeasurement { get; set; }

    [JsonPropertyName("device_class")]
    public string? DeviceClass { get; set; }

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [JsonPropertyName("friendly_name")]
    public string? FriendlyName { get; set; }
}
