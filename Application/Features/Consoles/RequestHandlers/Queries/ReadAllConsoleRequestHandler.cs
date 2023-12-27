using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Features.Consoles.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Consoles.RequestHandlers.Queries
{
    public class ReadAllConsoleRequestHandler : IRequestHandler<ReadAllConsoleRequest, HttpResponseDto<ReadAllConsoleResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<ReadAllConsoleRequestHandler> _logger;

        public ReadAllConsoleRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ReadAllConsoleRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadAllConsoleResponseDto>> Handle(ReadAllConsoleRequest readAllConsoleRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadAllConsole {@ReadAllConsoleRequest}.", readAllConsoleRequest);

                var consoles = await _unitOfWork.ConsoleRepository.ReadAllAsync(true);
                var readAllConsoleResponseDtos = consoles
                    .Select(_mapper.Map<ReadAllConsoleResponseDto>)
                    .ToList();

                var httpResponseDto = new HttpResponseDto<ReadAllConsoleResponseDto>(readAllConsoleResponseDtos, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadAllConsole {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadAllConsoleResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadAllConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
