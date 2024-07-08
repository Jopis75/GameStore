using Application.Dtos.General;
using Application.Features.Consoles.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Consoles.RequestHandlers.Queries
{
    public class ReadConsoleAllRequestHandler : IRequestHandler<ReadConsoleAllRequest, HttpResponseDto<List<ConsoleDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<ReadConsoleAllRequestHandler> _logger;

        public ReadConsoleAllRequestHandler(IUnitOfWork unitOfWork, ILogger<ReadConsoleAllRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<List<ConsoleDto>>> Handle(ReadConsoleAllRequest readConsoleAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadConsoleAll {@ReadConsoleAllRequest}.", readConsoleAllRequest);

                cancellationToken.ThrowIfCancellationRequested();

                var consoleDtos = await _unitOfWork.ConsoleRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<List<ConsoleDto>>(consoleDtos.ToList(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadConsoleAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<ConsoleDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadConsoleAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<ConsoleDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadConsoleAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
