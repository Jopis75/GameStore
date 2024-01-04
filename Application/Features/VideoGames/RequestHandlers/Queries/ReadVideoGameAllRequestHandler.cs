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
    public class ReadVideoGameAllRequestHandler : IRequestHandler<ReadVideoGameAllRequest, HttpResponseDto<ReadVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<ReadVideoGameAllRequestHandler> _logger;

        public ReadVideoGameAllRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ReadVideoGameAllRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadVideoGameResponseDto>> Handle(ReadVideoGameAllRequest readAllVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadAllVideoGame {@ReadAllVideoGameRequest}.", readAllVideoGameRequest);

                var videoGames = await _unitOfWork.VideoGameRepository.ReadAllAsync(true);
                var readAllVideoGamesResponseDtos = videoGames
                    .Select(_mapper.Map<ReadVideoGameResponseDto>)
                    .ToList();

                var httpResponseDto = new HttpResponseDto<ReadVideoGameResponseDto>(readAllVideoGamesResponseDtos, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadAllVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadAllVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
