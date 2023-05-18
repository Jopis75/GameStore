namespace Application.Dtos.Common.Interfaces
{
    public interface IDeleteResponseDto : IResponseDto
    {
        DateTime? DeletedAt { get; }

        string? DeletedBy { get; }
    }
}
