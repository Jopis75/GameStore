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
    public class UpdateTrophyRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateTrophyRequest> validator, ILogger<UpdateTrophyRequestHandler> logger) : IRequestHandler<UpdateTrophyRequest, HttpResponseDto<TrophyDto>>
    {
        public async Task<HttpResponseDto<TrophyDto>> Handle(UpdateTrophyRequest updateTrophyRequest, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin UpdateTrophy {@UpdateTrophyRequest}.", updateTrophyRequest);

                if (updateTrophyRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(updateTrophyRequest));
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error UpdateTrophy {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(updateTrophyRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error UpdateTrophy {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var trophyDto = mapper.Map<TrophyDto>(updateTrophyRequest);
                var updatedTrophyDto = await unitOfWork.TrophyRepository.UpdateAsync(trophyDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<TrophyDto>(updatedTrophyDto, StatusCodes.Status200OK);
                logger.LogInformation("Done UpdateTrophy {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled UpdateTrophy {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error UpdateTrophy {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
