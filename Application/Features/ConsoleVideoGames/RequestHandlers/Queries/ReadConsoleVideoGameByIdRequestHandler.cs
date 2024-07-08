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
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<ReadConsoleVideoGameByIdRequest> _validator;

        private readonly ILogger<ReadConsoleVideoGameByIdRequestHandler> _logger;

        public ReadConsoleVideoGameByIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadConsoleVideoGameByIdRequest> validator, ILogger<ReadConsoleVideoGameByIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ConsoleVideoGameDto>> Handle(ReadConsoleVideoGameByIdRequest readConsoleVideoGameByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadConsoleVideoGameById {@ReadConsoleVideoGameByIdRequest}.", readConsoleVideoGameByIdRequest);

                cancellationToken.ThrowIfCancellationRequested();

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

                var consoleVideoGameDto = await _unitOfWork.ConsoleVideoGameRepository.ReadByIdAsync(readConsoleVideoGameByIdRequest.Id, cancellationToken);

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
