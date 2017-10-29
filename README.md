# selenium-utils
**The purpose of this framework is to make UI testing much more easier and cheaper.**
Framework is based on selenium wrappers and principles which canopy uses. 

Creation of tests full of Assert.AssertationFunc() was not acceptable for us. These tests usualy become very hard to read even it checks simple case. 

To simplify testing and reading of these tests we are using `CheckIfSomething` functions.

These functions throws meaningful exceptions when something goes wrong. The exceptions usualy contains informations about css selector of element, expected value and provided value etc.

## configuration
All critical settings are configurable in seleniumconfig.json. This provides you the possibility of creation specific configuration for your CI servers.  
