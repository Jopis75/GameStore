using Application.Events.Addresses;

namespace Application.Aggregates
{
    public class AddressAggregate : AggregateRoot
    {
        public bool Active { get; set; }

        public AddressAggregate()
        {
        }

        public AddressAggregate(string streetAddress, string postalCode, string city, string state, string country)
        {
            CreateAddress(streetAddress, postalCode, city, state, country);
        }

        public void Apply(AddressCreatedEvent addressCreatedEvent)
        {
            Id = addressCreatedEvent.Id;
            Active = true;
        }

        public void Apply(AddressDeletedEvent addressDeletedEvent)
        {
            Id = addressDeletedEvent.Id;
            Active = false;
        }

        public void Apply(AddressUpdatedEvent addressUpdatedEvent)
        {
            Id = addressUpdatedEvent.Id;
        }

        private void CreateAddress(string streetAddress, string postalCode, string city, string state, string country)
        {
            var addressCreatedEvent = new AddressCreatedEvent
            {
                StreetAddress = streetAddress,
                PostalCode = postalCode,
                City = city,
                State = state,
                Country = country
            };

            RaiseEvent(addressCreatedEvent);
        }

        public void DeleteAddress()
        {
            if (Active == false)
            {
                throw new InvalidOperationException($"Unable to delete the inactive post with Id {Id}.");
            }

            var addressDeletedEvent = new AddressDeletedEvent
            {
                Id = Id
            };

            RaiseEvent(addressDeletedEvent);
        }

        public void UpdateAddress(string streetAddress, string postalCode, string city, string state, string country)
        {
            if (Active == false)
            {
                throw new InvalidOperationException($"Unable to update the inactive post with Id {Id}.");
            }

            var addressUpdatedEvent = new AddressUpdatedEvent
            {
                Id = Id,
                StreetAddress = streetAddress,
                PostalCode = postalCode,
                City = city,
                State = state,
                Country = country
            };

            RaiseEvent(addressUpdatedEvent);
        }
    }
}
