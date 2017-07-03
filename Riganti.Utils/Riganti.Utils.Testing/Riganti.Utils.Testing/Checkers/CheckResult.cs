using System.Collections.Generic;
using System.Linq;

namespace Riganti.Utils.Testing.Selenium.Core.Api.Checkers
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
    }
}