using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Addresses
{
    public class UpdateAddressResponseDto : IUpdateResponseDto
    {
        public int Id { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
