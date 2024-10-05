using System.Collections.Generic;

namespace ScenarioBasedVideoUploader.Compiler.CommandHandlers
{
    public class GoToCommandHandler : BaseCommandHandler
    {
        public GoToCommandHandler() : base("GoTo", "Tells the browser to go to the url", LexemeType.QuotedText.WithName("url"))
        {
        }

        protected override ICommand CreateCommand( IReadOnlyList<ILexeme> lexemes )
        {
            return new GoToCommand( CommandName, lexemes[ 2 ].Text.Trim('"') );
        }
    }
    
    public class GoToCommand : BaseCommand, ICommand
    {
        private readonly string _url;

        public GoToCommand( string commandName, string url ) : base( commandName )
        {
            _url = url;
        }

        public void Process( IContext context )
        {
            context.WebDriver.Navigate().GoToUrl( _url.InContext( context ) );
        }
        
        public override string[] GetArgumentValues()
        {
            return new []{ _url };
        }
    }
}