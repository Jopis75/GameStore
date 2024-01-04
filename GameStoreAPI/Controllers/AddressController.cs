using Application.Dtos.Addresses;
using Application.Dtos.Common;
using Application.Features.Addresses.Requests.Commands;
using Application.Features.Addresses.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<AddressController> _logger;

        public AddressController(IMediator mediator, ILogger<AddressController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(CreateAddressResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateAddressResponseDto>> CreateAsync([FromBody] CreateAddressRequestDto createAddressRequestDto)
        {
            var httpResponseDto = await _mediator.Send(new CreateAddressRequest
            {
                CreateAddressRequestDto = createAddressRequestDto
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [ProducesResponseType(typeof(DeleteAddressResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeleteAddressResponseDto>> DeleteAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new DeleteAddressRequest
            {
                DeleteAddressRequestDto = new DeleteAddressRequestDto
                {
                    Id = id
                }
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadAll")]
        [ProducesResponseType(typeof(ReadAddressResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadAddressResponseDto>> ReadAllAsync()
        {
            var httpResponseDto = await _mediator.Send(new ReadAddressAllRequest());
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadById/{id}")]
        [ProducesResponseType(typeof(ReadAddressResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadAddressResponseDto>> ReadByIdAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new ReadAddressByIdRequest
            {
                ReadByIdRequestDto = new ReadByIdRequestDto
                {
                    Id = id
                }
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(typeof(UpdateAddressResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UpdateAddressResponseDto>> UpdateAsync([FromBody] UpdateAddressRequestDto updateAddressRequestDto)
        {
            var httpResponseDto = await _mediator.Send(new UpdateAddressRequest
            {
                UpdateAddressRequestDto = updateAddressRequestDto
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
