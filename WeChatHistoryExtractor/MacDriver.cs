using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;


namespace WeChatHistoryExtractor
{
    class MacDriver : AppiumDriver<RemoteWebElement>
    {
        public MacDriver(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities) : base(commandExecutor, desiredCapabilities)
        {
        }
 
        public MacDriver(ICapabilities desiredCapabilities) : base(desiredCapabilities)
        {
        }
 
        public MacDriver(ICapabilities desiredCapabilities, TimeSpan commandTimeout) : base(desiredCapabilities, commandTimeout)
        {
        }
 
        public MacDriver(AppiumServiceBuilder builder, ICapabilities desiredCapabilities) : base(builder, desiredCapabilities)
        {
        }
 
        public MacDriver(AppiumServiceBuilder builder, ICapabilities desiredCapabilities, TimeSpan commandTimeout) : base(builder, desiredCapabilities, commandTimeout)
        {
        }
 
        public MacDriver(Uri remoteAddress, ICapabilities desiredCapabilities) : base(remoteAddress, desiredCapabilities)
        {
        }
 
        public MacDriver(AppiumLocalService service, ICapabilities desiredCapabilities) : base(service, desiredCapabilities)
        {
        }
 
        public MacDriver(Uri remoteAddress, ICapabilities desiredCapabilities, TimeSpan commandTimeout) : base(remoteAddress, desiredCapabilities, commandTimeout)
        {
        }
 
        public MacDriver(AppiumLocalService service, ICapabilities desiredCapabilities, TimeSpan commandTimeout) : base(service, desiredCapabilities, commandTimeout)
        {
        }
    }
}