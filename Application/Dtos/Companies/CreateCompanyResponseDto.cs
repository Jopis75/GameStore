using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Companies
{
    public class CreateCompanyResponseDto : ICreateResponseDto
    {
        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public int Id { get; set; }
    }
}
