using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.DTO.Messages
{
    public class DomainResult
    {
        private static readonly DomainResult _success = new DomainResult(true);

        private KeyValuePair<HttpStatusCode, string> _errorMessage;

        public DomainResult(bool succeeded)
        {
            Succeeded = succeeded;
        }

        public bool Succeeded { get; private set; }

        public bool Failed
        {
            get { return !Succeeded; }
        }

        public KeyValuePair<HttpStatusCode, string> Error
        {
            get { return _errorMessage; }
        }

        public static DomainResult Success
        {
            get { return _success; }
        }

        public static DomainResult Failure(HttpStatusCode code, string error)
        {
            var failure = new DomainResult(false);

            if (!string.IsNullOrWhiteSpace(error))
            {
                failure._errorMessage = new KeyValuePair<HttpStatusCode, string>(code, error);
            }

            return failure;
        }
    }
}
