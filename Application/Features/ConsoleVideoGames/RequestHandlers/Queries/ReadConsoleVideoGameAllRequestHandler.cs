using Application.Dtos.General;
using Application.Features.ConsoleVideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.ConsoleVideoGames.RequestHandlers.Queries
{
    public class ReadConsoleVideoGameAllRequestHandler : IRequestHandler<ReadConsoleVideoGameAllRequest, HttpResponseDto<List<ConsoleVideoGameDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<ReadConsoleVideoGameAllRequestHandler> _logger;

        public ReadConsoleVideoGameAllRequestHandler(IUnitOfWork unitOfWork, ILogger<ReadConsoleVideoGameAllRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<List<ConsoleVideoGameDto>>> Handle(ReadConsoleVideoGameAllRequest readConsoleVideoGameAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadConsoleVideoGameAll {@ReadConsoleVideoGameAllRequest}.", readConsoleVideoGameAllRequest);

                cancellationToken.ThrowIfCancellationRequested();

                var consoleVideoGameDtos = await _unitOfWork.ConsoleVideoGameRepository.ReadAllAsync(true);

                var httpResponseDto = new HttpResponseDto<List<ConsoleVideoGameDto>>(consoleVideoGameDtos.ToList(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadConsoleVideoGameAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<ConsoleVideoGameDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadConsoleVideoGameAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<ConsoleVideoGameDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadConsoleVideoGameAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
