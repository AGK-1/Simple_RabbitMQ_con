using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace Rabbit_mq_get_msg
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory() { HostName = "localhost" };
			using (var con = factory.CreateConnection())
			{
				using (var chanel = con.CreateModel())
				{
					chanel.QueueDeclare(queue: "first",
						exclusive: false,
						durable: true,
						autoDelete: false,
						arguments: null);

					var consumer = new EventingBasicConsumer(chanel);

					consumer.Received += (model, es) =>
					{
						var body = es.Body.ToArray();
						var message = Encoding.UTF8.GetString(body);
						Console.WriteLine($"Received message: {message}");
					};

					chanel.BasicConsume(queue: "first",
						autoAck: true,
						consumer: consumer);

					Console.WriteLine("Waiting for messages. Press [enter] to exit.");
					Console.ReadLine(); // Prevent program from exiting immediately
				}
			}
		}
	}
}
