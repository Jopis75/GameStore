using Application.Dtos.General;
using Application.Features.Companies.Requests.Queries;
using Application.Interfaces.Infrastructure;
using Application.Interfaces.Persistance;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Companies.RequestHandlers.Queries
{
    public class ReadCompanyAllRequestHandler(IUnitOfWork unitOfWork, ILoggerService<ReadCompanyAllRequestHandler> loggerService) : IRequestHandler<ReadCompanyAllRequest, HttpResponseDto<ReadCompanyAllRequest>>
    {
        public async Task<HttpResponseDto<ReadCompanyAllRequest>> Handle(ReadCompanyAllRequest readCompanyAllRequest, CancellationToken cancellationToken)
        {
            var methodName = "HandleReadCompanyAllRequest";

            try
            {
                loggerService.LogBeginInformation(methodName, readCompanyAllRequest);

                if (readCompanyAllRequest == null)
                {
                    return loggerService.LogArgumentNullException<ReadCompanyAllRequest>(new ArgumentNullException(nameof(readCompanyAllRequest)), methodName);
                }

                var companyDtos = await unitOfWork.CompanyRepository.ReadAllAsync(cancellationToken);

                return loggerService.LogDoneInformation(methodName, readCompanyAllRequest, companyDtos.Any() ? StatusCodes.Status200OK : StatusCodes.Status204NoContent);
            }
            catch (OperationCanceledException ex)
            {
                return loggerService.LogOperationCanceledException<ReadCompanyAllRequest>(ex, methodName);
            }
            catch (Exception ex)
            {
                return loggerService.LogException<ReadCompanyAllRequest>(ex, methodName);
            }
        }
    }
}
