using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using OpenQA.Selenium;

namespace Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions
{
    public abstract class TestExceptionBase : WebDriverException
    {
        public string FullStackTrace => base.StackTrace;

        public override string StackTrace
        {
            get
            {
                var frames = new StackTrace(this, true)?.GetFrames()
                    ?.Where(frame => frame.GetMethod()?.ReflectedType?.Namespace
                                         ?.Contains("Riganti.Utils.Testing.Selenium") != true)
                    ?.Select(frame => new StackTrace(frame).ToString());
                return frames == null ? String.Empty : string.Concat(frames);
            }
        }

        public ICheckResult CheckResult { get; set; }

        public TestExceptionBase()
        {
        }

        public TestExceptionBase(string message) : base(message)
        {
        }

        public TestExceptionBase(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TestExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
