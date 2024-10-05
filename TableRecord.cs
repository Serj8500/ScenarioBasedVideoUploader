using System;
using System.Collections.Generic;

namespace ScenarioBasedVideoUploader
{
    public class Parameter
    {
        private string _name;
        public string Name   
        { 
            get => _name;
            set 
            {
                if( string.IsNullOrEmpty( value ) )
                    throw new Exception("Empty names are not allowed");
            
                _name = value;
            }
        }

        public string Value { get; set; } 

        public Parameter( string name, string value )
        {
            Name   = name;
            Value = value;
        }

        public static Parameter Create( KeyValuePair< string, string > pair )
        {
            return new Parameter( pair.Key, pair.Value );
        }
    }
}