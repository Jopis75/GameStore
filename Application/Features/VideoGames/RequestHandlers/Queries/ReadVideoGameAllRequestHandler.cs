using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class ReadVideoGameAllRequestHandler : IRequestHandler<ReadVideoGameAllRequest, HttpResponseDto<List<VideoGameDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<ReadVideoGameAllRequestHandler> _logger;

        public ReadVideoGameAllRequestHandler(IUnitOfWork unitOfWork, ILogger<ReadVideoGameAllRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<List<VideoGameDto>>> Handle(ReadVideoGameAllRequest readVideoGameAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadVideoGameAll {@ReadVideoGameAllRequest}.", readVideoGameAllRequest);

                cancellationToken.ThrowIfCancellationRequested();

                var videoGameDtos = await _unitOfWork.VideoGameRepository.ReadAllAsync(true);

                var httpResponseDto = new HttpResponseDto<List<VideoGameDto>>(videoGameDtos.ToList(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadVideoGameAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<VideoGameDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadVideoGameAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<VideoGameDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadVideoGameAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
