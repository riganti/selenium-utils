using System.Collections.Generic;
using System.Linq;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers
{
    public class CheckResult : ICheckResult
    {
        public bool IsSucceeded { get; }
        public string ErrorMessage { get; }
        public IList<ICheckResult> InnerResults { get; }
        public IEnumerable<ICheckResult> InnerFailedResults => Enumerable.Where(InnerResults, result => !result.IsSucceeded);

        private CheckResult()
        {
            IsSucceeded = true;
        }

        public CheckResult(string errorMessage, IEnumerable<CheckResult> checkResults = null)
        {
            IsSucceeded = false;
            ErrorMessage = errorMessage;
            InnerResults = checkResults?.Cast<ICheckResult>().ToList() ?? new List<ICheckResult>();
        }

        public static CheckResult Succeeded { get; } = new CheckResult();

        public override string ToString()
        {
            if (IsSucceeded)
            {
                return $"Check was successful";
            }

            if (Enumerable.Any(InnerResults))
            {
                return ErrorMessage + "\n\t" + string.Join("\n\t", Enumerable.Select(InnerResults, ir => ir.ToString().Replace("\n", "\n\t")));
            }

            return ErrorMessage;
        }
    }
}