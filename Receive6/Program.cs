using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

// See https://aka.ms/new-console-template for more information
var factory = new ConnectionFactory() { HostName = "localhost" };
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

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: "MyTestQueue",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
