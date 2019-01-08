using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Shared.Responses
{
    public sealed class HttpApiResultResponse
    {
        public object Result { get; set; }

        public string TargetUrl { get; set; }

        public bool Success { get; set; }

        public bool Error { get; set; }
         
    }

    public class Error
    {
        public string ErrorMessage { get; set; }

        public string InnerException { get; set; }

        public string StackTrace { get; set; }
    }

}
