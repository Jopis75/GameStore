﻿using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Trophies.Requests.Commands
{
    public class UpdateTrophyRequest : IRequest<HttpResponseDto<TrophyDto>>
    {
        public TrophyDto TrophyDto { get; set; } = new();
    }
}
