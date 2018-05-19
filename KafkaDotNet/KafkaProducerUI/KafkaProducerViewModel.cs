using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace KafkaProducerUI
{
    public class KafkaProducerViewModel : INotifyPropertyChanged
    {
        #region Private Fields

        private string _topic;
        private string _message;

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public KafkaProducerViewModel()
        {
            Topic = string.Empty;
            Message = string.Empty;
            SendTopic = new RelayCommand(SendTopicMessage, CanSendTopic);
        }


        #region Public properties
        public string Topic
        {
            get => _topic;
            set
            {
                if (_topic != value)
                {
                    _topic = value;
                    OnPropertyChanged("Topic");
                }
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        public ICommand SendTopic
        {
            get;
        }

        #endregion

        #region Private Methods

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        private bool CanSendTopic(object obj)
        {
            return Topic.Length > 0 && Message.Length > 0;
        }

        private void SendTopicMessage(object obj)
        {
            //KAFKA-NET USAGE
            //Message message = new Message(Message);

            //Uri uri = new Uri("http://localhost:9092");
            //var options = new KafkaOptions(uri);

            //var router = new BrokerRouter(options);
            
            //var client = new Producer(router);

            //client.SendMessageAsync(Topic, new List<Message> { message });
            var config = new Dictionary<string, object>
            {
                    { "bootstrap.servers", "localhost:9092" }
            };

            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {                
                producer.ProduceAsync(Topic, null, Message);
                producer.Flush(100);               
            }
            
        }      

        #endregion
    }
}
