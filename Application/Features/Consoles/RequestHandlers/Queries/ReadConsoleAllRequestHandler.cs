using Application.Dtos.General;
using Application.Features.Consoles.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Consoles.RequestHandlers.Queries
{
    public class ReadConsoleAllRequestHandler : IRequestHandler<ReadConsoleAllRequest, HttpResponseDto<ConsoleDto>>
    {
        private readonly IConsoleRepository _consoleRepository;

        private readonly ILogger<ReadConsoleAllRequestHandler> _logger;

        public ReadConsoleAllRequestHandler(IConsoleRepository consoleRepository, ILogger<ReadConsoleAllRequestHandler> logger)
        {
            _consoleRepository = consoleRepository;
            _logger = logger;
        }

        public async Task<HttpResponseDto<ConsoleDto>> Handle(ReadConsoleAllRequest readConsoleAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadConsoleAll {@ReadConsoleAllRequest}.", readConsoleAllRequest);

                var consoleDtos = await _consoleRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<ConsoleDto>(consoleDtos.ToArray(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadConsoleAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled ReadConsoleAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error ReadConsoleAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
