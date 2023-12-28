using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using Application.Features.ConsoleVideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.ConsoleVideoGames.RequestHandlers.Queries
{
    public class ReadAllConsoleVideoGameRequestHandler : IRequestHandler<ReadAllConsoleVideoGameRequest, HttpResponseDto<ReadAllConsoleVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<ReadAllConsoleVideoGameRequestHandler> _logger;

        public ReadAllConsoleVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ReadAllConsoleVideoGameRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadAllConsoleVideoGameResponseDto>> Handle(ReadAllConsoleVideoGameRequest readAllConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadAllConsoleVideoGame {@ReadAllConsoleVideoGameRequest}.", readAllConsoleVideoGameRequest);

                var consoleVideoGames = await _unitOfWork.ConsoleVideoGameRepository.ReadAllAsync(true);
                var readAllConsoleVideoGameResponseDtos = consoleVideoGames
                    .Select(_mapper.Map<ReadAllConsoleVideoGameResponseDto>)
                    .ToList();

                var httpResponseDto = new HttpResponseDto<ReadAllConsoleVideoGameResponseDto>(readAllConsoleVideoGameResponseDtos, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadAllConsoleVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadAllConsoleVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadAllConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
