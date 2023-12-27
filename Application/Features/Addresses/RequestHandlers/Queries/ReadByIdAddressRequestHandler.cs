using Application.Dtos.Addresses;
using Application.Dtos.Common;
using Application.Features.Addresses.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Queries
{
    public class ReadByIdAddressRequestHandler : IRequestHandler<ReadByIdAddressRequest, HttpResponseDto<ReadByIdAddressResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdAddressRequestDto> _validator;

        private readonly ILogger<ReadByIdAddressRequestHandler> _logger;

        public ReadByIdAddressRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdAddressRequestDto> validator, ILogger<ReadByIdAddressRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadByIdAddressResponseDto>> Handle(ReadByIdAddressRequest readByIdAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadByIdAddress {@ReadByIdAddressRequest}.", readByIdAddressRequest);

                if (readByIdAddressRequest.ReadByIdAddressRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadByIdAddressResponseDto>(new ArgumentNullException(nameof(readByIdAddressRequest.ReadByIdAddressRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readByIdAddressRequest.ReadByIdAddressRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadByIdAddressResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var address = await _unitOfWork.AddressRepository.ReadByIdAsync(readByIdAddressRequest.ReadByIdAddressRequestDto.Id, true);
                var readByIdAddressResponseDto = _mapper.Map<ReadByIdAddressResponseDto>(address);

                var httpResponseDto = new HttpResponseDto<ReadByIdAddressResponseDto>(readByIdAddressResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadByIdAddress {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadByIdAddressResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadByIdAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
