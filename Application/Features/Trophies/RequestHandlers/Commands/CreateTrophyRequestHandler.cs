using Application.Dtos.General;
using Application.Features.Trophies.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Trophies.RequestHandlers.Commands
{
    public class CreateTrophyRequestHandler : IRequestHandler<CreateTrophyRequest, HttpResponseDto<TrophyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateTrophyRequest> _validator;

        private readonly ILogger<CreateTrophyRequestHandler> _logger;

        public CreateTrophyRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateTrophyRequest> validator, ILogger<CreateTrophyRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<TrophyDto>> Handle(CreateTrophyRequest createTrophyRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateTrophy {@CreateTrophyRequest}.", createTrophyRequest);

                if (createTrophyRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(new ArgumentNullException(nameof(createTrophyRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateTrophy {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createTrophyRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateTrophy {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var trophyDto = _mapper.Map<TrophyDto>(createTrophyRequest);
                var createdTrophyDto = await _unitOfWork.TrophyRepository.CreateAsync(trophyDto, cancellationToken);

                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<TrophyDto>(createdTrophyDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateTrophy {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled CreateTrophy {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateTrophy {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
