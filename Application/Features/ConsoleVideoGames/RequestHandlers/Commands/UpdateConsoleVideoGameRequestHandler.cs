using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using Application.Features.ConsoleVideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ConsoleVideoGames.RequestHandlers.Commands
{
    public class UpdateConsoleVideoGameRequestHandler : IRequestHandler<UpdateConsoleVideoGameRequest, HttpResponseDto<UpdateConsoleVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateConsoleVideoGameRequestDto> _validator;

        public UpdateConsoleVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateConsoleVideoGameRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<UpdateConsoleVideoGameResponseDto>> Handle(UpdateConsoleVideoGameRequest updateConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (updateConsoleVideoGameRequest.UpdateConsoleVideoGameRequestDto == null)
                {
                    return new HttpResponseDto<UpdateConsoleVideoGameResponseDto>(new ArgumentNullException(nameof(updateConsoleVideoGameRequest.UpdateConsoleVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(updateConsoleVideoGameRequest.UpdateConsoleVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<UpdateConsoleVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var consoleVideoGame = await _unitOfWork.ConsoleVideoGameRepository.ReadByIdAsync(updateConsoleVideoGameRequest.UpdateConsoleVideoGameRequestDto.Id);
                _mapper.Map(updateConsoleVideoGameRequest.UpdateConsoleVideoGameRequestDto, consoleVideoGame);
                var updatedConsoleVideoGame = await _unitOfWork.ConsoleVideoGameRepository.UpdateAsync(consoleVideoGame);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<UpdateConsoleVideoGameResponseDto>(new UpdateConsoleVideoGameResponseDto
                {
                    Id = updatedConsoleVideoGame.Id,
                    UpdatedAt = updatedConsoleVideoGame.UpdatedAt,
                    UpdatedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<UpdateConsoleVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
