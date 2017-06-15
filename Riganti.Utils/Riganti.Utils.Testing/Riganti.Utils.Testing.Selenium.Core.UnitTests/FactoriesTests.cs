using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;
using System;

namespace Selenium.Core.UnitTests
{
    [TestClass]
    public class FactoriesTests
    {
        [TestMethod]
        public void ChromeFastModeFactoryRecreateTest()
        {
            var factory = new ChromeFastModeFactory();
            var inst1 = factory.CreateNewInstance();

            factory.Recreate();

            var inst2 = factory.CreateNewInstance();


            if (inst1 == inst2)
            {
                throw new Exception("Instances are same! Recreating instance failed.");
            }
            inst2.Dispose();
        }
    }
}