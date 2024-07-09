using Application.Dtos.General;
using Application.Features.Addresses.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Queries
{
    public class ReadAddressAllRequestHandler : IRequestHandler<ReadAddressAllRequest, HttpResponseDto<AddressDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<ReadAddressAllRequestHandler> _logger;

        public ReadAddressAllRequestHandler(IUnitOfWork unitOfWork, ILogger<ReadAddressAllRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<AddressDto>> Handle(ReadAddressAllRequest readAddressAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadAddressAll {@ReadAddressAllRequest}.", readAddressAllRequest);

                cancellationToken.ThrowIfCancellationRequested();

                var addressDtos = await _unitOfWork.AddressRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<AddressDto>(addressDtos.ToArray(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadAddressAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadAddressAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadAddressAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
