# Rebranding !!
Our test-utils becomes selenium-utils!

We want to build more libs based on Selenium Core. 
Currently we have integration for BrowserStack and in the future we plan new lib for easier testing of DotVVM apps. <br/>
Unfortunatly, this brings some breaking changes.

##Breaking changes:
- namespace `Riganti.Utils.Testing.SeleniumCore` changed to `Riganti.Utils.Testing.Selenium.Core`
- namespace `Riganti.Utils.Testing.SeleniumCore.BrowserStack` changed to `Riganti.Utils.Testing.Selenium.BrowserStack`



# selenium-utils
**The purpouse of this framework is to make UI testing much more easier and cheaper.**
Framework is based on selenium wrappers and principles which canopy uses. 

Creation of tests full of Assert.AssertationFunc() was not acceptable for us. These tests usualy become very hard to read even it checks simple case. 

To simplify testing and reading of these tests we are using `CheckIfSomething` functions.

These functions throws meaningful exceptions when something goes wrong. The exceptions usualy contains informations about css selector of element, expected value and provided value etc.



## configuration
All critical settings are configurable in app.config. You can easily change your configuration of tests by changing of app.config via XML transformation. This provides you the possibility of creation specific configuration for your CI servers.  

All settings starts with `<add key="selenium:_configurationKey_" value="" />` <br />
This make you able to define how many times the test is retried in case of failure, baseurl of relative urls, what browsers should be tested, logging etc. 

## Recommended configuration
This configuration is tested on CI servers.

- Selenium.WebDriver.dll =2.53.1
- Selenium.Support.dll =2.53.1 
- Chrome =52
- Firefox =38
- ChromeDriver.exe =2.22
- IEDriverServer.exe =2.53.1

**Do not use Selenium.Support.dll 3.0.0-Beta-\*. Compatible ChromeDriver.exe contains bugs with alerts and SwitchTo().**


