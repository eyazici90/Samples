using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayFlex.Identity.API.Attributes
{ 
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisableResponseFormatterAttribute :  Attribute // Marker Attribute
    {
    }
}
