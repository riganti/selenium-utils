using System;

namespace Riganti.Utils.Testing.Selenium.DotVVM
{
    public class DataBindAttributeComparer
    {
        private readonly string databindValue;

        public DataBindAttributeComparer(string databindValue)
        {
            this.databindValue = databindValue;
        }

        public string FailureMessage { get; set; }

        public void Has()
        {
            throw new NotImplementedException();
        }

        public void HasNot()
        {
            throw new NotImplementedException();
        }
    }
}