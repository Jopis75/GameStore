﻿using Application.Dtos.General;
using Application.Features.Addresses.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Queries
{
    public class ReadAddressAllRequestHandler : IRequestHandler<ReadAddressAllRequest, HttpResponseDto<AddressDto>>
    {
        private readonly IAddressRepository _addressRepository;

        private readonly ILogger<ReadAddressAllRequestHandler> _logger;

        public ReadAddressAllRequestHandler(IAddressRepository addressRepository, ILogger<ReadAddressAllRequestHandler> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<HttpResponseDto<AddressDto>> Handle(ReadAddressAllRequest readAddressAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadAddressAll {@ReadAddressAllRequest}.", readAddressAllRequest);

                var addressDtos = await _addressRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<AddressDto>(addressDtos.ToArray(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadAddressAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled ReadAddressAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error ReadAddressAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
