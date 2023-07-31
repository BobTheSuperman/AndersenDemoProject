using Microsoft.Extensions.Logging;
using System.Net;

namespace Domain.Core.DTO.Messages
{
    public class BaseResponse
    {
        public DomainResult Result { get; set; } = DomainResult.Success;

        public static T Failure<T>(HttpStatusCode code, string error, ILogger logger) where T : BaseResponse, new()
        {
            logger.LogError(error);

            return new T
            {
                Result = DomainResult.Failure(code, error)
            };
        }


        public static BaseResponse Failure(HttpStatusCode code, string error, ILogger logger)
        {
            logger.LogError(error);

            return new BaseResponse()
            {
                Result = DomainResult.Failure(code, error)
            };
        }

        public static BaseResponse Success => new BaseResponse();
    }
}
