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
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<ReadTrophyAllRequestHandler> _logger;

        public ReadTrophyAllRequestHandler(IUnitOfWork unitOfWork, ILogger<ReadTrophyAllRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<TrophyDto>> Handle(ReadTrophyAllRequest readTrophyAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadTrophyAll {@ReadTrophyAllRequest}.", readTrophyAllRequest);

                cancellationToken.ThrowIfCancellationRequested();

                var trophyDtos = await _unitOfWork.TrophyRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<TrophyDto>(trophyDtos.ToArray(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadTrophyAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadTrophyAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadTrophyAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
