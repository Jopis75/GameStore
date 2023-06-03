using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Addresses
{
    public class DeleteAddressRequestDto : IDeleteRequestDto
    {
        public int Id { get; set; }

        public DeleteAddressRequestDto(int id)
        {
            Id = id;
        }
    }
}
