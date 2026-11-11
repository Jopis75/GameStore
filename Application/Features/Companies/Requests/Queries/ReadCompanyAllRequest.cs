using GameStore.Application.Dtos.General;
using MediatR;

namespace GameStore.Application.Features.Companies.Requests.Queries
{
    public class ReadCompanyAllRequest : IRequest<HttpResponseDto<ReadCompanyAllRequest>>
    {
    }
}
