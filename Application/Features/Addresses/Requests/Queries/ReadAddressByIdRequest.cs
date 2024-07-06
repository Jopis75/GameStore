using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Addresses.Requests.Queries
{
    public class ReadAddressByIdRequest : IRequest<HttpResponseDto<AddressDto>>
    {
        public int Id { get; set; }
    }
}
