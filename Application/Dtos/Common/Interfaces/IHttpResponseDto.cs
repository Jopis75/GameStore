namespace Application.Dtos.Common.Interfaces
{
    public interface IHttpResponseDto<TDto>
        where TDto : class, IResponseDto, new()
    {
        List<TDto> Data { get; }

        string ErrorMessage { get; }

        bool Fail { get; }

        int StatusCode { get; }

        bool Success { get; }
    }
}
