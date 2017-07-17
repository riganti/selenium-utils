using System.Collections.Generic;
using System.Linq;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckResult
    {
        public bool IsSucceeded { get; }
        public string ErrorMessage { get; }
        public IList<CheckResult> InnerResults { get; }
        public IEnumerable<CheckResult> InnerFailedResults => InnerResults.Where(result => !result.IsSucceeded);

        private CheckResult()
        {
            IsSucceeded = true;
        }

        public CheckResult(string errorMessage, IEnumerable<CheckResult> checkResults = null)
        {
            IsSucceeded = false;
            ErrorMessage = errorMessage;
            InnerResults = checkResults?.ToList() ?? new List<CheckResult>();
        }

        public static CheckResult Succeeded { get; } = new CheckResult();

        public override string ToString()
        {
            if (IsSucceeded)
            {
                return $"Check was successful";
            }

            if (InnerResults.Any())
            {
                return ErrorMessage + "\n" + string.Join("\n\t", InnerResults.Select(ir => ir.ToString()));
            }

            return ErrorMessage;
        }
    }
}