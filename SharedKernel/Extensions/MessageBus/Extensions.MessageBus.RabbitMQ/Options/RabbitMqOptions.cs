namespace Extensions.MessageBus.RabbitMQ.Options
{
    public class RabbitMqOptions
    {
        public string Url { get; set; }
        public bool PersistMessage { get; set; }
        public string ExchangeName { get; set; }
        public string ServiceName { get; set; }
    }
}
