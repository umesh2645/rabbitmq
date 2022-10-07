using System;
using RabbitMQ.Client;
using System.Text;
using System.Threading;

// See https://aka.ms/new-console-template for more information
var factory = new ConnectionFactory() { HostName = "localhost"};
factory.UserName = "user";
factory.Password ="nrs-rocks";
        using(var connection = factory.CreateConnection())
    using(var channel = connection.CreateModel())
    {
        channel.QueueDeclare(queue: "MyTestQueue",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

        
        for (int i = 1; i <= 100; i++)
        {
            Thread.Sleep(2000);
            string message = $"Message no {i} at  {DateTime.Now}";
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "",
                                routingKey: "MyTestQueue",
                                basicProperties: null,
                                body: body);
        Console.WriteLine(" [x] Sent {0}", message);
        }
        
    }

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
