namespace Application.Events.Addresses
{
    public class AddressUpdatedEvent() : EventBase(nameof(AddressUpdatedEvent))
    {
        public string City { get; set; } = default!;

        public string Country { get; set; } = default!;

        public string PostalCode { get; set; } = default!;

        public string State { get; set; } = default!;

        public string StreetAddress { get; set; } = default!;
    }
}
