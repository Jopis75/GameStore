using Application.Dtos.General;
using Application.Features.Companies.Requests.Commands;
using Application.Features.Companies.Requests.Queries;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("CreateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<CompanyDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<CompanyDto>>> CreateAsync([FromBody] CreateCompanyRequest createCompanyRequest)
        {
            var httpResponseDto = await mediator.Send(createCompanyRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPost]
        [Route("CreateWithHeadquarterAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<CompanyDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<CompanyDto>>> CreateWithHeadquarterAsync([FromBody] CreateCompanyWithHeadquarterRequest createCompanyWithHeadquarterRequest)
        {
            var httpResponseDto = await mediator.Send(createCompanyWithHeadquarterRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpDelete]
        [Route("DeleteAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<CompanyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<CompanyDto>>> DeleteAsync(int id)
        {
            var httpResponseDto = await mediator.Send(new DeleteCompanyRequest { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadAllAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<CompanyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<CompanyDto>>> ReadAllAsync()
        {
            var httpResponseDto = await mediator.Send(new ReadCompanyAllRequest());
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadByIdAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<CompanyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<CompanyDto>>> ReadByIdAsync(int id)
        {
            var httpResponseDto = await mediator.Send(new ReadCompanyByIdRequest { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPut]
        [Route("UpdateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<CompanyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<CompanyDto>>> UpdateAsync([FromBody] UpdateCompanyRequest updateCompanyRequest)
        {
            var httpResponseDto = await mediator.Send(updateCompanyRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
