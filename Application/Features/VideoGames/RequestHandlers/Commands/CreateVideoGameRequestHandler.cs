using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.VideoGames.RequestHandlers.Commands
{
    public class CreateVideoGameRequestHandler : IRequestHandler<CreateVideoGameRequest, HttpResponseDto<CreateVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateVideoGameRequestDto> _validator;

        public CreateVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateVideoGameRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<CreateVideoGameResponseDto>> Handle(CreateVideoGameRequest createVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (createVideoGameRequest.CreateVideoGameRequestDto == null)
                {
                    return new HttpResponseDto<CreateVideoGameResponseDto>(new ArgumentNullException(nameof(createVideoGameRequest.CreateVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(createVideoGameRequest.CreateVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<CreateVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var videoGame = _mapper.Map<VideoGame>(createVideoGameRequest.CreateVideoGameRequestDto);
                var createdVideoGame = await _unitOfWork.VideoGameRepository.CreateAsync(videoGame);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<CreateVideoGameResponseDto>(new CreateVideoGameResponseDto
                {
                    Id = createdVideoGame.Id,
                    CreatedAt = createdVideoGame.CreatedAt,
                    CreatedBy = string.Empty
                }, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<CreateVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
