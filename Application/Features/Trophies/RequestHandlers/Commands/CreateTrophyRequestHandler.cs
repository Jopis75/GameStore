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
        private readonly ITrophyRepository _trophyRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateTrophyRequest> _validator;

        private readonly ILogger<CreateTrophyRequestHandler> _logger;

        public CreateTrophyRequestHandler(ITrophyRepository trophyRepository, IMapper mapper, IValidator<CreateTrophyRequest> validator, ILogger<CreateTrophyRequestHandler> logger)
        {
            _trophyRepository = trophyRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<TrophyDto>> Handle(CreateTrophyRequest createTrophyRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateTrophy {@CreateTrophyRequest}.", createTrophyRequest);

                if (createTrophyRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(createTrophyRequest));
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error CreateTrophy {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createTrophyRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error CreateTrophy {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var trophyDto = _mapper.Map<TrophyDto>(createTrophyRequest);
                var createdTrophyDto = await _trophyRepository.CreateAsync(trophyDto, cancellationToken);

                var httpResponseDto = new HttpResponseDto<TrophyDto>(createdTrophyDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateTrophy {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled CreateTrophy {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error CreateTrophy {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
