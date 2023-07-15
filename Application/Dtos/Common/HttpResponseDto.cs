namespace Application.Dtos.Common
{
    public class HttpResponseDto<TDto> : IHttpResponseDto<TDto>
        where TDto : ResponseDto, new()
    {
        public bool ClientError => StatusCode >= 400 && StatusCode <= 499;

        public List<TDto> Data { get; set; }

        public string ErrorMessage { get; set; }

        public bool Informational => StatusCode >= 100 && StatusCode <= 199;

        public bool Redirection => StatusCode >= 300 && StatusCode <= 399;

        public bool ServerError => StatusCode >= 500 && StatusCode <= 599;

        public int StatusCode { get; set; }

        public bool Successful => StatusCode >= 200 && StatusCode <= 299;

        public HttpResponseDto(TDto data, int statusCode)
        {
            Data = new List<TDto>
            {
                data
            };
            ErrorMessage = string.Empty;
            StatusCode = statusCode;
        }

        public HttpResponseDto(List<TDto> data, int statusCode)
        {
            Data = new List<TDto>();
            Data.AddRange(data);
            ErrorMessage = string.Empty;
            StatusCode = statusCode;
        }

        public HttpResponseDto(string errorMessage, int statusCode)
        {
            Data = new List<TDto>();
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }
    }
}
