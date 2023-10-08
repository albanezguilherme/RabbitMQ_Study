using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);


Console.WriteLine("Type x to exit or type the message to RabbitMQ");
var stopString = Console.ReadLine();

while (stopString != "x")
{
    var body = Encoding.UTF8.GetBytes(stopString);

    channel.BasicPublish(exchange: string.Empty,
                     routingKey: "hello",
                     basicProperties: null,
                     body:  body);
    Console.WriteLine($" [x] Sent {stopString}");

    stopString = Console.ReadLine();
}



Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
