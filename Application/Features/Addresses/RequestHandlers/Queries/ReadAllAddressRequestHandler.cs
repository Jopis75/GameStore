﻿using Application.Dtos.Addresses;
using Application.Dtos.Common;
using Application.Features.Addresses.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Queries
{
    public class ReadAllAddressRequestHandler : IRequestHandler<ReadAllAddressRequest, HttpResponseDto<ReadAllAddressResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<ReadAllAddressRequestHandler> _logger;

        public ReadAllAddressRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ReadAllAddressRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadAllAddressResponseDto>> Handle(ReadAllAddressRequest readAllAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Hello from ReadAllAddressRequestHandler.Handle!");

                var addresses = await _unitOfWork.AddressRepository.ReadAllAsync(true);
                var readAllAddressResponseDtos = addresses
                    .Select(_mapper.Map<ReadAllAddressResponseDto>)
                    .ToList();

                return new HttpResponseDto<ReadAllAddressResponseDto>(readAllAddressResponseDtos, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadAllAddressResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
