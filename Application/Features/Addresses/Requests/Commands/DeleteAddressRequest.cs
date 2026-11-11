using GameStore.Application.Dtos.General;
using MediatR;

namespace GameStore.Application.Features.Addresses.Requests.Commands
{
    public class DeleteAddressRequest : IRequest<HttpResponseDto<DeleteAddressRequest>>
    {
        public int Id { get; set; }
    }
}
