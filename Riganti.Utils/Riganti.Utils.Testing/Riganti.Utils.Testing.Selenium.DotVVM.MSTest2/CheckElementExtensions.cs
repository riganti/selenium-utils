using System;
using Riganti.Utils.Testing.Selenium.Core;

namespace Riganti.Utils.Testing.Selenium.DotVVM
{
    public static class CheckElementExtensions
    {
        public static void DataBind(CheckElementWrapper wrapper, string name, Action<DataBindAttributeComparer> action, string failureMessage = null)
        {
            action(new DataBindAttributeComparer(wrapper.ElementWrapper.GetAttribute("data-bind")) { FailureMessage = failureMessage });
        }
    }
}