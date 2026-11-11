using GameStore.Application.Dtos.General;
using GameStore.Application.Features.Consoles.Requests.Queries;
using GameStore.Application.Interfaces.Persistance;
using GameStore.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GameStore.Application.Features.Consoles.RequestHandlers.Queries
{
    public class ReadConsoleAllRequestHandler(IUnitOfWork unitOfWork, ILogger<ReadConsoleAllRequestHandler> logger) : IRequestHandler<ReadConsoleAllRequest, HttpResponseDto<ConsoleDto>>
    {
        public async Task<HttpResponseDto<ConsoleDto>> Handle(ReadConsoleAllRequest readConsoleAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadConsoleAll {@ReadConsoleAllRequest}.", readConsoleAllRequest);

                if (readConsoleAllRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readConsoleAllRequest));
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadConsoleAll {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleDtos = await unitOfWork.ConsoleRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<ConsoleDto>(consoleDtos.ToArray(), consoleDtos.Any()
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent);
                logger.LogInformation("Done ReadConsoleAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadConsoleAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadConsoleAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
