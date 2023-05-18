using Application.Dtos.Common;
using Application.Dtos.Companies;
using MediatR;

namespace Application.Features.Companies.Requests.Commands
{
    public class CreateCompanyRequest : IRequest<HttpResponseDto<CreateCompanyResponseDto>>
    {
        public CreateCompanyRequestDto? CreateCompanyRequestDto { get; set; }
    }
}
