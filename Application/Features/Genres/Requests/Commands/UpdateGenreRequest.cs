﻿using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Genres.Requests.Commands
{
    public class UpdateGenreRequest : IRequest<HttpResponseDto<GenreDto>>
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }
    }
}
