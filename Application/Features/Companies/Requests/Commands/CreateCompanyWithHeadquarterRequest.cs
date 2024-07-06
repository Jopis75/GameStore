using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Companies.Requests.Commands
{
    public class CreateCompanyWithHeadquarterRequest : IRequest<HttpResponseDto<CompanyDto>>
    {
        public CompanyDto CompanyDto { get; set; } = new();
    }
}
