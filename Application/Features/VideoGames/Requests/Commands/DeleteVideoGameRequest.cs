﻿using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.VideoGames.Requests.Commands
{
    public class DeleteVideoGameRequest : IRequest<HttpResponseDto<VideoGameDto>>
    {
        public int Id { get; set; }
    }
}
