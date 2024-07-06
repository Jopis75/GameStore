﻿using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Addresses.Requests.Commands
{
    public class UpdateAddressRequest : IRequest<HttpResponseDto<AddressDto>>
    {
        public AddressDto AddressDto { get; set; } = new();
    }
}
