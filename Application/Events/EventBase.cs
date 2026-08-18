using Application.Messages;

namespace Application.Events
{
    public abstract class EventBase(string type) : Message
    {
        public int Version { get; set; }

        public string Type { get; set; } = type;
    }
}
