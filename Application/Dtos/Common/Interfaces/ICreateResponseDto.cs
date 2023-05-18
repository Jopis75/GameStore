namespace Application.Dtos.Common.Interfaces
{
    public interface ICreateResponseDto : IResponseDto
    {
        DateTime? CreatedAt { get; }

        string? CreatedBy { get; }
    }
}
