using Application.Dtos.Common;

namespace Application.Dtos.Companies
{
    public class CreateCompanyWithAddressResponseDto : CreateResponseDto
    {
        public int AddressId { get; set; }
    }
}
