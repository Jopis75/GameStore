using Application.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events
{
    public abstract class EventBase(string type) : Message
    {
        public int Version { get; set; }

        public string Type { get; set; } = type;
    }
}
