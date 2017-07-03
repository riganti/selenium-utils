using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;
using Riganti.Utils.Testing.Selenium.Core.Checkers;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public static class AssertUI
    {
        private static readonly OperationValidator operationValidator = new OperationValidator();

        public static void CheckIfInnerText(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var checkIfInnerText = new CheckIfInnerText(rule, failureMessage);
            EvaluateCheck<UnexpectedElementStateException>(wrapper, checkIfInnerText);
        }

        private static void EvaluateCheck<TException>(ElementWrapper wrapper, ICheck check) where TException : TestExceptionBase, new()
        {
            var operationResult = check.Validate(wrapper);
            operationValidator.Validate<TException>(operationResult);
        }

        public static AnyOperationRunner Any(ElementWrapper[] wrappers)
        {
            return new AnyOperationRunner(wrappers);
        }

        public static AllOperationRunner All(ElementWrapper[] elementWrappers)
        {
            return new AllOperationRunner(elementWrappers);
        }
    }
}
