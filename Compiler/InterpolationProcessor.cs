using System;
using System.Collections.Generic;
using System.Linq;

namespace ScenarioBasedVideoUploader.Compiler
{
    public static class InterpolationProcessor
    {
        public const char _PARAMETER_OPEN_BRACKET  = '{';
        public const char _PARAMETER_CLOSE_BRACKET = '}';

        public static string GetProcessedString( string source, IReadOnlyDictionary< string, string > key2Value )
        {
            var result = source;

            int openBracketPos;
            do
            {
                openBracketPos = result.IndexOf( _PARAMETER_OPEN_BRACKET );
            
                if( openBracketPos != -1 )
                {
                    var closeBracketPos = result.IndexOf( _PARAMETER_CLOSE_BRACKET );
                    if( closeBracketPos == -1 || closeBracketPos < openBracketPos )
                        throw new Exception( $"Incorrect brackets in the line: {source}" );
                
                    //01234567890
                    //  { p }
                    var parameter = result.Substring( openBracketPos + 1, closeBracketPos - openBracketPos - 1 ).Trim();
                
                    if( !key2Value.ContainsKey(parameter) || string.IsNullOrEmpty(key2Value[ parameter ]) )
                        throw new Exception( $"There is no value passed for the parameter: {parameter} in the line: '{source}'" );
                
                    result = result.Replace( result.Substring(openBracketPos, closeBracketPos - openBracketPos + 1), key2Value[ parameter] );
                }
            } 
            while( openBracketPos != -1 );

            return result;
        }
        
        private static IEnumerable< string > GetParameters( string source )
        {
            var initialPos = 0;
            
            int openBracketPos;
            do
            {
                openBracketPos = source.IndexOf( _PARAMETER_OPEN_BRACKET, initialPos );
            
                if( openBracketPos != -1 )
                {
                    var closeBracketPos = source.IndexOf( _PARAMETER_CLOSE_BRACKET, openBracketPos + 1 );
                    if( closeBracketPos == -1 || closeBracketPos < openBracketPos )
                        throw new Exception( $"Incorrect brackets in the line: {source}" );
                
                    //01234567890
                    //  { p }
                    yield return source.Substring( openBracketPos + 1, closeBracketPos - openBracketPos - 1 ).Trim();

                    initialPos = closeBracketPos;
                }
            } 
            while( openBracketPos != -1 );
        }
        
        public static string[] GetStringsWithParameters( params string[] strings )
        {
            return strings.Where( e => e.Contains( _PARAMETER_OPEN_BRACKET ) ).SelectMany( GetParameters ).ToArray();
        }
    }
}