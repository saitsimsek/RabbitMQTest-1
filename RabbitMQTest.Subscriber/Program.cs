using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace UdemyRabbitMQ.subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://hlhvpexd:8tyFLTYa1wv1adchaD25UOMlJkS7njzJ@shrimp.rmq.cloudamqp.com/hlhvpexd ");

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();


            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            Console.WriteLine("Logları dinleniyor...");
            var queueName = $"direct-queue-soybis";
            channel.BasicConsume(queueName, false, consumer); /*Kuyruklar oluşturuluyor.*/

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var tc = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine(Arac(tc));
                Console.WriteLine(Tapu(tc));
                Console.WriteLine(SGK(tc));
                Console.WriteLine(EmekliSandiği(tc));
                channel.BasicAck(e.DeliveryTag, false);
            };
            Console.ReadLine();
        }

        public static string Arac(string tc)
        {
            Thread.Sleep(1000);
            return "Tc Kimlik No Araç soybis yapıldı:" + tc; 
        }
        public static string Tapu(string tc)
        {
            Thread.Sleep(1000);
            return "Tapu soybis yapıldı:" + tc;
        }
        public static string SGK(string tc)
        {
            Thread.Sleep(1000);
            return "SGK soybis yapıldı:" + tc;
        }
        public static string EmekliSandiği(string tc)
        {
            Thread.Sleep(1000);
            return "Emekli Sandığı soybis yapıldı:" + tc;
        }


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

    }
}
