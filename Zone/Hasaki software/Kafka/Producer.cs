using Confluent.Kafka;
using Intech_software.Interface;
using Intech_software.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;

namespace Intech_software.Kafka
{
    internal class Producer
    {
        private readonly string _bootstrapServers;
        public Producer()
        {
            _bootstrapServers = System.Configuration.ConfigurationSettings.AppSettings["KAFKA_ADDRS"];
        }

        public void PushInboundMessage(Inbound data)
        {

            try
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = _bootstrapServers,
                };

                const string topic = "wms.outbox.conveyor_scanned_orders";
                var jsonData = JsonConvert.SerializeObject(data, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    },
                    Formatting = Formatting.Indented,
                });
                using (var producer = new ProducerBuilder<string, string>(config).SetErrorHandler((_, error) =>
                {
                    Console.WriteLine($"Error pushing message: {error}");
                    WriteError($"Error pushing message: {error}", jsonData);
                }
                    ).Build())
                {
                    producer.Produce(topic, new Message<string, string>
                    {
                        Key = "payload",
                        Value = jsonData,
                    }, (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                            WriteError($"Failed to deliver message: {deliveryReport.Error.Reason}", jsonData);
                        }
                        else
                        {
                            Console.WriteLine($"Produced event to topic {topic}: {jsonData}");
                        }
                    });
                    producer.Flush(TimeSpan.FromSeconds(5));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error pushing message");
                WriteError($"Error pushing message: {e.Message}", data.PackageCode);
            }
        }

        void WriteError(string error, string msg)
        {
            try
            {
                string projectDir = Environment.CurrentDirectory;

                string errorFileDir = $"{Directory.GetParent(projectDir).Parent.Parent.FullName}\\Hasaki software\\failedToPushMessage.txt";
                if (!File.Exists(errorFileDir))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(errorFileDir))
                    {
                        sw.WriteLine(error);
                        sw.WriteLine(msg);
                    }
                }
                using (StreamWriter sw = File.AppendText(errorFileDir))
                {
                    sw.WriteLine(error);
                    sw.WriteLine(msg);
                }

            }
            catch { }
        }

        public void PushMessage(IKafkaMessage obj, string topic, string key)
        {
            string message = obj.ToString();

            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers,
            };

            try
            {
                using (var producer = new ProducerBuilder<string, string>(config).SetErrorHandler((_, error) =>
                {
                    Console.WriteLine($"Error pushing message: {error}");
                    WriteError($"Error pushing message: {error}", message);
                }
                    ).Build())
                {
                    producer.Produce(topic, new Message<string, string>
                    {
                        Key = key == "" ? "payload" : key,
                        Value = message,
                    }, (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                            WriteError($"Failed to deliver message: {deliveryReport.Error.Reason}", message);
                        }
                        else
                        {
                            Console.WriteLine($"Produced event to topic {topic}: {message}");
                        }
                    });
                    producer.Flush(TimeSpan.FromSeconds(5));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error pushing message");
                WriteError($"Error pushing message: {e.Message}", message);
            }
        }

        public void PushMessage(string message, string topic, string key)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers,
            };

            try
            {
                using (var producer = new ProducerBuilder<string, string>(config).SetErrorHandler((_, error) =>
                {
                    Console.WriteLine($"Error pushing message: {error}");
                    WriteError($"Error pushing message: {error}", message);
                }
                    ).Build())
                {
                    producer.Produce(topic, new Message<string, string>
                    {
                        Key = key == "" ? "payload" : key,
                        Value = message,
                    }, (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                            WriteError($"Failed to deliver message: {deliveryReport.Error.Reason}", message);
                        }
                        else
                        {
                            Console.WriteLine($"Produced event to topic {topic}: {message}");
                        }
                    });
                    producer.Flush(TimeSpan.FromSeconds(5));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error pushing message");
                WriteError($"Error pushing message: {e.Message}", message);
            }
        }
    }
}
