using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.ElementWrapperCollectionCheckers
{
    public abstract class ElementWrapperCollectionCheckBase : ICheck<ElementWrapperCollection>
    {
        public abstract CheckResult Validate(ElementWrapperCollection wrapper);
    }
}
