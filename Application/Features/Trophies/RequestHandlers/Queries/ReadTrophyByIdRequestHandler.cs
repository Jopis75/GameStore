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
    public class ReadTrophyByIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadTrophyByIdRequest> validator, ILogger<ReadTrophyByIdRequestHandler> logger) : IRequestHandler<ReadTrophyByIdRequest, HttpResponseDto<TrophyDto>>
    {
        public async Task<HttpResponseDto<TrophyDto>> Handle(ReadTrophyByIdRequest readTrophyByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadTrophyById {@ReadTrophyByIdRequest}.", readTrophyByIdRequest);

                if (readTrophyByIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readTrophyByIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadTrophyById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(readTrophyByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadTrophyById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var trophyDto = await unitOfWork.TrophyRepository.ReadByIdAsync(readTrophyByIdRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<TrophyDto>(trophyDto, StatusCodes.Status200OK);
                logger.LogInformation("Done ReadTrophyById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadTrophyById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadTrophyById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
