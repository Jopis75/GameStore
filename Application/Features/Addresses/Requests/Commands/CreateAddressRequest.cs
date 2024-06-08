﻿using Application.Dtos.Common;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Addresses.Requests.Commands
{
    public class CreateAddressRequest : IRequest<HttpResponseDto<AddressDto>>
    {
        public AddressDto? AddressDto { get; set; }
    }
}
