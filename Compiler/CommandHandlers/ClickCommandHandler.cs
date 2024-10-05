using System.Collections.Generic;

namespace ScenarioBasedVideoUploader.Compiler.CommandHandlers
{
    public class ClickCommandHandler : BaseCommandHandler
    {
        public ClickCommandHandler() : base( "Click", "Tells the browser to click on the referenced element", LexemeType.QuotedText.WithName( "reference" ) )
        {
        }

        protected override ICommand CreateCommand( IReadOnlyList< ILexeme > lexemes )
        {
            return new ClickCommand( CommandName, lexemes[ 2 ].Text.Trim('"') );
        }
    }

    public class ClickCommand : BaseCommand, ICommand
    {
        private readonly string _reference;

        public ClickCommand( string commandName, string reference ) : base( commandName )
        {
            _reference = reference;
        }

        public void Process( IContext context )
        {
            var by = ByOperatorProvider.GetBy( _reference.InContext( context ) );
            
            context.WebDriver.FindElement( by ).Click();
        }

        public override string[] GetArgumentValues()
        {
            return new []{ _reference };
        }
    }
}