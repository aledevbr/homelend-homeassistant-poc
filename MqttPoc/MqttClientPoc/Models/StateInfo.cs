using System.Text.Json.Serialization;

namespace MqttClientPoc.Models;

public class StateInfo
{
    [JsonPropertyName("entity_id")]
    public string? EntityId { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("attributes")]
    public Attributes? Attributes { get; set; }

    [JsonPropertyName("last_changed")]
    public DateTime? LastChanged { get; set; }

    [JsonPropertyName("last_updated")]
    public DateTime? LastUpdated { get; set; }

    [JsonPropertyName("context")]
    public Context? Context { get; set; }
}
