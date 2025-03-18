using Application.Dtos.General;
using Application.Features.Trophies.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Trophies.RequestHandlers.Queries
{
    public class ReadTrophyAllRequestHandler : IRequestHandler<ReadTrophyAllRequest, HttpResponseDto<TrophyDto>>
    {
        private readonly ITrophyRepository _trophyRepository;

        private readonly ILogger<ReadTrophyAllRequestHandler> _logger;

        public ReadTrophyAllRequestHandler(ITrophyRepository trophyRepository, ILogger<ReadTrophyAllRequestHandler> logger)
        {
            _trophyRepository = trophyRepository;
            _logger = logger;
        }

        public async Task<HttpResponseDto<TrophyDto>> Handle(ReadTrophyAllRequest readTrophyAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadTrophyAll {@ReadTrophyAllRequest}.", readTrophyAllRequest);

                var trophyDtos = await _trophyRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<TrophyDto>(trophyDtos.ToArray(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadTrophyAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled ReadTrophyAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error ReadTrophyAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
