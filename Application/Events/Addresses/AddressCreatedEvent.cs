namespace Application.Events.Addresses
{
    public class AddressCreatedEvent() : EventBase(nameof(AddressCreatedEvent))
    {
        public string City { get; set; } = String.Empty;

        public string Country { get; set; } = String.Empty;

        public string PostalCode { get; set; } = String.Empty;

        public string State { get; set; } = String.Empty;

        public string StreetAddress { get; set; } = String.Empty;
    }
}
