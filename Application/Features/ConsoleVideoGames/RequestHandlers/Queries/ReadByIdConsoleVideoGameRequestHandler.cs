using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using Application.Features.ConsoleVideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ConsoleVideoGames.RequestHandlers.Queries
{
    public class ReadByIdConsoleVideoGameRequestHandler : IRequestHandler<ReadByIdConsoleVideoGameRequest, HttpResponseDto<ReadByIdConsoleVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdConsoleVideoGameRequestDto> _validator;

        public ReadByIdConsoleVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdConsoleVideoGameRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<ReadByIdConsoleVideoGameResponseDto>> Handle(ReadByIdConsoleVideoGameRequest readByIdConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (readByIdConsoleVideoGameRequest.ReadByIdConsoleVideoGameRequestDto == null)
                {
                    return new HttpResponseDto<ReadByIdConsoleVideoGameResponseDto>(new ArgumentNullException(nameof(readByIdConsoleVideoGameRequest.ReadByIdConsoleVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(readByIdConsoleVideoGameRequest.ReadByIdConsoleVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<ReadByIdConsoleVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var consoleVideoGame = await _unitOfWork.ConsoleVideoGameRepository.ReadByIdAsync(readByIdConsoleVideoGameRequest.ReadByIdConsoleVideoGameRequestDto.Id, true);
                var readByIdConsoleVideoGameResponseDto = _mapper.Map<ReadByIdConsoleVideoGameResponseDto>(consoleVideoGame);

                return new HttpResponseDto<ReadByIdConsoleVideoGameResponseDto>(readByIdConsoleVideoGameResponseDto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadByIdConsoleVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
