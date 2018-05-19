using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using KafkaNet;
using KafkaNet.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KafkaConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             KAFKA-NET USAGE
             * string topic = "MYTOPIC";

            Uri uri = new Uri("http://localhost:9092");
            var options = new KafkaOptions(uri);

            var router = new BrokerRouter(options);

            var consumer = new Consumer(new ConsumerOptions(topic, router));
                 
            foreach (var message in consumer.Consume())
            {
                Console.WriteLine(Encoding.UTF8.GetString(message.Value));
            }
            Console.ReadLine();*/

            var config = new Dictionary<string, object>
            {
                    { "group.id", "sample-consumer" },
                    { "bootstrap.servers", "localhost:9092" },
                    { "enable.auto.commit", "false"}
            };

            using (var consumer = new Consumer<Null, string>(config, null, new StringDeserializer(Encoding.UTF8)))
            {
                consumer.Subscribe(new string[] { "MYTOPIC" });

                consumer.OnMessage += (_, msg) =>
                {
                    Console.WriteLine($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value}");
                    consumer.CommitAsync(msg);
                };

                while (true)
                {
                    consumer.Poll(-1);
                }
            }
        }
    }
}
