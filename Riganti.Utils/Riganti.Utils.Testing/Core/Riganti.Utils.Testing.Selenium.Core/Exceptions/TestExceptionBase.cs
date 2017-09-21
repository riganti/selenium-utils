using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Validators.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Exceptions
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

        public ICheckResult CheckResult { get; internal set; }

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
