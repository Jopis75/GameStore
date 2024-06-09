using Application.Dtos.Common;
using Application.Features.ConsoleVideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.ConsoleVideoGames.RequestHandlers.Commands
{
    public class CreateConsoleVideoGameRequestHandler : IRequestHandler<CreateConsoleVideoGameRequest, HttpResponseDto<ConsoleVideoGameDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<ConsoleVideoGameDto> _validator;

        private readonly ILogger<CreateConsoleVideoGameRequestHandler> _logger;

        public CreateConsoleVideoGameRequestHandler(IUnitOfWork unitOfWork, IValidator<ConsoleVideoGameDto> validator, ILogger<CreateConsoleVideoGameRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ConsoleVideoGameDto>> Handle(CreateConsoleVideoGameRequest createConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateConsoleVideoGame {@CreateConsoleVideoGameRequest}.", createConsoleVideoGameRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (createConsoleVideoGameRequest.ConsoleVideoGameDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(new ArgumentNullException(nameof(createConsoleVideoGameRequest.ConsoleVideoGameDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createConsoleVideoGameRequest.ConsoleVideoGameDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var createdConsoleVideoGameDto = await _unitOfWork.ConsoleVideoGameRepository.CreateAsync(createConsoleVideoGameRequest.ConsoleVideoGameDto);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<ConsoleVideoGameDto>(createdConsoleVideoGameDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled CreateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
