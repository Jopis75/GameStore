﻿using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.ConsoleProducts
{
    public class CreateConsoleProductRequestDto : CreateRequestDto
    {
        public int? ConsoleId { get; set; }

        public int? ProductId { get; set; }
    }
}
