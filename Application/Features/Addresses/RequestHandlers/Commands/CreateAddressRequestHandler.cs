using Application.Dtos.Addresses;
using Application.Dtos.Common;
using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Commands
{
    public class CreateAddressRequestHandler : IRequestHandler<CreateAddressRequest, HttpResponseDto<CreateAddressResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateAddressRequestDto> _validator;

        private readonly ILogger<CreateAddressRequestHandler> _logger;

        public CreateAddressRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateAddressRequestDto> validator, ILogger<CreateAddressRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<CreateAddressResponseDto>> Handle(CreateAddressRequest createAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateAddress {@CreateAddressRequest}.", createAddressRequest);

                if (createAddressRequest.CreateAddressRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<CreateAddressResponseDto>(new ArgumentNullException(nameof(createAddressRequest.CreateAddressRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createAddressRequest.CreateAddressRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<CreateAddressResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var address = _mapper.Map<Address>(createAddressRequest.CreateAddressRequestDto);
                var createdAddress = await _unitOfWork.AddressRepository.CreateAsync(address);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<CreateAddressResponseDto>(new CreateAddressResponseDto
                {
                    Id = createdAddress.Id,
                    CreatedAt = createdAddress.CreatedAt,
                    CreatedBy = createdAddress.CreatedBy,
                }, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateAddress {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CreateAddressResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
