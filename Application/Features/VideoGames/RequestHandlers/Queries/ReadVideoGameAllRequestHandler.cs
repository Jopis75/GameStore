﻿using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class ReadVideoGameAllRequestHandler : IRequestHandler<ReadVideoGameAllRequest, HttpResponseDto<VideoGameDto>>
    {
        private readonly IVideoGameRepository _videoGameRepository;

        private readonly ILogger<ReadVideoGameAllRequestHandler> _logger;

        public ReadVideoGameAllRequestHandler(IVideoGameRepository videoGameRepository, ILogger<ReadVideoGameAllRequestHandler> logger)
        {
            _videoGameRepository = videoGameRepository ?? throw new ArgumentNullException(nameof(videoGameRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<VideoGameDto>> Handle(ReadVideoGameAllRequest readVideoGameAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadVideoGameAll {@ReadVideoGameAllRequest}.", readVideoGameAllRequest);

                var videoGameDtos = await _videoGameRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(videoGameDtos.ToArray(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadVideoGameAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadVideoGameAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadVideoGameAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
