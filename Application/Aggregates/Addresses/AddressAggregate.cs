using Application.Events;
using Application.Events.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregates.Addresses
{
    public class AddressAggregate : AggregateRoot
    {
        public bool Active { get; set; }

        public AddressAggregate()
        {
        }

        public AddressAggregate(Guid id, string streetAddress, string postalCode, string city, string state, string country)
        {
            CreateAddress(id, streetAddress, postalCode, city, state, country);
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

        private void CreateAddress(Guid id, string streetAddress, string postalCode, string city, string state, string country)
        {
            RaiseEvent(new AddressCreatedEvent()
            {
                Id = id,
                StreetAddress = streetAddress,
                PostalCode = postalCode,
                City = city,
                State = state,
                Country = country
            });
        }

        public void DeleteAddress()
        {
            if (Active == false)
            {
                throw new InvalidOperationException($"Unable to delete the inactive post with Id {Id}.");
            }

            RaiseEvent(new AddressDeletedEvent()
            {
                Id = Id
            });
        }

        public void UpdateAddress(string streetAddress, string postalCode, string city, string state, string country)
        {
            if (Active == false)
            {
                throw new InvalidOperationException($"Unable to update the inactive post with Id {Id}.");
            }

            RaiseEvent(new AddressUpdatedEvent()
            {
                Id = Id,
                StreetAddress = streetAddress,
                PostalCode = postalCode,
                City = city,
                State = state,
                Country = country
            });
        }
    }
}
