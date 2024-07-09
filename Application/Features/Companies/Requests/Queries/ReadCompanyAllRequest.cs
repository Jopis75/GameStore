using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Companies.Requests.Queries
{
    public class ReadCompanyAllRequest : IRequest<HttpResponseDto<CompanyDto>>
    {
    }
}
