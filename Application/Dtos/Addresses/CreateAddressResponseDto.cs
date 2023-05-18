using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Addresses
{
    public class CreateAddressResponseDto : ICreateResponseDto
    {
        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public int Id { get; set; }
    }
}
