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
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<ReadTrophyByIdRequest> _validator;

        private readonly ILogger<ReadTrophyByIdRequestHandler> _logger;

        public ReadTrophyByIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadTrophyByIdRequest> validator, ILogger<ReadTrophyByIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<TrophyDto>> Handle(ReadTrophyByIdRequest readTrophyByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadTrophyById {@ReadTrophyByIdRequest}.", readTrophyByIdRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (readTrophyByIdRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(new ArgumentNullException(nameof(readTrophyByIdRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadTrophyById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readTrophyByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadTrophyById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var trophyDto = await _unitOfWork.TrophyRepository.ReadByIdAsync(readTrophyByIdRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<TrophyDto>(trophyDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadTrophyById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadTrophyById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadTrophyById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
