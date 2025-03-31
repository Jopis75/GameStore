using Application.Dtos.General;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class CreateCompanyWithHeadquarterRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateCompanyWithHeadquarterRequest> validator, ILogger<CreateCompanyWithHeadquarterRequestHandler> logger) : IRequestHandler<CreateCompanyWithHeadquarterRequest, HttpResponseDto<CompanyDto>>
    {
        public async Task<HttpResponseDto<CompanyDto>> Handle(CreateCompanyWithHeadquarterRequest createCompanyWithHeadquarterRequest, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin CreateCompanyWithHeadquarter {@CreateCompanyWithHeadquarterRequest}.", createCompanyWithHeadquarterRequest);

                if (createCompanyWithHeadquarterRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(createCompanyWithHeadquarterRequest));
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error CreateCompanyWithHeadquarter {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(createCompanyWithHeadquarterRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error CreateCompanyWithHeadquarter {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var addressDto = mapper.Map<AddressDto>(createCompanyWithHeadquarterRequest);
                var createdAddressDto = await unitOfWork.AddressRepository.CreateAsync(addressDto, cancellationToken);

                createCompanyWithHeadquarterRequest.HeadquarterId = createdAddressDto.Id;

                var companyDto = mapper.Map<CompanyDto>(createCompanyWithHeadquarterRequest);
                var createdCompanyDto = await unitOfWork.CompanyRepository.CreateAsync(companyDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<CompanyDto>(createdCompanyDto, StatusCodes.Status201Created);
                logger.LogInformation("Done CreateCompanyWithHeadquarter {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled CreateCompanyWithHeadquarter {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error CreateCompanyWithHeadquarter {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
