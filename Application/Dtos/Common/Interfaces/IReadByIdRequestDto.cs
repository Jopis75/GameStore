namespace Application.Dtos.Common.Interfaces
{
    public interface IReadByIdRequestDto : IReadRequestDto
    {
        int Id { get; }
    }
}
