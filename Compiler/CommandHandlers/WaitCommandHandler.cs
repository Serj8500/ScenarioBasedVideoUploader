using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Support.UI;

namespace ScenarioBasedVideoUploader.Compiler.CommandHandlers
{
    public class WaitCommandHandler : BaseCommandHandler
    {
        public WaitCommandHandler() : base( "Wait", "Waits the specified amount of time", LexemeType.QuotedText.WithName("timeInSeconds"))
        {
        }

        protected override ICommand CreateCommand( IReadOnlyList< ILexeme > lexemes )
        {
            return new WaitCommand( CommandName, lexemes[ 2 ].Text.Trim('"') );
        }
    }
    
    public class WaitCommand : BaseCommand, ICommand
    {
        private readonly string _valueInSeconds;

        public WaitCommand( string commandName, string valueInSeconds ) : base( commandName )
        {
            if( string.IsNullOrEmpty(valueInSeconds) )
                throw new ArgumentException( $"{nameof(valueInSeconds)} can't be null or empty" );
            
            _valueInSeconds = valueInSeconds;
        }

        public void Process( IContext context )
        {
            var delayStr = _valueInSeconds.InContext( context );
            if( !int.TryParse( delayStr, out var delayInContext ) )
                throw new Exception($"Can't convert '{delayStr}' into integer value for a delay in ms");

            var wait = new WebDriverWait( context.WebDriver, TimeSpan.FromSeconds( delayInContext ) );
            wait.Until( _ => false );//зато не блокируется консоль =)
        }

        public override string[] GetArgumentValues()
        {
            return new []{ _valueInSeconds };
        }
    }
}