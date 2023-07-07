using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Features.Consoles.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Consoles.RequestHandlers.Queries
{
    public class ReadByIdConsoleRequestHandler : IRequestHandler<ReadByIdConsoleRequest, HttpResponseDto<ReadByIdConsoleResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdConsoleRequestDto> _validator;

        public ReadByIdConsoleRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdConsoleRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<ReadByIdConsoleResponseDto>> Handle(ReadByIdConsoleRequest readByIdConsoleRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (readByIdConsoleRequest.ReadByIdConsoleRequestDto == null)
                {
                    return new HttpResponseDto<ReadByIdConsoleResponseDto>(new ArgumentNullException(nameof(readByIdConsoleRequest.ReadByIdConsoleRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(readByIdConsoleRequest.ReadByIdConsoleRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<ReadByIdConsoleResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var console = await _unitOfWork.ConsoleRepository.ReadByIdAsync(readByIdConsoleRequest.ReadByIdConsoleRequestDto.Id, true);
                var readByIdConsoleResponseDto = _mapper.Map<ReadByIdConsoleResponseDto>(console);

                return new HttpResponseDto<ReadByIdConsoleResponseDto>(readByIdConsoleResponseDto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadByIdConsoleResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
