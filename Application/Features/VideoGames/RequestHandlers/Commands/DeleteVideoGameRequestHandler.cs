using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.VideoGames.RequestHandlers.Commands
{
    public class DeleteVideoGameRequestHandler : IRequestHandler<DeleteVideoGameRequest, HttpResponseDto<DeleteVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<DeleteVideoGameRequestDto> _validator;

        public DeleteVideoGameRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteVideoGameRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<DeleteVideoGameResponseDto>> Handle(DeleteVideoGameRequest deleteVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (deleteVideoGameRequest.DeleteVideoGameRequestDto == null)
                {
                    return new HttpResponseDto<DeleteVideoGameResponseDto>(new ArgumentNullException(nameof(deleteVideoGameRequest.DeleteVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(deleteVideoGameRequest.DeleteVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<DeleteVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var videoGame = await _unitOfWork.VideoGameRepository.ReadByIdAsync(deleteVideoGameRequest.DeleteVideoGameRequestDto.Id);
                var deletedVideoGame = await _unitOfWork.VideoGameRepository.DeleteAsync(videoGame);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<DeleteVideoGameResponseDto>(new DeleteVideoGameResponseDto
                {
                    Id = deletedVideoGame.Id,
                    DeletedAt = deletedVideoGame.DeletedAt,
                    DeletedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<DeleteVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
