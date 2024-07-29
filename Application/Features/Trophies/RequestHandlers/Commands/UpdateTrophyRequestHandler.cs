using Application.Dtos.General;
using Application.Features.Trophies.Requests.Commands;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Trophies.RequestHandlers.Commands
{
    public class UpdateTrophyRequestHandler : IRequestHandler<UpdateTrophyRequest, HttpResponseDto<TrophyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<UpdateTrophyRequest> _validator;

        private readonly ILogger<UpdateTrophyRequestHandler> _logger;

        public UpdateTrophyRequestHandler(IUnitOfWork unitOfWork, IValidator<UpdateTrophyRequest> validator, ILogger<UpdateTrophyRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<TrophyDto>> Handle(UpdateTrophyRequest updateTrophyRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateTrophy {@UpdateTrophyRequest}.", updateTrophyRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (updateTrophyRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(new ArgumentNullException(nameof(updateTrophyRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateTrophy {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateTrophyRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateTrophy {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var updatedTrophyDto = await _unitOfWork.TrophyRepository.UpdateAsync(updateTrophyRequest.TrophyDto, cancellationToken);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<TrophyDto>(updatedTrophyDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done UpdateTrophy {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled UpdateTrophy {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UpdateTrophy {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
