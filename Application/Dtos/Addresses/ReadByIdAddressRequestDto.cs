using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Addresses
{
    public class ReadByIdAddressRequestDto : IReadByIdRequestDto
    {
        public int Id { get; set; }
    }
}
