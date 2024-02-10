using Application.Interfaces.Persistance;
using Domain.Entities;
using Moq;

namespace UnitTests.Features.Addresses.Mocks
{
    public static class AddressRepositoryMock
    {
        public static Mock<IAddressRepository> Create()
        {
            var addresses = new List<Address>
            {
                new Address
                {
                    Id = 1,
                    StreetAddress = "StreetAddress1",
                    PostalCode = "12345",
                    City = "City1",
                    State = "State1",
                    Country = "Country1",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedAt = null,
                    UpdatedBy = null,
                    DeletedAt = null,
                    DeletedBy = null
                },
                new Address
                {
                    Id = 2,
                    StreetAddress = "StreetAddress2",
                    PostalCode = "12345",
                    City = "City2",
                    State = "State2",
                    Country = "Country2",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedAt = null,
                    UpdatedBy = null,
                    DeletedAt = null,
                    DeletedBy = null
                },
                new Address
                {
                    Id = 3,
                    StreetAddress = "StreetAddress3",
                    PostalCode = "12345",
                    City = "City3",
                    State = "State3",
                    Country = "Country3",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedAt = null,
                    UpdatedBy = null,
                    DeletedAt = null,
                    DeletedBy = null
                }
            };

            var addressRepositoryMock = new Mock<IAddressRepository>();

            addressRepositoryMock
                .Setup(addressRepository => addressRepository.CreateAsync(It.IsAny<Address>()))
                .ReturnsAsync((Address address) =>
                {
                    addresses.Add(address);
                    return address;
                });

            addressRepositoryMock
                .Setup(addressRepository => addressRepository.ReadAllAsync(false))
                .ReturnsAsync(addresses);

            addressRepositoryMock
                .Setup(addressRepository => addressRepository.ReadByIdAsync(It.IsAny<int>(), false))
                .ReturnsAsync((int id) => addresses.Where(address => address.Id == id).Single());

            //addressRepositoryMock
            //    .Setup(addressRepository => addressRepository.UpdateAsync(It.IsAny<Address>()))
            //    .ReturnsAsync((Address address) =>
            ////    {

            ////    });

            return addressRepositoryMock;
        }
    }
}
