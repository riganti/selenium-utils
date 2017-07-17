namespace Riganti.Utils.Testing.Selenium.Core.Checkers.ElementWrapperCheckers
{
    public abstract class ElementWrapperCheckBase : ICheck<ElementWrapper>
    {
        public abstract CheckResult Validate(ElementWrapper wrapper);
    }
}