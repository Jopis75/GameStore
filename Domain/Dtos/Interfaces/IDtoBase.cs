﻿namespace Domain.Dtos.Interfaces
{
    public interface IDtoBase
    {
        DateTime? CreatedAt { get; }

        string? CreatedBy { get; }

        DateTime? DeletedAt { get; }

        string? DeletedBy { get; }

        int Id { get; }

        DateTime? UpdatedAt { get; }

        string? UpdatedBy { get; }
    }
}
