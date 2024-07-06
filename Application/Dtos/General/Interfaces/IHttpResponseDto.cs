namespace Application.Dtos.General.Interfaces
{
    public interface IHttpResponseDto<TDto>
        where TDto : new()
    {
        bool ClientError { get; }

        TDto[] Data { get; }

        string ErrorMessage { get; }

        bool Informational { get; }

        bool Redirection { get; }

        bool ServerError { get; }

        int StatusCode { get; }

        bool Successful { get; }
    }
}
