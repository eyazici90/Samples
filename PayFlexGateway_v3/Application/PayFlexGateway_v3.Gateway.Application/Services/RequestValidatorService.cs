using MediatR;
using PayFlexGateway_v3.Gateway.Services.Contracts;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Application.Services
{
    public class RequestValidatorService : IRequestValidatorService
    {
        private readonly IMediator _mediatr;
        public RequestValidatorService(IMediator mediatr) =>
            _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
        
        public async Task ValidateRequest(ValidateRequestCommand command) =>
            await _mediatr.Send(command);
        
    }
}
