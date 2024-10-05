using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Support.UI;

namespace ScenarioBasedVideoUploader.Compiler.CommandHandlers
{
    public class WaitForCommandHandler : BaseCommandHandler
    {
        public WaitForCommandHandler() : base( "WaitFor", 
            "Waits for the referenced element to appear on the page", 
            LexemeType.QuotedText.WithName("reference"), 
            LexemeType.QuotedText.WithName("timeInSeconds") )
        {
        }

        protected override ICommand CreateCommand( IReadOnlyList< ILexeme > lexemes )
        {
            return new WaitForCommand( CommandName, lexemes[ 2 ].Text.Trim('"'), lexemes[ 4 ].Text.Trim('"') );
        }
    }

    public class WaitForCommand : BaseCommand, ICommand
    {
        private readonly string _timeInSeconds;
        private readonly string _reference;
        
        public WaitForCommand( string commandName, string reference, string timeInSeconds ) : base( commandName )
        {
            if( string.IsNullOrEmpty( timeInSeconds ) )
                throw new ArgumentException( $"{nameof(timeInSeconds)} can't be empty" );
            
            if( string.IsNullOrEmpty( reference ) )
                throw new ArgumentException( $"{nameof(reference)} can't be empty" );
            
            _timeInSeconds  = timeInSeconds;
            _reference      = reference;
        }

        public void Process( IContext context )
        {
            var delayStr = _timeInSeconds.InContext( context );
            if( !int.TryParse( delayStr, out var delayInContext ) )
                throw new Exception($"Can't convert '{delayStr}' into integer value for a delay in ms");
            
            var wait = new WebDriverWait( context.WebDriver, TimeSpan.FromSeconds( delayInContext ) );
            
            var refInContext = _reference.InContext( context );
            var by = ByOperatorProvider.GetBy( refInContext );
            
            if( !wait.Until( drv => drv.FindElements( by ).Any() ) )
                throw new Exception($"Waiting failed for the element referenced by: '{refInContext}'; the delay was: {delayInContext} ms");
        }
        
        public override string[] GetArgumentValues()
        {
            return new []{ _reference, _timeInSeconds };
        }
    }
}