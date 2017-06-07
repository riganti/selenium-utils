using System;

namespace SeleniumCore.Samples.Tests
{
    public class TestFrameworkWrongBehaviorException : Exception
    {
        public TestFrameworkWrongBehaviorException(string testFailed) :base(testFailed)
        {
            
        }
    }
}