﻿using Application.Dtos.General;
using Application.Features.ConsoleVideoGames.Requests.Commands;
using Application.Features.ConsoleVideoGames.Requests.Queries;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsoleVideoGameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsoleVideoGameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CreateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<ConsoleVideoGameDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<ConsoleVideoGameDto>>> CreateAsync([FromBody] CreateConsoleVideoGameRequest createConsoleVideoGameRequest)
        {
            var httpResponseDto = await _mediator.Send(createConsoleVideoGameRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpDelete]
        [Route("DeleteAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<ConsoleVideoGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<ConsoleVideoGameDto>>> DeleteAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new DeleteConsoleVideoGameRequest { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadAllAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<ConsoleVideoGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<ConsoleVideoGameDto>>> ReadAllAsync()
        {
            var httpResponseDto = await _mediator.Send(new ReadConsoleVideoGameAllRequest());
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadByIdAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<ConsoleVideoGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<ConsoleVideoGameDto>>> ReadByIdAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new ReadConsoleVideoGameByIdRequest { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPut]
        [Route("UpdateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<ConsoleVideoGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<ConsoleVideoGameDto>>> UpdateAsync([FromBody] UpdateConsoleVideoGameRequest updateConsoleVideoGameRequest)
        {
            var httpResponseDto = await _mediator.Send(updateConsoleVideoGameRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
