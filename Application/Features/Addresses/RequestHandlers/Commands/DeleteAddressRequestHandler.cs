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
    public class DeleteAddressRequestHandler : IRequestHandler<DeleteAddressRequest, HttpResponseDto<DeleteAddressResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<DeleteAddressRequestDto> _validator;

        public DeleteAddressRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<DeleteAddressRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<DeleteAddressResponseDto>> Handle(DeleteAddressRequest deleteAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (deleteAddressRequest.DeleteAddressRequestDto == null)
                {
                    return new HttpResponseDto<DeleteAddressResponseDto>(new ArgumentNullException(nameof(deleteAddressRequest.DeleteAddressRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(deleteAddressRequest.DeleteAddressRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<DeleteAddressResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var address = await _unitOfWork.AddressRepository.ReadByIdAsync(deleteAddressRequest.DeleteAddressRequestDto.Id);
                address = await _unitOfWork.AddressRepository.DeleteAsync(address);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<DeleteAddressResponseDto>(new DeleteAddressResponseDto
                {
                    Id = address.Id,
                    DeletedAt = address.DeletedAt,
                    DeletedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<DeleteAddressResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
