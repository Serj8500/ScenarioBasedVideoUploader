using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenQA.Selenium;
using ScenarioBasedVideoUploader.Compiler;
using ScenarioBasedVideoUploader.Compiler.CommandHandlers;

namespace ScenarioBasedVideoUploader
{
    public class DebuggerWebDriver
    {
        private string?         _currentBrowserName;
        private IWebDriver?     _webDriver;
        
        public bool OutputParametersChanged { get; private set; }
        
        public bool Execute( ICommand command, Settings settings, string scenarioName, Dictionary<string, string> inputParameters, Dictionary<string, string> outputParameters )
        {
            var expectedBrowserName = settings.GetBrowserName( scenarioName );

            InitializeWebDriver( settings, expectedBrowserName );

            var context = new Context( _webDriver!, expectedBrowserName, settings, inputParameters );

            try
            {
                command.Process( context );
            }
            catch( Exception e )
            {
                MessageBox.Show( $"{e.Message}", "Error" );
                return false;
            }

            foreach( var pair in context.OutputKey2OutputValue )
                outputParameters[ pair.Key ] = pair.Value;

            OutputParametersChanged = context.OutputKey2OutputValue.Count != 0;
            
            return true;
        }

        private void InitializeWebDriver( Settings settings, string expectedBrowserName )
        {
            var isBrowserReady = false;
            if( _webDriver is not null )
            {
                try
                {
                    var _ = _webDriver.CurrentWindowHandle;
                    
                    if( _currentBrowserName != expectedBrowserName )
                        _webDriver.Quit();
                    else                    
                        isBrowserReady = true;
                }
                catch( Exception )
                {
                    // ignored
                }
            }

            if( !isBrowserReady )
            {
                _webDriver          = WebDriverProvider.GetWebDriver( expectedBrowserName, settings.WebDriverPath );
                _currentBrowserName = expectedBrowserName;
            }
        }

        public void Clear()
        {
            _webDriver?.Quit();
            _webDriver = null;
            _currentBrowserName = null;
        }
    }
}