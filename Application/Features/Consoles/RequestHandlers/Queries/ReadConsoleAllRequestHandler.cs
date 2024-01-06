﻿using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Features.Consoles.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Consoles.RequestHandlers.Queries
{
    public class ReadConsoleAllRequestHandler : IRequestHandler<ReadConsoleAllRequest, HttpResponseDto<ReadConsoleResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<ReadConsoleAllRequestHandler> _logger;

        public ReadConsoleAllRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ReadConsoleAllRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadConsoleResponseDto>> Handle(ReadConsoleAllRequest readConsoleAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadConsoleAll {@ReadConsoleAllRequest}.", readConsoleAllRequest);

                var consoles = await _unitOfWork.ConsoleRepository.ReadAllAsync(true);
                var readConsoleResponseDtos = consoles
                    .Select(_mapper.Map<ReadConsoleResponseDto>)
                    .ToList();

                var httpResponseDto = new HttpResponseDto<ReadConsoleResponseDto>(readConsoleResponseDtos, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadConsoleAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadConsoleResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadConsoleAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
