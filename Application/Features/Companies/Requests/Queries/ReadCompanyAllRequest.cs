using Application.Dtos.General;
using MediatR;

namespace Application.Features.Companies.Requests.Queries
{
    public class ReadCompanyAllRequest : IRequest<HttpResponseDto<ReadCompanyAllRequest>>
    {
    }
}
