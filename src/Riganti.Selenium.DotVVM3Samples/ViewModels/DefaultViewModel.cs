namespace Riganti.Selenium.DotVVM3.Samples.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
		public string Title { get; set;}

		public DefaultViewModel()
		{
			Title = "Hello from DotVVM!";
		}
    }
}
