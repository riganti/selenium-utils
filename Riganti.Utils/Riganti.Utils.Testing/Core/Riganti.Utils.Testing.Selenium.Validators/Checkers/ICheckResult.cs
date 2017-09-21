using System.Collections.Generic;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers
{
    public interface ICheckResult
    {
        string ErrorMessage { get; }
        IEnumerable<CheckResult> InnerFailedResults { get; }
        IList<CheckResult> InnerResults { get; }
        bool IsSucceeded { get; }

        string ToString();
    }
}