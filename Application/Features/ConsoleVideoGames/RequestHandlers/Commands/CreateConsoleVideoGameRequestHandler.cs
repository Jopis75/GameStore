using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Dtos.ConsoleVideoGames;
using Application.Features.ConsoleVideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.ConsoleVideoGames.RequestHandlers.Commands
{
    public class CreateConsoleVideoGameRequestHandler : IRequestHandler<CreateConsoleVideoGameRequest, HttpResponseDto<CreateConsoleVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateConsoleVideoGameRequestDto> _validator;

        private readonly ILogger<CreateConsoleVideoGameRequestHandler> _logger;

        public CreateConsoleVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateConsoleVideoGameRequestDto> validator, ILogger<CreateConsoleVideoGameRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<CreateConsoleVideoGameResponseDto>> Handle(CreateConsoleVideoGameRequest createConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateConsoleVideoGame {@CreateConsoleVideoGameRequest}.", createConsoleVideoGameRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (createConsoleVideoGameRequest.CreateConsoleVideoGameRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<CreateConsoleVideoGameResponseDto>(new ArgumentNullException(nameof(createConsoleVideoGameRequest.CreateConsoleVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createConsoleVideoGameRequest.CreateConsoleVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<CreateConsoleVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleVideoGame = _mapper.Map<ConsoleVideoGame>(createConsoleVideoGameRequest.CreateConsoleVideoGameRequestDto);
                var createdConsoleVideoGame = await _unitOfWork.ConsoleVideoGameRepository.CreateAsync(consoleVideoGame);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<CreateConsoleVideoGameResponseDto>(new CreateConsoleVideoGameResponseDto
                {
                    Id = createdConsoleVideoGame.Id,
                    CreatedAt = createdConsoleVideoGame.CreatedAt,
                    CreatedBy = createdConsoleVideoGame.CreatedBy,
                }, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CreateConsoleVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled CreateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CreateConsoleVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
