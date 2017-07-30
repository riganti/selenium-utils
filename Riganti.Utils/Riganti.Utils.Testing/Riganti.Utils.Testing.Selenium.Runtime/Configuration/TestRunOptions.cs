namespace Riganti.Utils.Testing.Selenium.Runtime.Configuration
{
    public class TestRunOptions
    {

        public int ActionTimeout { get; set; } = 250;

        public int TestAttemptsCount { get; set; } = 2;

        public bool TryToKillWhenNotResponding { get; set; } = true;
    }
}