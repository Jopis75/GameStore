using Application.Dtos.Addresses;
using Application.Dtos.Common;
using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Addresses.RequestHandlers.Commands
{
    public class UpdateAddressRequestHandler : IRequestHandler<UpdateAddressRequest, HttpResponseDto<UpdateAddressResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateAddressRequestDto> _validator;

        public UpdateAddressRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateAddressRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<UpdateAddressResponseDto>> Handle(UpdateAddressRequest updateAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (updateAddressRequest.UpdateAddressRequestDto == null)
                {
                    return new HttpResponseDto<UpdateAddressResponseDto>(new ArgumentNullException(nameof(updateAddressRequest.UpdateAddressRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(updateAddressRequest.UpdateAddressRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<UpdateAddressResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var address = await _unitOfWork.AddressRepository.ReadByIdAsync(updateAddressRequest.UpdateAddressRequestDto.Id);
                _mapper.Map(updateAddressRequest.UpdateAddressRequestDto, address);
                var updatedAddress = await _unitOfWork.AddressRepository.UpdateAsync(address);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<UpdateAddressResponseDto>(new UpdateAddressResponseDto
                {
                    Id = updatedAddress.Id,
                    UpdatedAt = updatedAddress.UpdatedAt,
                    UpdatedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<UpdateAddressResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
