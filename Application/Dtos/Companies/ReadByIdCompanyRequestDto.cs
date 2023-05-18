using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Companies
{
    public class ReadByIdCompanyRequestDto : IReadByIdRequestDto
    {
        public int Id { get; set; }
    }
}
