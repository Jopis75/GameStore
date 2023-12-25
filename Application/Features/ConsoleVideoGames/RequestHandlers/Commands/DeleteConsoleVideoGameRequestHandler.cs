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
    public class DeleteConsoleVideoGameRequestHandler : IRequestHandler<DeleteConsoleVideoGameRequest, HttpResponseDto<DeleteConsoleVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<DeleteConsoleVideoGameRequestDto> _validator;

        public DeleteConsoleVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<DeleteConsoleVideoGameRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<DeleteConsoleVideoGameResponseDto>> Handle(DeleteConsoleVideoGameRequest deleteConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (deleteConsoleVideoGameRequest.DeleteConsoleVideoGameRequestDto == null)
                {
                    return new HttpResponseDto<DeleteConsoleVideoGameResponseDto>(new ArgumentNullException(nameof(deleteConsoleVideoGameRequest.DeleteConsoleVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(deleteConsoleVideoGameRequest.DeleteConsoleVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<DeleteConsoleVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var consoleVideoGame = await _unitOfWork.ConsoleVideoGameRepository.ReadByIdAsync(deleteConsoleVideoGameRequest.DeleteConsoleVideoGameRequestDto.Id);
                var deletedConsoleVideoGame = await _unitOfWork.ConsoleVideoGameRepository.DeleteAsync(consoleVideoGame);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<DeleteConsoleVideoGameResponseDto>(new DeleteConsoleVideoGameResponseDto
                {
                    Id = deletedConsoleVideoGame.Id,
                    DeletedAt = deletedConsoleVideoGame.DeletedAt,
                    DeletedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<DeleteConsoleVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
