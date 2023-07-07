using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Features.Consoles.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Console = Domain.Entities.Console;

namespace Application.Features.Consoles.RequestHandlers.Commands
{
    public class CreateConsoleRequestHandler : IRequestHandler<CreateConsoleRequest, HttpResponseDto<CreateConsoleResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateConsoleRequestDto> _validator;

        public CreateConsoleRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateConsoleRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<CreateConsoleResponseDto>> Handle(CreateConsoleRequest createConsoleRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (createConsoleRequest.CreateConsoleRequestDto == null)
                {
                    return new HttpResponseDto<CreateConsoleResponseDto>(new ArgumentNullException(nameof(createConsoleRequest.CreateConsoleRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(createConsoleRequest.CreateConsoleRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<CreateConsoleResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var console = _mapper.Map<Console>(createConsoleRequest.CreateConsoleRequestDto);
                var createdConsole = await _unitOfWork.ConsoleRepository.CreateAsync(console);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<CreateConsoleResponseDto>(new CreateConsoleResponseDto
                {
                    Id = createdConsole.Id,
                    CreatedAt = createdConsole.CreatedAt,
                    CreatedBy = string.Empty
                }, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<CreateConsoleResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
