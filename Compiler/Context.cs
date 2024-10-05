using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace ScenarioBasedVideoUploader.Compiler
{
    public interface IContext
    {
        IWebDriver                                      WebDriver               { get; }
        IReadOnlyDictionary< string, string >           Parameter2Value         { get; }
        IReadOnlyDictionary< string, string >           OutputKey2OutputValue   { get; }
        
        string GetInterpolated( string value );
        void SetOutputValue( string key, string value );
        string GetWindowTitleForCurrentBrowser();
    }

    public class Context : IContext
    {
        private readonly string _browserName;
        private readonly Settings _settings;
        public IWebDriver                               WebDriver               { get; }
        public IReadOnlyDictionary< string, string >    Parameter2Value         { get; }
        
        private readonly Dictionary< string, string >  _outputKey2OutputValue;
        public IReadOnlyDictionary< string, string >    OutputKey2OutputValue => _outputKey2OutputValue;
        
        public Context( IWebDriver webDriver, string browserName, Settings settings, IReadOnlyDictionary< string, string > parameter2Value )
        {
            WebDriver               = webDriver         ?? throw new ArgumentException( nameof( webDriver ) );
            _browserName            = browserName       ?? throw new ArgumentException( nameof( browserName ) );
            _settings               = settings          ?? throw new ArgumentException( nameof( settings ) );
            Parameter2Value         = parameter2Value   ?? throw new ArgumentException( nameof( parameter2Value ) );

            _outputKey2OutputValue  = new Dictionary< string, string >();
        }

        public string GetInterpolated( string value )
        {
            return InterpolationProcessor.GetProcessedString( value, Parameter2Value );
        }

        public void SetOutputValue( string key, string value )
        {
            if( string.IsNullOrEmpty( key ) )
                throw new ArgumentException(nameof(key));
            
            if( value is null )
                throw new ArgumentException(nameof(value));
            
            if( _outputKey2OutputValue.ContainsKey( key ) )
                throw new Exception( $"Output dictionary already contains a value for the key: '{key}'" );
            
            _outputKey2OutputValue[ key ] = value;
        }

        public string GetWindowTitleForCurrentBrowser()
        {
            return _settings.GetOpenFileWindowTitleForCurrentBrowser( _browserName );
        }
    }
}