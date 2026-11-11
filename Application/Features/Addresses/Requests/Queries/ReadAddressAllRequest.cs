using GameStore.Application.Dtos.General;
using MediatR;

namespace GameStore.Application.Features.Addresses.Requests.Queries
{
    public class ReadAddressAllRequest : IRequest<HttpResponseDto<ReadAddressAllRequest>>
    {
    }
}
