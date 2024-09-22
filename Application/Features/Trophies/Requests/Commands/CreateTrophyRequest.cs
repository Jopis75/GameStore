﻿using Application.Dtos.General;
using Domain.Dtos;
using Domain.Enums;
using MediatR;

namespace Application.Features.Trophies.Requests.Commands
{
    public class CreateTrophyRequest : IRequest<HttpResponseDto<TrophyDto>>
    {
        public string Name { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public string IconUrl { get; set; } = String.Empty;

        public TrophyValue TrophyValue { get; set; }

        public int VideoGameId { get; set; }
    }
}
