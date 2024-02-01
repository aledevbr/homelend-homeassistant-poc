using MQTTnet;
using MQTTnet.Client;

using System.Collections.Concurrent;

namespace MqttClientPoc;

public class MqttService
{
    private readonly ILogger<MqttService> _logger;
    private readonly IMqttClient _mqttClient;
    private readonly ConcurrentDictionary<string, Action<MqttApplicationMessage>> _callbacks = new ConcurrentDictionary<string, Action<MqttApplicationMessage>>();


    public MqttService(ILogger<MqttService> logger)
    {
        _logger = logger;

        var factory = new MqttFactory();
        _mqttClient = factory.CreateMqttClient();

        ConnectAsync().GetAwaiter().GetResult();

        _mqttClient.ApplicationMessageReceivedAsync += HandleReceivedApplicationMessage;
    }

    private Task HandleReceivedApplicationMessage(MqttApplicationMessageReceivedEventArgs e)
    {
        if (_callbacks.TryGetValue(e.ApplicationMessage.Topic, out var callback))
        {
            callback?.Invoke(e.ApplicationMessage);
        }
        return Task.CompletedTask;
    }

    public async Task SubscribeAsync(string topic, Action<MqttApplicationMessage> callback, CancellationToken cancellationToken = default)
    {
        await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build(), cancellationToken);
        _callbacks[topic] = callback;
    }

    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        var options = new MqttClientOptionsBuilder()
            .WithClientId("MqttClientPoc")
            .WithTcpServer("homeassistant.local", 1883)
            .WithCredentials("testuser", "TestUser$enh@123")
            .Build();

        await _mqttClient.ConnectAsync(options, cancellationToken);
    }

    public async Task PublishAsync(string topic, string payload, CancellationToken cancellationToken = default)
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .WithRetainFlag()
            .Build();

        await _mqttClient.PublishAsync(message, cancellationToken);

        //await _mqttClient.DisconnectAsync(cancellationToken: cancellationToken);
    }

}
