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

            // binding has this signiture:
            // data-bind="text: some.expression() + something.inside, attr: { style: {color: '#711000', cursor: 'pointer'}}, displayed: IsDisplayed()" 
        }

        public void HasNot()
        {

            throw new NotImplementedException();
            // binding has this signiture:
            // data-bind="text: some.expression() + something.inside, attr: { style: {color: '#711000', cursor: 'pointer'}}, displayed: IsDisplayed()" 

        }
    }
}