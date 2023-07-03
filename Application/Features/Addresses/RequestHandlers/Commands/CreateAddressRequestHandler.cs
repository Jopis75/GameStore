using Application.Dtos.Addresses;
using Application.Dtos.Common;
using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Addresses.RequestHandlers.Commands
{
    public class CreateAddressRequestHandler : IRequestHandler<CreateAddressRequest, HttpResponseDto<CreateAddressResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateAddressRequestDto> _validator;

        public CreateAddressRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateAddressRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<CreateAddressResponseDto>> Handle(CreateAddressRequest createAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (createAddressRequest.CreateAddressRequestDto == null)
                {
                    return new HttpResponseDto<CreateAddressResponseDto>(new ArgumentNullException(nameof(createAddressRequest.CreateAddressRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(createAddressRequest.CreateAddressRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<CreateAddressResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var address = _mapper.Map<Address>(createAddressRequest.CreateAddressRequestDto);
                var createdAddress = await _unitOfWork.AddressRepository.CreateAsync(address);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<CreateAddressResponseDto>(new CreateAddressResponseDto
                {
                    Id = createdAddress.Id,
                    CreatedAt = createdAddress.CreatedAt,
                    CreatedBy = string.Empty
                }, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<CreateAddressResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
