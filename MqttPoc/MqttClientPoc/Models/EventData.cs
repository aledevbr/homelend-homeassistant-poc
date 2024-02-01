using System.Text.Json.Serialization;

namespace MqttClientPoc.Models;

public class EventData
{
    [JsonPropertyName("entity_id")]
    public string? EntityId { get; set; }

    [JsonPropertyName("old_state")]
    public StateInfo? OldState { get; set; }

    [JsonPropertyName("new_state")]
    public StateInfo? NewState { get; set; }
}
