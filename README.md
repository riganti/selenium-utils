# test-utils
This framework makes writing UI tests much more easier and cheaper.
Framework is based on selenium wrappers and principles which canopy uses. 
Use functions CheckIf... to write your acceptations tests. Do not care about Assert class.

CheckIf... functions throws meaningful exceptions. The exceptions usualy contains informations about css selector of element, expected value and provided value etc.

## configuration
All critical settings are configurable in app.config. You can change your configuration of tests by changing of your build configuration via XML transformation. 

All settings starts with &lt;add key="selenium:...." value="" /&gt;
This make you able to define how many times the test is retried in case of failure, baseurl of relative urls, what browsers should be tested, logging etc. This app.config you are able to use for integration with continuous integration server.
