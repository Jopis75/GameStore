using Application.Dtos.General.Interfaces;

namespace Application.Dtos.General
{
    public class HttpResponseDto<TDto> : IHttpResponseDto<TDto>
        where TDto : new()
    {
        public bool ClientError => StatusCode >= 400 && StatusCode <= 499;

        public TDto[] Data { get; set; }

        public string ErrorMessage { get; set; }

        public bool Informational => StatusCode >= 100 && StatusCode <= 199;

        public bool Redirection => StatusCode >= 300 && StatusCode <= 399;

        public bool ServerError => StatusCode >= 500 && StatusCode <= 599;

        public int StatusCode { get; set; }

        public bool Successful => StatusCode >= 200 && StatusCode <= 299;

        public HttpResponseDto(TDto data, int statusCode)
        {
            Data = new TDto[]
            {
                data
            };
            ErrorMessage = string.Empty;
            StatusCode = statusCode;
        }

        public HttpResponseDto(TDto[] data, int statusCode)
        {
            Data = new TDto[data.Length];
            data.CopyTo(Data, 0);
            ErrorMessage = string.Empty;
            StatusCode = statusCode;
        }

        public HttpResponseDto(string errorMessage, int statusCode)
        {
            Data = Array.Empty<TDto>();
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }
    }
}
