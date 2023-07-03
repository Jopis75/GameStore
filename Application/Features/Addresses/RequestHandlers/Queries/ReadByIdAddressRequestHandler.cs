using Application.Dtos.Addresses;
using Application.Dtos.Common;
using Application.Features.Addresses.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Addresses.RequestHandlers.Queries
{
    public class ReadByIdAddressRequestHandler : IRequestHandler<ReadByIdAddressRequest, HttpResponseDto<ReadByIdAddressResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdAddressRequestDto> _validator;

        public ReadByIdAddressRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdAddressRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<ReadByIdAddressResponseDto>> Handle(ReadByIdAddressRequest readByIdAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (readByIdAddressRequest.ReadByIdAddressRequestDto == null)
                {
                    return new HttpResponseDto<ReadByIdAddressResponseDto>(new ArgumentNullException(nameof(readByIdAddressRequest.ReadByIdAddressRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(readByIdAddressRequest.ReadByIdAddressRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<ReadByIdAddressResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var address = await _unitOfWork.AddressRepository.ReadByIdAsync(readByIdAddressRequest.ReadByIdAddressRequestDto.Id, true);
                var readByIdAddressResponseDto = _mapper.Map<ReadByIdAddressResponseDto>(address);

                return new HttpResponseDto<ReadByIdAddressResponseDto>(readByIdAddressResponseDto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadByIdAddressResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
