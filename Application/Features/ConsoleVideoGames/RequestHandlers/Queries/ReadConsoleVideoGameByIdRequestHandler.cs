using Application.Dtos.General;
using Application.Exceptions;
using Application.Features.ConsoleVideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.ConsoleVideoGames.RequestHandlers.Queries
{
    public class ReadConsoleVideoGameByIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadConsoleVideoGameByIdRequest> validator, ILogger<ReadConsoleVideoGameByIdRequestHandler> logger) : IRequestHandler<ReadConsoleVideoGameByIdRequest, HttpResponseDto<ConsoleVideoGameDto>>
    {
        public async Task<HttpResponseDto<ConsoleVideoGameDto>> Handle(ReadConsoleVideoGameByIdRequest readConsoleVideoGameByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadConsoleVideoGameById {@ReadConsoleVideoGameByIdRequest}.", readConsoleVideoGameByIdRequest);

                if (readConsoleVideoGameByIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readConsoleVideoGameByIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadConsoleVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(readConsoleVideoGameByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadConsoleVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleVideoGameDto = await unitOfWork.ConsoleVideoGameRepository.ReadByIdAsync(readConsoleVideoGameByIdRequest.Id, cancellationToken);

                if (consoleVideoGameDto.IsNullObject)
                {
                    var ex = new NotFoundException($"Could not find ConsoleVideoGame object with Id {readConsoleVideoGameByIdRequest.Id}.");
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status404NotFound);
                    logger.LogError(ex, "Error ReadConsoleVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var httpResponseDto = new HttpResponseDto<ConsoleVideoGameDto>(consoleVideoGameDto, StatusCodes.Status200OK);
                logger.LogInformation("Done ReadConsoleVideoGameById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadConsoleVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadConsoleVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
