using Application.Dtos.Addresses;
using Application.Dtos.Common;
using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Commands
{
    public class UpdateAddressRequestHandler : IRequestHandler<UpdateAddressRequest, HttpResponseDto<UpdateAddressResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateAddressRequestDto> _validator;

        private readonly ILogger<UpdateAddressRequestHandler> _logger;

        public UpdateAddressRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateAddressRequestDto> validator, ILogger<UpdateAddressRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<UpdateAddressResponseDto>> Handle(UpdateAddressRequest updateAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateAddress {@UpdateAddressRequest}.", updateAddressRequest);

                if (updateAddressRequest.UpdateAddressRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<UpdateAddressResponseDto>(new ArgumentNullException(nameof(updateAddressRequest.UpdateAddressRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateAddressRequest.UpdateAddressRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<UpdateAddressResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var address = await _unitOfWork.AddressRepository.ReadByIdAsync(updateAddressRequest.UpdateAddressRequestDto.Id);
                _mapper.Map(updateAddressRequest.UpdateAddressRequestDto, address);
                var updatedAddress = await _unitOfWork.AddressRepository.UpdateAsync(address);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<UpdateAddressResponseDto>(new UpdateAddressResponseDto
                {
                    Id = updatedAddress.Id,
                    UpdatedAt = updatedAddress.UpdatedAt,
                    UpdatedBy = updatedAddress.UpdatedBy,
                }, StatusCodes.Status200OK);
                _logger.LogInformation("End UpdateAddress {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UpdateAddressResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UpdateAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
