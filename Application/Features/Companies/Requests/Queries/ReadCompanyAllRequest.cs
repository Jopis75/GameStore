using Application.Dtos.Common;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Companies.Requests.Queries
{
    public class ReadCompanyAllRequest : IRequest<HttpResponseDto<List<CompanyDto>>>
    {
    }
}
