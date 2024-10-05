using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace ScenarioBasedVideoUploader
{
    public static class WebDriverProvider
    {
        public const string _FIREFOX       = "firefox";
        public const string _CHROME        = "chrome";
        public const string _EDGE          = "edge";
        
        private static readonly string[]   _knownBrowsers  =  { _FIREFOX, _CHROME, _EDGE };
        public  static string[]             KnownBrowsers  => _knownBrowsers;
        
        public static IWebDriver GetWebDriver( Settings settings, string scenarioName, out string browser )
        {
            browser = settings.GetBrowserName( scenarioName );

            return GetWebDriver( browser, settings.WebDriverPath );
        }

        public static IWebDriver GetWebDriver( string browser, string webDriverPath )
        {
            if( string.Compare( browser, _CHROME, StringComparison.InvariantCultureIgnoreCase ) == 0 )
                return new ChromeDriver( webDriverPath, new ChromeOptions { PageLoadStrategy = PageLoadStrategy.None } );

            if( string.Compare( browser, _EDGE, StringComparison.InvariantCultureIgnoreCase ) == 0 )
                return new EdgeDriver( webDriverPath, new EdgeOptions { PageLoadStrategy = PageLoadStrategy.None } );

            return new FirefoxDriver( webDriverPath, new FirefoxOptions { PageLoadStrategy = PageLoadStrategy.None } );
        }
    }
}