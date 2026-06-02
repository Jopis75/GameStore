using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events.Addresses
{
    public class UpdateAddressEvent() : EventBase(nameof(UpdateAddressEvent))
    {
        public string City { get; set; } = String.Empty;

        public string Country { get; set; } = String.Empty;

        public string PostalCode { get; set; } = String.Empty;

        public string State { get; set; } = String.Empty;

        public string StreetAddress { get; set; } = String.Empty;
    }
}
