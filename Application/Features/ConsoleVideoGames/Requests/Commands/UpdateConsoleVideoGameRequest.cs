﻿using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.ConsoleVideoGames.Requests.Commands
{
    public class UpdateConsoleVideoGameRequest : IRequest<HttpResponseDto<ConsoleVideoGameDto>>
    {
        public int ConsoleId { get; set; }

        public int Id { get; set; }

        public int VideoGameId { get; set; }
    }
}
