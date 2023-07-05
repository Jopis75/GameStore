﻿using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Consoles
{
    public class UpdateConsoleResponseDto : IUpdateResponseDto
    {
        public int Id { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
