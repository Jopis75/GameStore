using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using Application.Features.ConsoleVideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ConsoleVideoGames.RequestHandlers.Commands
{
    public class CreateConsoleVideoGameRequestHandler : IRequestHandler<CreateConsoleVideoGameRequest, HttpResponseDto<CreateConsoleVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateConsoleVideoGameRequestDto> _validator;

        public CreateConsoleVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateConsoleVideoGameRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<CreateConsoleVideoGameResponseDto>> Handle(CreateConsoleVideoGameRequest createConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (createConsoleVideoGameRequest.CreateConsoleVideoGameRequestDto == null)
                {
                    return new HttpResponseDto<CreateConsoleVideoGameResponseDto>(new ArgumentNullException(nameof(createConsoleVideoGameRequest.CreateConsoleVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(createConsoleVideoGameRequest.CreateConsoleVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<CreateConsoleVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var consoleVideoGame = _mapper.Map<ConsoleVideoGame>(createConsoleVideoGameRequest.CreateConsoleVideoGameRequestDto);
                var createdConsoleVideoGame = await _unitOfWork.ConsoleVideoGameRepository.CreateAsync(consoleVideoGame);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<CreateConsoleVideoGameResponseDto>(new CreateConsoleVideoGameResponseDto
                {
                    Id = createdConsoleVideoGame.Id,
                    CreatedAt = createdConsoleVideoGame.CreatedAt,
                    CreatedBy = string.Empty
                }, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<CreateConsoleVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
