using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Companies
{
    public class UpdateCompanyResponseDto : IUpdateResponseDto
    {
        public int Id { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
