﻿using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Addresses.Requests.Commands
{
    public class DeleteAddressRequest : IRequest<HttpResponseDto<AddressDto>>
    {
        public int Id { get; set; }
    }
}
