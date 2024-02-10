using Application.Dtos.Common;
using Application.Dtos.Companies;
using MediatR;

namespace Application.Features.Companies.Requests.Commands
{
    public class CreateCompanyWithAddressRequest : IRequest<HttpResponseDto<CreateCompanyWithAddressResponseDto>>
    {
        public CreateCompanyWithAddressRequestDto? CreateCompanyWithAddressRequestDto { get; set; }
    }
}
