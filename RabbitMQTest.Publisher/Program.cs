using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace UdemyRabbitMQ.publisher
{
    public enum LogNames
    {
        Successful = 1,
        Error = 2,
        Warning = 3,
        Info = 4,
    }
    public enum SoybisType
    {
        Arac = 1,
        Tapu = 2,
        SGK = 3,
        EmekliSandiği = 4
    }

    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://hlhvpexd:8tyFLTYa1wv1adchaD25UOMlJkS7njzJ@shrimp.rmq.cloudamqp.com/hlhvpexd");

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.ExchangeDeclare("soybis-direct", durable: true, type: ExchangeType.Direct);
            var routeKey = $"route-soybis";
            var queueName = $"direct-queue-soybis";
            channel.QueueDeclare(queueName, true, false, false);
            channel.QueueBind(queueName, "soybis-direct", routeKey, null);
            Enumerable.Range(1, 1000).ToList().ForEach(x =>
            {
                int tc = 10000+x;
                string message = $"{tc}";
                var messageBody = Encoding.UTF8.GetBytes(message);
                var routeKey = $"route-soybis";
                channel.BasicPublish("soybis-direct", routeKey, null, messageBody);
                Console.WriteLine($"Tc {tc} gönderilmiştir : {message}");
            });

            Console.ReadLine();


        }
    }
}
