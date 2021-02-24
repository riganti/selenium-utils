namespace Riganti.Selenium.Core
{
    /// <summary>
    /// Options to modify behavior of WaitFor block.
    /// </summary>
    public class WaitForOptions
    {
        /// <summary>
        /// Defines whether WaitFor can be applied or no.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Defines whether WaitBlock should throw original exception or aggregated WaitBlockException in case of failure.
        /// </summary>
        public bool ThrowOriginalException { get; set; } = true;

        /// <summary>
        /// Custom message that is show when check fails.
        /// </summary>
        public string FailureMessage { get; set; }
        /// <summary>
        /// Maximum time within condition has to be satisfied.
        /// </summary>
        public int Timeout { get; set; } = 8000;
        /// <summary>
        /// Define interval between periodical check whether given condition is satisfied or not.
        /// </summary>
        public int CheckInterval { get; set; } = 30;
        /// <summary>
        /// This option disables usage of WaitFor block.
        /// </summary>
        public readonly static WaitForOptions Disabled = new WaitForOptions() { Enabled = false };
        /// <summary>
        /// Options set to total timeout 8s, check interval 30ms
        /// </summary>
        public readonly static WaitForOptions DefaultOptions = new WaitForOptions { };
        /// <summary>
        /// Options set to total timeout 4s, check interval 30ms
        /// </summary>
        public readonly static WaitForOptions ShortTimeout = new WaitForOptions { CheckInterval = 30, Enabled = true, FailureMessage = null, Timeout = 4000 };
        /// <summary>
        /// Options set to total timeout 16s, check interval 30ms
        /// </summary>
        public readonly static WaitForOptions LongTimeout = new WaitForOptions { CheckInterval = 30, Enabled = true, FailureMessage = null, Timeout = 16000 };
    }
}
