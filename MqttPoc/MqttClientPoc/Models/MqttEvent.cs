using System.Text.Json.Serialization;

namespace MqttClientPoc.Models;

public class MqttEvent
{
    [JsonPropertyName("event_type")]
    public string EventType { get; set; } = default!;

    [JsonPropertyName("event_data")]
    public EventData EventData { get; set; } = default!;
}
