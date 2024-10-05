using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace ScenarioBasedVideoUploader.Compiler.CommandHandlers
{
    public static class ByOperatorProvider
    {
        static readonly Dictionary<string, Func<string, By> > ExpressionType2ExpressionFactory;

        static ByOperatorProvider()
        {
            ExpressionType2ExpressionFactory = new Dictionary< string, Func< string, By > >
            {
               ["ID"]           = By.Id,
               ["LINKTEXT"]     = By.LinkText,
               ["CSS"]          = By.CssSelector,
               ["XPATH"]        = By.XPath,
               ["NAME"]         = By.Name,
               ["CLASSNAME"]    = By.ClassName,
               ["TAGNAME"]      = By.TagName
            };
        }
        
        public static By GetBy( string expression )
        {
            var upperCaseExpression = expression.ToUpperInvariant().Trim();

            foreach( var pair in ExpressionType2ExpressionFactory )
                if( upperCaseExpression.StartsWith( $"{pair.Key}=" ) )
                    return pair.Value( expression.Substring( pair.Key.Length + 1 ) );

            return By.XPath( expression );
        }
    }
}