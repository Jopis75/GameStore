namespace Application.Dtos.Common.Interfaces
{
    public interface IReadByIdResponseDto : IResponseDto
    {
        DateTime? CreatedAt { get; }

        string? CreatedBy { get; }

        DateTime? DeletedAt { get; }

        string? DeletedBy { get; }

        DateTime? UpdatedAt { get; }

        string? UpdatedBy { get; }
    }
}
