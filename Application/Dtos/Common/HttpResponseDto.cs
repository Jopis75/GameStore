using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Common
{
    public class HttpResponseDto<TDto> : IHttpResponseDto<TDto>
        where TDto : class, IResponseDto, new()
    {
        public List<TDto> Data { get; set; }

        public string ErrorMessage { get; set; }

        public bool Fail => !Success;

        public int StatusCode { get; set; }

        public bool Success => StatusCode >= 200 && StatusCode <= 299;

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
