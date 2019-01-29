using Galaxy.Domain.Auditing;
using PayFlex.Identity.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Domain.AggregatesModel.PermissionAggregate
{
    public sealed class Permission : FullyAuditAggregateRootEntity
    { 
        public string Name { get; private set; }

        public string NormalizedName { get; private set; }

        public string Description { get; private set; }

        public string Url { get; private set; }

        public string Namespace { get; private set; }

        public string ServiceName { get; private set; }

        public string MethodExecutionName { get; private set; }

        private Permission()
        {
        }

        private Permission(string name, string serviceName, string methodExecutionName) : this()
        {
            this.Name = !string.IsNullOrWhiteSpace(name) ? name
                                                         : throw new ArgumentNullException(nameof(name));

            this.ServiceName = !string.IsNullOrWhiteSpace(serviceName) ? serviceName
                                                       : throw new ArgumentNullException(nameof(serviceName));

            this.MethodExecutionName = !string.IsNullOrWhiteSpace(methodExecutionName) ? methodExecutionName
                                                       : throw new ArgumentNullException(nameof(methodExecutionName));

        }

        public static Permission Create(string name, string serviceName, string methodExecutionName)
        {
            return new Permission(name, serviceName, methodExecutionName);
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new IdentityDomainException($"Invalid Permission Name: {name}");
            this.Name = name; 
        }

        public void ChangeOrSetDesciption(string desc)
        { 
            this.Description = desc; 
        }

        public void ChangeOrSetUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new IdentityDomainException($"Invalid Permission Url: {url}");
            this.Url = url; 
        }

        public void DeleteThisPermission()
        {
            this.IsDeleted = true;
        }

    }
}
