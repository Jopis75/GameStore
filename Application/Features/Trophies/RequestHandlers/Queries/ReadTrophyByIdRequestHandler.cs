using Application.Dtos.General;
using Application.Features.Trophies.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Trophies.RequestHandlers.Queries
{
    public class ReadTrophyByIdRequestHandler : IRequestHandler<ReadTrophyByIdRequest, HttpResponseDto<TrophyDto>>
    {
        private readonly ITrophyRepository _trophyRepository;

        private readonly IValidator<ReadTrophyByIdRequest> _validator;

        private readonly ILogger<ReadTrophyByIdRequestHandler> _logger;

        public ReadTrophyByIdRequestHandler(ITrophyRepository trophyRepository, IValidator<ReadTrophyByIdRequest> validator, ILogger<ReadTrophyByIdRequestHandler> logger)
        {
            _trophyRepository = trophyRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<TrophyDto>> Handle(ReadTrophyByIdRequest readTrophyByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadTrophyById {@ReadTrophyByIdRequest}.", readTrophyByIdRequest);

                if (readTrophyByIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readTrophyByIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error ReadTrophyById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readTrophyByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error ReadTrophyById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var trophyDto = await _trophyRepository.ReadByIdAsync(readTrophyByIdRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<TrophyDto>(trophyDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadTrophyById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled ReadTrophyById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error ReadTrophyById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
