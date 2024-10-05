using System.Collections.Generic;

namespace ScenarioBasedVideoUploader.Compiler.CommandHandlers
{
    public class CommentCommandHandler : BaseCommandHandler
    {
        public CommentCommandHandler() : base( "Comment", "Displays a comment in the scenario command list. Does nothing.", LexemeType.QuotedText.WithName("text") )
        {
        }

        protected override ICommand CreateCommand( IReadOnlyList< ILexeme > lexemes )
        {
            return new CommentCommand( CommandName, lexemes[ 2 ].Text.Trim('"') );
        }
    }
    
    public class CommentCommand : BaseCommand, ICommand
    {
        private readonly string _text;
        
        public CommentCommand( string commandName, string text ) : base( commandName )
        {
            _text      = text;
        }

        public void Process( IContext context )
        {
            //does nothing
        }
        
        public override string[] GetArgumentValues()
        {
            return new []{ _text };
        }
    }    
}