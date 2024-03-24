﻿namespace Application.Dtos.Common
{
    public interface IHttpResponseDto<TDto>
        where TDto : new()
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
