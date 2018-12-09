using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using System; 
using System.Threading;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.CommandHandlers
{
    public class ValidateRequestCommandHandler : IRequestHandler<ValidateRequestCommand,bool>
    {
        public async Task<bool> Handle(ValidateRequestCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.ContentType))
            {
                this.AssertJson(request.Body);
            }
            return true;
        }

        private void AssertJson(string jsonString)
        {
            if ((jsonString.StartsWith("{") && jsonString.EndsWith("}")) || 
                 (jsonString.StartsWith("[") && jsonString.EndsWith("]"))) 
            {
                try
                {
                    var obj = JToken.Parse(jsonString);
                } 
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                throw new Exception($"Invalid Json string: {jsonString}");
            }
        }
    }
}
