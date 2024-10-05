using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Support.UI;

namespace ScenarioBasedVideoUploader.Compiler.CommandHandlers
{
    public class WaitForSubstringCommandHandler : BaseCommandHandler
    {
        public WaitForSubstringCommandHandler() : base( "WaitForSubstring", 
            "Waits until the referenced element text contains the specified substring", 
            LexemeType.QuotedText.WithName("reference"), 
            LexemeType.QuotedText.WithName("substring"),
            LexemeType.QuotedText.WithName("timeInSeconds"))
        {
        }

        protected override ICommand CreateCommand( IReadOnlyList< ILexeme > lexemes )
        {
            return new WaitForSubstringCommand( CommandName, lexemes[ 2 ].Text.Trim('"'), lexemes[ 4 ].Text.Trim('"'), lexemes[ 6 ].Text.Trim('"') );
        }
    }

    public class WaitForSubstringCommand : BaseCommand, ICommand
    {
        private readonly string _reference;
        private readonly string _substring;
        private readonly string _timeInSeconds;

        public WaitForSubstringCommand( string commandName, string reference, string substring, string timeInSeconds ) : base( commandName )
        {
            if( string.IsNullOrEmpty(reference) )
                throw new ArgumentException( $"{nameof(reference)} can't be null or empty" );

            if( string.IsNullOrEmpty(substring) )
                throw new ArgumentException( $"{nameof(substring)} can't be null or empty" );

            if( string.IsNullOrEmpty(timeInSeconds) )
                throw new ArgumentException( $"{nameof(timeInSeconds)} can't be null or empty" );
            
            _reference      = reference;
            _substring      = substring;
            _timeInSeconds  = timeInSeconds;
        }

        public void Process( IContext context )
        {
            var delayStr = _timeInSeconds.InContext( context );
            if( !int.TryParse( delayStr, out var delayInContext ) )
                throw new Exception($"Can't convert '{delayStr}' into integer value for a delay in ms");

            var refInContext = _reference.InContext( context );
            var by = ByOperatorProvider.GetBy( refInContext );
            
            var wait = new WebDriverWait( context.WebDriver, TimeSpan.FromSeconds( delayInContext ) );            
            if( !wait.Until( drv => drv.FindElements( by ).Any( e => e.Text.Contains(_substring.InContext(context)) ) ) )
                throw new Exception($"Waiting failed for the element referenced by: '{refInContext}'; the delay was: {delayInContext} ms");
        }

        public override string[] GetArgumentValues()
        {
            return new []{ _reference, _substring, _timeInSeconds };
        }
    }
}