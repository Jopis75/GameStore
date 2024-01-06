using Application.Dtos.Addresses;
using Application.Dtos.Common;
using Application.Features.Addresses.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Queries
{
    public class ReadAddressAllRequestHandler : IRequestHandler<ReadAddressAllRequest, HttpResponseDto<ReadAddressResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<ReadAddressAllRequestHandler> _logger;

        public ReadAddressAllRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ReadAddressAllRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadAddressResponseDto>> Handle(ReadAddressAllRequest readAddressAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadAddressAll {@ReadAddressAllRequest}.", readAddressAllRequest);

                var addresses = await _unitOfWork.AddressRepository.ReadAllAsync(true);
                var readAddressResponseDtos = addresses
                    .Select(_mapper.Map<ReadAddressResponseDto>)
                    .ToList();

                var httpResponseDto = new HttpResponseDto<ReadAddressResponseDto>(readAddressResponseDtos, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadAddressAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadAddressResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadAddressAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
