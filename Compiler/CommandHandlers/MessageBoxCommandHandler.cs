using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ScenarioBasedVideoUploader.Compiler.CommandHandlers
{
    public class MessageBoxCommandHandler : BaseCommandHandler
    {
        public MessageBoxCommandHandler() : base( "MessageBox", "Shows a message box with the specified title and text", 
            LexemeType.QuotedText.WithName("title"), LexemeType.QuotedText.WithName("text") )
        {
        }

        protected override ICommand CreateCommand( IReadOnlyList< ILexeme > lexemes )
        {
            return new MessageBoxCommand( CommandName, lexemes[ 2 ].Text.Trim('"'), lexemes[ 4 ].Text.Trim('"') );
        }
    }

    public class MessageBoxCommand : BaseCommand, ICommand
    {
        private readonly string _title;
        private readonly string _text;

        public MessageBoxCommand( string commandName, string title, string text ) : base( commandName )
        {
            if( string.IsNullOrEmpty( text ) )
                throw new ArgumentException( $"{nameof(text)} can't be null or empty" );
            
            _title = title ?? string.Empty;
            _text  = text;
        }

        public void Process( IContext context )
        {
            MessageBox.Show( _text.InContext( context ), _title.InContext( context ) );
        }

        public override string[] GetArgumentValues()
        {
            return new []{ _title, _text };
        }
    }
}