using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.IdentityConfiguration
{
    interface Interface: IResourceOwnerPasswordValidator
    {
        Task ValidateAsync(ResourceOwnerPasswordValidationContext context);
    }
}
