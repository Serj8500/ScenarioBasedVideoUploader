using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;

namespace ScenarioBasedVideoUploader
{
    public class Settings
    {
        public const string _FILE_NAME                          = "settings.json";
        public const string _DEFAULT_CONFIG_TEMPLATE_FILE_NAME  = "config-template.json";

        public string FolderForScenarios        { get; }
        public string ScenarioFileExtension     { get; }
        public string FileNameForInputTemplate  { get; }
        public string WebDriverPath             { get; }
        public string OutputFolder              { get; }
        public string DefaultBrowser            { get; }
        
        public bool DeleteSuccededScenariosInInputFileAtTheEnd { get; }
        
        public string[] KnownBrowsers        => WebDriverProvider.KnownBrowsers;  
        
        public IReadOnlyDictionary<string, string> CustomScenarioName2Browser       { get; } 
        public IReadOnlyDictionary<string, string> BrowserName2OpenFileDialogTitle  { get; }

        [JsonIgnore]
        public string FileNameMaskForScenarios  => $"*.{ScenarioFileExtension}";
        
        [JsonConstructor]
        public Settings( 
            string                              folderForScenarios, 
            string                              scenarioFileExtension, 
            string                              fileNameForInputTemplate, 
            string                              webDriverPath, 
            string                              outputFolder, 
            string                              defaultBrowser,
            bool                                deleteSuccededScenariosInInputFileAtTheEnd,
            string[]                            knownBrowsers,//not used
            IReadOnlyDictionary<string, string> customScenarioName2Browser,
            IReadOnlyDictionary<string, string> browserName2OpenFileDialogTitle )
        {
            if( string.IsNullOrEmpty( folderForScenarios ) )
                throw new ArgumentException( nameof(folderForScenarios)  );
            if( string.IsNullOrEmpty( scenarioFileExtension ) )
                throw new ArgumentException( nameof(scenarioFileExtension)  );
            if( string.IsNullOrEmpty( fileNameForInputTemplate ) )
                throw new ArgumentException( nameof(fileNameForInputTemplate)  );
            if( string.IsNullOrEmpty( webDriverPath ) )
                throw new ArgumentException( nameof(webDriverPath)  );
            if( string.IsNullOrEmpty( outputFolder ) )
                throw new ArgumentException( nameof(outputFolder)  );

            if( !IsBrowserNameInTheListOfKnown( defaultBrowser ) )
                throw new Exception($"Passed {nameof(DefaultBrowser)}: {defaultBrowser} is unknown");

            foreach( var scenarioName in customScenarioName2Browser.Keys )
            {
                if( scenarioName.EndsWith( $".{ScenarioFileExtension}" ) )
                    throw new Exception($"Incorrect scenario name in the {nameof(CustomScenarioName2Browser)}: {scenarioName} (extra '.{ScenarioFileExtension} at the end?')");
            
                var browser = customScenarioName2Browser[ scenarioName ];
                if( !IsBrowserNameInTheListOfKnown( browser ) )
                    throw new Exception($"Unknown browser: {browser} specified in the {nameof(CustomScenarioName2Browser)} for the {scenarioName}");
            }
            
            FolderForScenarios                          = folderForScenarios;
            ScenarioFileExtension                       = scenarioFileExtension;
            FileNameForInputTemplate                    = fileNameForInputTemplate;
            WebDriverPath                               = webDriverPath;
            OutputFolder                                = outputFolder;
            DefaultBrowser                              = GetNormalizeBrowserName( defaultBrowser );
            CustomScenarioName2Browser                  = customScenarioName2Browser.ToDictionary( e => e.Key, e => GetNormalizeBrowserName(e.Value) );
            DeleteSuccededScenariosInInputFileAtTheEnd  = deleteSuccededScenariosInInputFileAtTheEnd;
            BrowserName2OpenFileDialogTitle             = browserName2OpenFileDialogTitle;
        }

        private static string GetNormalizeBrowserName( string browser )
        {
            return browser.ToLower( CultureInfo.InvariantCulture );
        }
        
        private bool IsBrowserNameInTheListOfKnown( string defaultBrowser )
        {
            return WebDriverProvider.KnownBrowsers.Any( e => string.Compare(e, defaultBrowser, StringComparison.InvariantCultureIgnoreCase) == 0 );
        }

        public static Settings? LoadSettings()
        {
            if( !File.Exists( _FILE_NAME ) )
                return null;

            return Serializer.DeserializeFromFile<Settings>( _FILE_NAME );
        }
        
        public static Settings CreateDefaultSettingsFile()
        {
            var settings = new Settings( 
                "scenarios", 
                "txt", 
                _DEFAULT_CONFIG_TEMPLATE_FILE_NAME, 
                "webdriver", 
                "output", 
                WebDriverProvider._CHROME,
                false, 
                Array.Empty< string >(), 
                new Dictionary< string, string >{["unusual-scenario-name"]=WebDriverProvider._FIREFOX},
                new Dictionary< string, string >{
                    [WebDriverProvider._FIREFOX] = "Open File",
                    [WebDriverProvider._CHROME ] = "Open",
                    [WebDriverProvider._EDGE   ] = "Open",
                });

            Serializer.SerializeToFile( settings, _FILE_NAME );

            return settings;
        }
        
        public string GetOpenFileWindowTitleForCurrentBrowser( string browserName )
        {
            if( !BrowserName2OpenFileDialogTitle.ContainsKey( browserName ) )
                throw new Exception($"{nameof(BrowserName2OpenFileDialogTitle)} doesn't contain the key: '{browserName}'");

            return BrowserName2OpenFileDialogTitle[ browserName ];
        }
        
        public string GetBrowserName( string scenarioName )
        {
            return CustomScenarioName2Browser.ContainsKey( scenarioName ) ? CustomScenarioName2Browser[ scenarioName ] : DefaultBrowser;
        }
    }
}