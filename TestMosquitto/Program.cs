using System;
using System.Text;
using System.Threading;
using MQTTnet;
using MQTTnet.Client;
class Program
{
    static async Task Main(string[] args)
    {
        var factory = new MqttFactory();
        var client = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer("host.docker.internal") // replace with the address of your MQTT 
            .WithCredentials("snefUser", "Sn3fL@b!26") // replace with your credentials
            .WithClientId("Dev_Send_MQPub") // replace with a client ID of your choice
            .Build();

        await client.ConnectAsync(options, CancellationToken.None);

        var message = new MqttApplicationMessageBuilder()
            .WithTopic("internal-exchange/update-car") // replace with the topic you want to publish to
            .WithPayload("Hello world!") // replace with the message you want to publish
            .Build();

        while (true)
        {
            await client.PublishAsync(message, CancellationToken.None);
            Thread.Sleep(1000);
        }

        await client.DisconnectAsync();
    }
}