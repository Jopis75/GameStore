using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Commands
{
    public class CreateVideoGameRequestHandler : IRequestHandler<CreateVideoGameRequest, HttpResponseDto<CreateVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateVideoGameRequestDto> _validator;

        private readonly ILogger<CreateVideoGameRequestHandler> _logger;

        public CreateVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateVideoGameRequestDto> validator, ILogger<CreateVideoGameRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<CreateVideoGameResponseDto>> Handle(CreateVideoGameRequest createVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateVideoGame {@CreateVideoGameRequest}.", createVideoGameRequest);

                if (createVideoGameRequest.CreateVideoGameRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<CreateVideoGameResponseDto>(new ArgumentNullException(nameof(createVideoGameRequest.CreateVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createVideoGameRequest.CreateVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<CreateVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGame = _mapper.Map<VideoGame>(createVideoGameRequest.CreateVideoGameRequestDto);
                var createdVideoGame = await _unitOfWork.VideoGameRepository.CreateAsync(videoGame);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<CreateVideoGameResponseDto>(new CreateVideoGameResponseDto
                {
                    Id = createdVideoGame.Id,
                    CreatedAt = createdVideoGame.CreatedAt,
                    CreatedBy = string.Empty
                }, StatusCodes.Status201Created);
                _logger.LogInformation("End CreateVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CreateVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
