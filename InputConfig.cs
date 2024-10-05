using System;
using System.Collections.Generic;

namespace ScenarioBasedVideoUploader
{
    public class InputConfig
    {
        public string[]                     Scenarios   { get; }
        public Dictionary<string, string>   Parameters  { get; }
        
        public InputConfig( string[] scenarios, Dictionary< string, string > parameters )
        {
            Scenarios   = scenarios  ?? new string[0];
            Parameters  = parameters ?? new Dictionary< string, string >();
        }
    }
    
    public class OutputConfig : InputConfig
    {
        public Dictionary<string, Dictionary<string, string>> Scenario2Info { get; }
        
        public OutputConfig( InputConfig inputConfig, Dictionary<string, Dictionary<string, string>> scenario2Info ) 
            : base( inputConfig.Scenarios, inputConfig.Parameters )
        {
            Scenario2Info = scenario2Info;
        }
    }
}