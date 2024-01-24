using Microsoft.AspNetCore.Mvc;

using MqttClientPoc.Models;

using System.Text;
using System.Text.Json;

namespace MqttClientPoc.Controllers;

public class MqttController : ControllerBase
{
    private readonly MqttService _mqttService;
    private readonly ILogger<MqttService> _logger;

    public MqttController(MqttService mqttService, ILogger<MqttService> logger)
    {
        _mqttService = mqttService;
        _logger = logger;
    }

    [HttpPost("/mqtt/connect")]
    public async Task<IActionResult> ConnectAsync(CancellationToken cancellationToken = default)
    {
        await _mqttService.ConnectAsync(cancellationToken);
        return Ok();
    }

    [HttpPost("/mqtt/publish")]
    public async Task<IActionResult> PublishAsync(string topic, string payload, CancellationToken cancellationToken = default)
    {
        await _mqttService.PublishAsync(topic, payload, cancellationToken);
        return Ok();
    }

    [HttpPost("/mqtt/publish/test")]
    public async Task<IActionResult> PublishTestAsync(string message, CancellationToken cancellationToken = default)
    {
        //await _mqttService.ConnectAsync(cancellationToken);
        await _mqttService.PublishAsync("test/topic", message, cancellationToken);
        return Ok();
    }

    [HttpGet("/mqtt/subscribe")]
    public async Task<IActionResult> SubscribeAsync(string topic = "homeassistant/events", CancellationToken cancellationToken = default)
    {
        await _mqttService.SubscribeAsync(topic, message =>
        {
            var dataJson = Encoding.UTF8.GetString(message.PayloadSegment);

            MqttEvent data = JsonSerializer.Deserialize<MqttEvent>(dataJson)!;

            _logger.LogInformation($"{data.EventType} - {data.EventData.EntityId} ({data.EventData.NewState?.Attributes?.FriendlyName}) - status:{data.EventData.NewState?.State}");

            //_logger.LogInformation($"Received message on topic {message.Topic}: {dataJson}");
        }, cancellationToken);
        return Ok();
    }

    [HttpGet("/mqtt/stream")]
    public async Task StreamMessages([FromQuery] string topic)
    {
        Response.ContentType = "text/event-stream";

        var streamClosed = new TaskCompletionSource<bool>();
        HttpContext.RequestAborted.Register(() => streamClosed.SetResult(true));

        await _mqttService.SubscribeAsync(topic, message =>
        {
            var data = Encoding.UTF8.GetString(message.PayloadSegment);
            var sseData = $"data: {data}\n\n";
            Response.Body.WriteAsync(Encoding.UTF8.GetBytes(sseData), HttpContext.RequestAborted);
        });

        await streamClosed.Task;
    }

    [HttpPost("/mqtt/toggle-light")]
    public async Task ToggleLightAsync(string entityId = "light.interruptor_duplo_luz_2", bool turnOn = false)
    {
        //string topic = $"homeassistant/lampada/{entityId}";
        //string payload = JsonSerializer.Serialize(new { state = turnOn ? "ON" : "OFF" });
        string topic = $"homeassistant/lampada";
        string payload = turnOn ? "on" : "off";

        await _mqttService.PublishAsync(topic, payload);
    }


}
