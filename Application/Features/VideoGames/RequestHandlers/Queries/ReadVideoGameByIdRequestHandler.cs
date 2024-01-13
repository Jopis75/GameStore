﻿using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class ReadVideoGameByIdRequestHandler : IRequestHandler<ReadVideoGameByIdRequest, HttpResponseDto<ReadVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdRequestDto> _validator;

        private readonly ILogger<ReadVideoGameByIdRequestHandler> _logger;

        public ReadVideoGameByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdRequestDto> validator, ILogger<ReadVideoGameByIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadVideoGameResponseDto>> Handle(ReadVideoGameByIdRequest readByIdVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadByIdVideoGame {@ReadByIdVideoGameRequest}.", readByIdVideoGameRequest);

                if (readByIdVideoGameRequest.ReadByIdRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadVideoGameResponseDto>(new ArgumentNullException(nameof(readByIdVideoGameRequest.ReadByIdRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadbyIdVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readByIdVideoGameRequest.ReadByIdRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadbyIdVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGame = await _unitOfWork.VideoGameRepository.ReadByIdAsync(readByIdVideoGameRequest.ReadByIdRequestDto.Id, true);
                var readByIdVideoGameResponseDto = _mapper.Map<ReadVideoGameResponseDto>(videoGame);

                var httpResponseDto = new HttpResponseDto<ReadVideoGameResponseDto>(readByIdVideoGameResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadByIdVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadbyIdVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}