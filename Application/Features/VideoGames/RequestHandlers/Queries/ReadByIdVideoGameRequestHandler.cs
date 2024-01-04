using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class ReadByIdVideoGameRequestHandler : IRequestHandler<ReadByIdVideoGameRequest, HttpResponseDto<ReadVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdVideoGameRequestDto> _validator;

        private readonly ILogger<ReadByIdVideoGameRequestHandler> _logger;

        public ReadByIdVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdVideoGameRequestDto> validator, ILogger<ReadByIdVideoGameRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadVideoGameResponseDto>> Handle(ReadByIdVideoGameRequest readByIdVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadByIdVideoGame {@ReadByIdVideoGameRequest}.", readByIdVideoGameRequest);

                if (readByIdVideoGameRequest.ReadByIdVideoGameRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadVideoGameResponseDto>(new ArgumentNullException(nameof(readByIdVideoGameRequest.ReadByIdVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadbyIdVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readByIdVideoGameRequest.ReadByIdVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadbyIdVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGame = await _unitOfWork.VideoGameRepository.ReadByIdAsync(readByIdVideoGameRequest.ReadByIdVideoGameRequestDto.Id, true);
                var readByIdVideoGameResponseDto = _mapper.Map<ReadVideoGameResponseDto>(videoGame);

                var httpResponseDto = new HttpResponseDto<ReadVideoGameResponseDto>(readByIdVideoGameResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadByIdVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadbyIdVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
