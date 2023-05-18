using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Companies
{
    public class DeleteCompanyResponseDto : IDeleteResponseDto
    {
        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public int Id { get; set; }
    }
}
