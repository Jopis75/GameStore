using Application.Dtos.General;
using MediatR;

namespace Application.Features.Addresses.Requests.Queries
{
    public class ReadAddressAllRequest : IRequest<HttpResponseDto<ReadAddressAllRequest>>
    {
    }
}
