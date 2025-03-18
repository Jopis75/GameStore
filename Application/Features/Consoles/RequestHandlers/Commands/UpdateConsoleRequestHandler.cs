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
    public class UpdateConsoleRequestHandler : IRequestHandler<UpdateConsoleRequest, HttpResponseDto<ConsoleDto>>
    {
        private readonly IConsoleRepository _consoleRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateConsoleRequest> _validator;

        private readonly ILogger<UpdateConsoleRequestHandler> _logger;

        public UpdateConsoleRequestHandler(IConsoleRepository consoleRepository, IMapper mapper, IValidator<UpdateConsoleRequest> validator, ILogger<UpdateConsoleRequestHandler> logger)
        {
            _consoleRepository = consoleRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<ConsoleDto>> Handle(UpdateConsoleRequest updateConsoleRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateConsole {@UpdateConsoleRequest}.", updateConsoleRequest);

                if (updateConsoleRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(updateConsoleRequest));
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error UpdateConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateConsoleRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error UpdateConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleDto = _mapper.Map<ConsoleDto>(updateConsoleRequest);
                var updatedConsoleDto = await _consoleRepository.UpdateAsync(consoleDto, cancellationToken);

                var httpResponseDto = new HttpResponseDto<ConsoleDto>(updatedConsoleDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done UpdateConsole {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled UpdateConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error UpdateConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
