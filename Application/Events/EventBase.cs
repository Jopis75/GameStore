namespace Application.Events
{
    public abstract class EventBase(string type)
    {
        public int Id { get; set; }

        public int Version { get; set; }

        public string Type { get; set; } = type;
    }
}
