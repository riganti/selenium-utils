using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Abstractions.Attributes;
using Riganti.Selenium.Core.Abstractions.Reporting;

namespace Riganti.Selenium.Core.Abstractions.Exceptions
{
    public abstract class TestExceptionBase : WebDriverException
    {
        public string FullStackTrace => base.StackTrace;

        public override string Message => RenderMetadata();

        public override string StackTrace
        {
            get
            {
                var useFullStack = new StackTrace(this, true).GetFrames()?
                                       .Any(s => s.GetMethod().GetCustomAttributes<FullStackTraceAttribute>().Any()) ??
                                   false;
                if (useFullStack)
                {
                    return FullStackTrace;
                }

                var allFrames = new StackTrace(this, true).GetFrames()
                    ?.Select(s => new {Frame = s, s.GetMethod()?.ReflectedType}).Where(frame =>
                        frame.ReflectedType?.Namespace?.Contains("Riganti.Selenium") != true).ToList();
                if (allFrames == null) return "";

                var lastIndex = allFrames.LastIndexOf(allFrames.LastOrDefault(s =>
                    s.ReflectedType.FullName?.Contains("System.Runtime") == true));
                var lastIndex2 = -1;

                for (int i = lastIndex; i > -1; i--)
                {
                    if (!allFrames[i].ReflectedType.FullName?.Contains("System.Runtime") == true)
                    {
                        lastIndex2 = i + 1;
                        break;
                    }
                }

                if (lastIndex2 > -1 && lastIndex > -1)
                {
                    allFrames.RemoveRange(lastIndex2, lastIndex - lastIndex2 + 1);
                }

                var frames = allFrames.Select(frame => new StackTrace(frame.Frame).ToString());
                return string.Concat(frames);
            }
        }

        public ICheckResult CheckResult { get; set; }
        public string WebBrowser { get; set; }
        public string CurrentUrl { get; set; }
        public string Screenshot { get; set; }
        public Dictionary<string, TestRunInputResult> ReporterResults { get; set; }

        protected TestExceptionBase()
        {
        }

        protected TestExceptionBase(string message) : base()
        {
            ExceptionMessage = message;
        }

        public string ExceptionMessage { get; set; }

        protected TestExceptionBase(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TestExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Message);

            if (InnerException != null)
            {
                sb.Append(" ---> ");
                sb.AppendLine(InnerException.ToString());
                sb.AppendLine(" --- End of inner exception --- ");
            }

            sb.AppendLine(StackTrace);

            return sb.ToString();
        }

        private string RenderMetadata()
        {
            var sb = new StringBuilder();
            RenderExceptionMessage(sb);
            sb.AppendLine();
            RenderCheckResult(sb);
            sb.AppendLine();
            AppendField(sb, "Browser", WebBrowser);
            AppendField(sb, "Url", CurrentUrl);
            AppendField(sb, "Screenshot", Screenshot);
            AppendReportersResults(sb, ReporterResults);

            return sb.ToString();
        }

        private void AppendReportersResults(StringBuilder sb, Dictionary<string, TestRunInputResult> reporterResults)
        {
            if (reporterResults != null && reporterResults.Any())
            {
                sb.AppendLine("Reporters results: ");
                foreach (var result in reporterResults)
                {
                    if (result.Value is FailedTestRunInputResult failedResult)
                    {
                        if (failedResult.Exception != null)
                        {
                            AppendField(sb, $"{result.Key}.Exception ", failedResult.Exception?.Message);
                        }

                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(result.Value?.TestResultUrl))
                    {
                        AppendField(sb, $"{result.Key}.TestResultUrl ", result.Value.TestResultUrl);
                    }

                    if (!string.IsNullOrWhiteSpace(result.Value?.TestSuiteUrl))
                    {
                        AppendField(sb, $"{result.Key}.TestSuiteUrl ", result.Value.TestSuiteUrl);
                    }
                }
            }
        }

        private void RenderExceptionMessage(StringBuilder sb)
        {
            if (ExceptionMessage == null || ExceptionMessage.Length <= 0)
            {
                sb.AppendLine(GetClassName());
            }
            else
            {
                sb.Append(GetClassName());
                sb.Append(": ");
                sb.AppendLine(ExceptionMessage);
            }
        }

        private void RenderCheckResult(StringBuilder sb)
        {
            AppendField(sb, "Check results", CheckResult?.ToString());
        }

        private void AppendField(StringBuilder sb, string fieldName, string value)
        {
            sb.Append(fieldName);
            sb.Append(": ");
            sb.AppendLine(value);
        }

        private string GetClassName()
        {
            return GetType().ToString();
        }
    }
}