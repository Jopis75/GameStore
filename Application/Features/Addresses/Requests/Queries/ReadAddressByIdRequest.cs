using GameStore.Application.Dtos.General;
using MediatR;

namespace GameStore.Application.Features.Addresses.Requests.Queries
{
    public class ReadAddressByIdRequest : IRequest<HttpResponseDto<ReadAddressByIdRequest>>
    {
        public int Id { get; set; }
    }
}
