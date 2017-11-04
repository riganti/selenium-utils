using System;
using Riganti.Selenium.Core;

namespace Riganti.Selenium.DotVVM
{
    public static class CheckElementExtensions
    {
        public static void DataBind(CheckElementWrapper wrapper, string name, Action<DataBindAttributeComparer> action, string failureMessage = null)
        {
            action(new DataBindAttributeComparer(wrapper.ElementWrapper.GetAttribute("data-bind")) { FailureMessage = failureMessage });
        }
    }
}