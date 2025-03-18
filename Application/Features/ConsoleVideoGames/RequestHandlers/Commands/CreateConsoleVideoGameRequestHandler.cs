using Application.Dtos.General;
using Application.Features.ConsoleVideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.ConsoleVideoGames.RequestHandlers.Commands
{
    public class CreateConsoleVideoGameRequestHandler : IRequestHandler<CreateConsoleVideoGameRequest, HttpResponseDto<ConsoleVideoGameDto>>
    {
        private readonly IConsoleVideoGameRepository _consoleVideoGameRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateConsoleVideoGameRequest> _validator;

        private readonly ILogger<CreateConsoleVideoGameRequestHandler> _logger;

        public CreateConsoleVideoGameRequestHandler(IConsoleVideoGameRepository consoleVideoGameRepository, IMapper mapper, IValidator<CreateConsoleVideoGameRequest> validator, ILogger<CreateConsoleVideoGameRequestHandler> logger)
        {
            _consoleVideoGameRepository = consoleVideoGameRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<ConsoleVideoGameDto>> Handle(CreateConsoleVideoGameRequest createConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateConsoleVideoGame {@CreateConsoleVideoGameRequest}.", createConsoleVideoGameRequest);

                if (createConsoleVideoGameRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(createConsoleVideoGameRequest));
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error CreateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createConsoleVideoGameRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error CreateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleVideoGameDto = _mapper.Map<ConsoleVideoGameDto>(createConsoleVideoGameRequest);
                var createdConsoleVideoGameDto = await _consoleVideoGameRepository.CreateAsync(consoleVideoGameDto, cancellationToken);

                var httpResponseDto = new HttpResponseDto<ConsoleVideoGameDto>(createdConsoleVideoGameDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled CreateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error CreateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
