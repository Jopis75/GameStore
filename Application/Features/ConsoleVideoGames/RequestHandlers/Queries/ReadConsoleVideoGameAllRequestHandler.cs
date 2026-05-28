using Application.Dtos.General;
using Application.Features.ConsoleVideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.ConsoleVideoGames.RequestHandlers.Queries
{
    public class ReadConsoleVideoGameAllRequestHandler(IUnitOfWork unitOfWork, ILogger<ReadConsoleVideoGameAllRequestHandler> logger) : IRequestHandler<ReadConsoleVideoGameAllRequest, HttpResponseDto<ConsoleVideoGameDto>>
    {
        public async Task<HttpResponseDto<ConsoleVideoGameDto>> Handle(ReadConsoleVideoGameAllRequest readConsoleVideoGameAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadConsoleVideoGameAll {@ReadConsoleVideoGameAllRequest}.", readConsoleVideoGameAllRequest);

                if (readConsoleVideoGameAllRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readConsoleVideoGameAllRequest));
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadConsoleVideoGameAll {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleVideoGameDtos = await unitOfWork.ConsoleVideoGameRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<ConsoleVideoGameDto>(consoleVideoGameDtos.ToArray(), consoleVideoGameDtos.Any()
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent);
                logger.LogInformation("Done ReadConsoleVideoGameAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadConsoleVideoGameAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadConsoleVideoGameAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
