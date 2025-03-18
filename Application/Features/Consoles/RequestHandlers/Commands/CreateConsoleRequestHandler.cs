using Application.Dtos.General;
using Application.Features.Consoles.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Consoles.RequestHandlers.Commands
{
    public class CreateConsoleRequestHandler : IRequestHandler<CreateConsoleRequest, HttpResponseDto<ConsoleDto>>
    {
        private readonly IConsoleRepository _consoleRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateConsoleRequest> _validator;

        private readonly ILogger<CreateConsoleRequestHandler> _logger;

        public CreateConsoleRequestHandler(IConsoleRepository consoleRepository, IMapper mapper, IValidator<CreateConsoleRequest> validator, ILogger<CreateConsoleRequestHandler> logger)
        {
            _consoleRepository = consoleRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<ConsoleDto>> Handle(CreateConsoleRequest createConsoleRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateConsole {@CreateConsoleRequest}.", createConsoleRequest);

                if (createConsoleRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(createConsoleRequest));
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error CreateConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createConsoleRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error CreateConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleDto = _mapper.Map<ConsoleDto>(createConsoleRequest);
                var createdConsoleDto = await _consoleRepository.CreateAsync(consoleDto, cancellationToken);

                var httpResponseDto = new HttpResponseDto<ConsoleDto>(createdConsoleDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateConsole {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled CreateConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error CreateConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
