﻿using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Consoles
{
    public class CreateConsoleResponseDto : ICreateResponseDto
    {
        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public int Id { get; set; }
    }
}
