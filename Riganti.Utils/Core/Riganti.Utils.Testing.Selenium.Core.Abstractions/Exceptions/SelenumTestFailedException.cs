using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using OpenQA.Selenium;

namespace Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions
{
    /// <summary>
    /// This exception is thrown only from execution block in SeleniumTestExecutor. Indicates that test failed.
    /// </summary>
    [Serializable]
    public sealed class SeleniumTestFailedException : WebDriverException
    {
        public List<Exception> InnerExceptions { get; }

        public ICheckResult[] InnerCheckResults { get; }


        /// <inheritdoc />
        public override string Message => RenderMetadata();

        public string ExceptionMessage { get; set; }

        public string FullStackTrace => base.StackTrace;

        public override string StackTrace => InnerExceptions?.FirstOrDefault()?.StackTrace ?? FullStackTrace;

        public SeleniumTestFailedException(List<Exception> exps)
        {
            InnerExceptions = UnwrapExceptions(exps);
            InnerCheckResults = CreateCheckResult(exps).ToArray();
        }

        private List<Exception> UnwrapExceptions(List<Exception> exps)
        {
            var l = new List<Exception>();
            var expToUnwrap = exps.OfType<SeleniumTestFailedException>();
            var okExceptions = exps.Where(s => !(s is SeleniumTestFailedException)).ToArray();
            l.AddRange(okExceptions);

            foreach (var exp in expToUnwrap)
            {
                l.AddRange(UnwrapExceptions(exp.InnerExceptions));
            }
            return l;
        }

        private IEnumerable<ICheckResult> CreateCheckResult(IEnumerable<Exception> exceptions)
        {
            return exceptions.OfType<TestExceptionBase>().Select(e => e.CheckResult);
        }


        ///<summary>
        /// Returns basic informations to debug exception trown in selenium test.
        /// </summary>
        /// <inheritdoc/>
        public override string ToString()
        {
            return RenderMetadata();
        }
        private string RenderMetadata()
        {
            var sb = new StringBuilder();

            RenderMessage(sb);
            sb.AppendLine();

            RenderInnerAggregatedExceptions(sb);

            return sb.ToString();

        }
        private void RenderInnerCheckResults(StringBuilder sb)
        {
            RenderCollectionIfNotEmpty(sb, InnerCheckResults, "Inner check results", (index, result) => $"Inner check result #{index}: '{result}'.");
        }

        private void RenderStackTrace(StringBuilder sb, int i, Exception exception)
        {
            if (exception.InnerException != null)
            {
                RenderStackTrace(sb, i + 1, exception.InnerException);
            }

            for (int j = 0; j < i; j++)
            {
                sb.Append("   ");
            }

            if (i != 0)
            {
                //inner
                sb.AppendLine(exception.GetType().FullName + ": " + exception.Message);
                sb.AppendLine(" ---> " + exception.StackTrace);
                sb.AppendLine(" --- End of inner exception stack trace ---");
            }
            else
            {
                //top exception = this
                sb.AppendLine(exception.StackTrace);

            }
        }

        private void RenderCollectionIfNotEmpty<T>(StringBuilder sb, T[] source, string collectionName, Func<int, T, string> createMessage)
        {
            if (source.Any())
            {
                sb.AppendLine($"{collectionName}:");
                for (int i = 0; i < source.Length; i++)
                {
                    var item = source[i];
                    RenderWhenNotNull(sb, item, $"\t{createMessage(i, item).Replace("\n", "\n\t")}");
                }
            }
        }

        private void RenderInnerAggregatedExceptions(StringBuilder sb)
        {

            // add all exceptions to report
            if (InnerExceptions != null && InnerExceptions.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("-----------  Aggregated Inner Exceptions  -----------");
                sb.AppendLine();

                for (int i = 0; i < InnerExceptions.Count; i++)
                {
                    RenderInnerException(sb, i);
                }
            }
        }

        private void RenderInnerException(StringBuilder sb, int i)
        {
            sb.AppendLine($"// Exception #{i + 1}: ");
            sb.AppendLine(InnerExceptions[i].ToString());

            //add separator
            if (i != InnerExceptions.Count - 1)
            {
                sb.AppendLine();
                sb.AppendLine();
            }
        }

        private void RenderWhenNotNull<T>(StringBuilder builder, T testedObject, string message)
        {
            if (testedObject != null)
            {
                builder.AppendLine(message);

            }
        }
        private void RenderMessage(StringBuilder sb)
        {
            RenderWhenNotNull(sb, ExceptionMessage, ExceptionMessage);
        }


    }
}