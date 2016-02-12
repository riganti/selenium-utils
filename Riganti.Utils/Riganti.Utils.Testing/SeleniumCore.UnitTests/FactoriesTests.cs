using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore;

namespace SeleniumCore.UnitTests
{
    [TestClass]
    public class FactoriesTests
    {
        [TestMethod]
        public void ChromeFastModeFactoryRecreateTest()
        {
            var factory = new ChromeFastModeFactoryBase();
            var inst1 = factory.CreateNewInstance();
            var inst1Id = inst1.GetDriverId();
            
            factory.Recreate();

            var inst2 = factory.CreateNewInstance();
            var inst2Id = inst2.GetDriverId();

            inst2.Dispose();

            if (inst2Id == inst1Id)
            {
                throw new Exception("Instances are same! Recreating instance failed.");
            }








        }
    }
}