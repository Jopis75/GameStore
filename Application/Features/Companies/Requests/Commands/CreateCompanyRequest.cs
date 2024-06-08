using Application.Dtos.Common;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Companies.Requests.Commands
{
    public class CreateCompanyRequest : IRequest<HttpResponseDto<CompanyDto>>
    {
        public CompanyDto? CompanyDto { get; set; }
    }
}
