using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Addresses
{
    public class DeleteAddressResponseDto : IDeleteResponseDto
    {
        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public int Id { get; set; }
    }
}
