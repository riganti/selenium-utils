using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riganti.Utils.Testing.Selenium.Core.Exceptions
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class HideFromStackTraceAttribute : Attribute { }
}
