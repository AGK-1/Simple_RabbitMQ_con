using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Hosting;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Rabbit_MQ_simple
{

	internal class Program
	{
		//public static IModel IModel { get; private set; }

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

					var message = "message-75211-52-cv-DE-777-REM";
					var body = Encoding.UTF8.GetBytes(message);

					chanel.BasicPublish(exchange: "",
						routingKey: "first",
						basicProperties: null,
						body: body);

					Console.WriteLine("DONE");
				}

			}
			Console.ReadLine();
		}

	}
}
