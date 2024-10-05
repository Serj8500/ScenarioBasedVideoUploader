using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace ScenarioBasedVideoUploader.Compiler.CommandHandlers
{
    public class WriteCommandHandler : BaseCommandHandler
    {
        public WriteCommandHandler() : base( "Write", "Sends the specified text to the referenced element", 
            LexemeType.QuotedText.WithName("reference"), LexemeType.QuotedText.WithName("text") )
        {
        }

        protected override ICommand CreateCommand( IReadOnlyList< ILexeme > lexemes )
        {
            return new WriteCommand( CommandName, lexemes[ 2 ].Text.Trim('"'), lexemes[ 4 ].Text.Trim('"') );
        }
    }

    public class WriteCommand : BaseCommand, ICommand
    {
        private readonly string _text;
        private readonly string _reference;
        
        public WriteCommand( string commandName, string reference, string text ) : base( commandName )
        {
            _text      = text;
            _reference = reference;
        }

        public void Process( IContext context )
        {
            var by   = ByOperatorProvider.GetBy( _reference.InContext( context ) );
            
            var element = context.WebDriver.FindElement( by );
            
            element.SendKeys( Keys.Control + "a" );
            element.SendKeys( Keys.Backspace );

            if( !string.IsNullOrEmpty(_text) )
            {
                var lines = _text.InContext( context ).Split(new[]{"\\n"}, StringSplitOptions.None);

                var nLines = lines.Length;
                for( int lineIndex = 0; lineIndex < nLines; lineIndex++ )
                {
                    element.SendKeys( lines[ lineIndex ] );
                    
                    if( lineIndex < nLines - 1 )
                        element.SendKeys( Keys.Enter );
                }
            }
        }
        
        public override string[] GetArgumentValues()
        {
            return new []{ _reference, _text };
        }
    }
}