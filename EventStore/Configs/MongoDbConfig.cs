namespace EventStore.Configs
{
    public class MongoDbConfig
    {
        public string ConnectionString { get; set; } = String.Empty;

        public string Database { get; set; } = String.Empty;

        public string Collection { get; set; } = String.Empty;
    }
}
