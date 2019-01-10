using Galaxy.Bootstrapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.TestBase
{
    public abstract class ApplicationTestBase : LocalTestBaseResolver
    {
        protected ApplicationTestBase()
        {
            Build(builder =>
            {
                builder
                     .UseGalaxyCore();
            });
        }
    }
}
