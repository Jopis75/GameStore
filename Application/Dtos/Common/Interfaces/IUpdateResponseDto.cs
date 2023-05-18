namespace Application.Dtos.Common.Interfaces
{
    public interface IUpdateResponseDto : IResponseDto
    {
        DateTime? UpdatedAt { get; }

        string? UpdatedBy { get; }
    }
}
