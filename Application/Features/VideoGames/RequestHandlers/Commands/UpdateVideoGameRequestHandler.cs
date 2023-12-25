using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.VideoGames.RequestHandlers.Commands
{
    public class UpdateVideoGameRequestHandler : IRequestHandler<UpdateVideoGameRequest, HttpResponseDto<UpdateVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateVideoGameRequestDto> _validator;

        public UpdateVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateVideoGameRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<UpdateVideoGameResponseDto>> Handle(UpdateVideoGameRequest updateVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (updateVideoGameRequest.UpdateVideoGameRequestDto == null)
                {
                    return new HttpResponseDto<UpdateVideoGameResponseDto>(new ArgumentNullException(nameof(updateVideoGameRequest.UpdateVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(updateVideoGameRequest.UpdateVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<UpdateVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var videoGame = await _unitOfWork.VideoGameRepository.ReadByIdAsync(updateVideoGameRequest.UpdateVideoGameRequestDto.Id);
                _mapper.Map(updateVideoGameRequest.UpdateVideoGameRequestDto, videoGame);
                var updatedVideoGame = await _unitOfWork.VideoGameRepository.UpdateAsync(videoGame);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<UpdateVideoGameResponseDto>(new UpdateVideoGameResponseDto
                {
                    Id = updatedVideoGame.Id,
                    UpdatedAt = updatedVideoGame.UpdatedAt,
                    UpdatedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<UpdateVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
