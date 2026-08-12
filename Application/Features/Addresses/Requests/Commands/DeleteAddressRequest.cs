using Application.Dtos.General;
using MediatR;

namespace Application.Features.Addresses.Requests.Commands
{
    public class DeleteAddressRequest : IRequest<HttpResponseDto<DeleteAddressRequest>>
    {
        public int Id { get; set; }
    }
}
