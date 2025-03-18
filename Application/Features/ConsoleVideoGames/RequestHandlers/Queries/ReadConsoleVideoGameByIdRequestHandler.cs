using Application.Dtos.General;
using Application.Features.ConsoleVideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.ConsoleVideoGames.RequestHandlers.Queries
{
    public class ReadConsoleVideoGameByIdRequestHandler : IRequestHandler<ReadConsoleVideoGameByIdRequest, HttpResponseDto<ConsoleVideoGameDto>>
    {
        private readonly IConsoleVideoGameRepository _consoleVideoGameRepository;

        private readonly IValidator<ReadConsoleVideoGameByIdRequest> _validator;

        private readonly ILogger<ReadConsoleVideoGameByIdRequestHandler> _logger;

        public ReadConsoleVideoGameByIdRequestHandler(IConsoleVideoGameRepository consoleVideoGameRepository, IValidator<ReadConsoleVideoGameByIdRequest> validator, ILogger<ReadConsoleVideoGameByIdRequestHandler> logger)
        {
            _consoleVideoGameRepository = consoleVideoGameRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<ConsoleVideoGameDto>> Handle(ReadConsoleVideoGameByIdRequest readConsoleVideoGameByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadConsoleVideoGameById {@ReadConsoleVideoGameByIdRequest}.", readConsoleVideoGameByIdRequest);

                if (readConsoleVideoGameByIdRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(new ArgumentNullException(nameof(readConsoleVideoGameByIdRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadConsoleVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readConsoleVideoGameByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadConsoleVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleVideoGameDto = await _consoleVideoGameRepository.ReadByIdAsync(readConsoleVideoGameByIdRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<ConsoleVideoGameDto>(consoleVideoGameDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadConsoleVideoGameById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadConsoleVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadConsoleVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
