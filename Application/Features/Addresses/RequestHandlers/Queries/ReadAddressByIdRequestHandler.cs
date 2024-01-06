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
    public class ReadAddressByIdRequestHandler : IRequestHandler<ReadAddressByIdRequest, HttpResponseDto<ReadAddressResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdRequestDto> _validator;

        private readonly ILogger<ReadAddressByIdRequestHandler> _logger;

        public ReadAddressByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdRequestDto> validator, ILogger<ReadAddressByIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadAddressResponseDto>> Handle(ReadAddressByIdRequest readAddressByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadAddressById {@ReadAddressByIdRequest}.", readAddressByIdRequest);

                if (readAddressByIdRequest.ReadByIdRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadAddressResponseDto>(new ArgumentNullException(nameof(readAddressByIdRequest.ReadByIdRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadAddressById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readAddressByIdRequest.ReadByIdRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadAddressResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadAddressById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var address = await _unitOfWork.AddressRepository.ReadByIdAsync(readAddressByIdRequest.ReadByIdRequestDto.Id, true);
                var readAddressResponseDto = _mapper.Map<ReadAddressResponseDto>(address);

                var httpResponseDto = new HttpResponseDto<ReadAddressResponseDto>(readAddressResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadAddressById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadAddressResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadAddressById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
