using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Services.Services
{
    public class RabbitMqService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public RabbitMqService(string host, int port, string username, string password)
        {
            var factory = new ConnectionFactory
            {
                HostName = host,
                Port = port,
                UserName = username,
                Password = password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        public void PublishMessage(string message, string queueName)
        {
            // Declare the queue if it doesn't exist
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            // Publish the message to the queue
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }
        //public  T WaitForResponse<T>(string queueName) where T : class
        //{
        //    // Declare the queue if it doesn't exist
        //    _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        //    // Create a consumer to receive messages from the queue
        //    var consumer = new EventingBasicConsumer(_channel);
        //    _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        //    // Wait for a message
        //    var response = consumer.Queue.Dequeue();

        //    // Deserialize and return the response
        //    var responseMessage = Encoding.UTF8.GetString(response.Body.ToArray());
        //    return JsonConvert.DeserializeObject<T>(responseMessage);
        //}
        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }

    }
}
