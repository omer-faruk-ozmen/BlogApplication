using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BlogApplication.Common.Infrastructure
{
    public static class QueueFactory
    {
        public static void SendMessageToExchange(string exchangeName,
                                        string exchangeType,
                                        string queueName,
                                        object obj)
        {
            var channel = CreateBasicConsumer()
                .EnsureExchange(exchangeName, exchangeType)
                .EnsureQueue(queueName, exchangeName)
                .Model;

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));

            channel.BasicPublish(exchange: exchangeName, routingKey: queueName, basicProperties: null, body: body);
        }


        public static EventingBasicConsumer CreateBasicConsumer()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = BlogConstants.RabbitMQHost
            };

            var connection = connectionFactory.CreateConnection();

            var channel = connection.CreateModel();

            return new EventingBasicConsumer(channel);
        }

        public static EventingBasicConsumer EnsureExchange(
            this EventingBasicConsumer consumer,
            string exchangeName,
            string exchangeType = BlogConstants.DefaultExchangeType)
        {
            consumer.Model.ExchangeDeclare(exchange: exchangeName, type: exchangeType, durable: false, autoDelete: false);

            return consumer;
        }

        public static EventingBasicConsumer EnsureQueue(
            this EventingBasicConsumer consumer,
            string queueName,
            string exchangeName)
        {
            consumer.Model.QueueDeclare(queue: queueName, durable: false, autoDelete: false, exclusive: false);

            consumer.Model.QueueBind(queue: queueName, exchange: exchangeName, routingKey: queueName);

            return consumer;
        }

        public static EventingBasicConsumer Receive<T>(this EventingBasicConsumer consumer, Action<T> action)
        {
            consumer.Received += (m, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var model = JsonSerializer.Deserialize<T>(message);

                action(model);

                consumer.Model.BasicAck(eventArgs.DeliveryTag, false);
            };

            return consumer;
        }

        public static EventingBasicConsumer StartConsuming(this EventingBasicConsumer consumer, string queueName)
        {
            consumer.Model.BasicConsume(queue: queueName,
                autoAck: false,
                consumer: consumer);

            return consumer;
        }
    }
}
