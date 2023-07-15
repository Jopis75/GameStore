namespace Application.Dtos.Common.Interfaces
{
    public interface IHttpResponseDto<TDto>
        where TDto : ResponseDto, new()
    {
        public bool ClientError { get; }

        List<TDto> Data { get; }

        string ErrorMessage { get; }

        public bool Informational { get; }

        public bool Redirection { get; }

        public bool ServerError { get; }

        int StatusCode { get; }

        bool Successful { get; }
    }
}
