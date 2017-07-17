using System;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.BrowserWrapperCheckers
{
    public abstract class BrowserWrapperCheckBase : ICheck<BrowserWrapper>
    {
        public abstract CheckResult Validate(BrowserWrapper wrapper);
    }
}