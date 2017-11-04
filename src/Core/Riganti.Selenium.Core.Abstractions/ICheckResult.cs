using System.Collections.Generic;

namespace Riganti.Selenium.Core.Abstractions
{
    public interface ICheckResult
    {
        string ErrorMessage { get; }
        IEnumerable<ICheckResult> InnerFailedResults { get; }
        IList<ICheckResult> InnerResults { get; }
        bool IsSucceeded { get; }

        string ToString();
    }
}