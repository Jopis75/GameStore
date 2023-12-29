using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class ReadAllVideoGameRequestHandler : IRequestHandler<ReadAllVideoGameRequest, HttpResponseDto<ReadAllVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<ReadAllVideoGameRequestHandler> _logger;

        public ReadAllVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ReadAllVideoGameRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadAllVideoGameResponseDto>> Handle(ReadAllVideoGameRequest readAllVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadAllVideoGame {@ReadAllVideoGameRequest}.", readAllVideoGameRequest);

                var videoGames = await _unitOfWork.VideoGameRepository.ReadAllAsync(true);
                var readAllVideoGamesResponseDtos = videoGames
                    .Select(_mapper.Map<ReadAllVideoGameResponseDto>)
                    .ToList();

                var httpResponseDto = new HttpResponseDto<ReadAllVideoGameResponseDto>(readAllVideoGamesResponseDtos, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadAllVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadAllVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadAllVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
