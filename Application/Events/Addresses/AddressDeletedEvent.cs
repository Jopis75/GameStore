using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events.Addresses
{
    public class AddressDeletedEvent() : EventBase(nameof(AddressDeletedEvent))
    {
    }
}
