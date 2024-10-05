using System;
using System.Collections.Generic;

namespace ScenarioBasedVideoUploader.Compiler.CommandHandlers
{
    /// чтобы сохранить target url загруженного ролика
    /// параметры: expression, outputKey 
    public class SaveOutputValueCommandHandler : BaseCommandHandler
    {
        public SaveOutputValueCommandHandler() : base( "SaveOutputValue", "Selects a text to be stored as an output value (for instance uploaded video's url)", LexemeType.QuotedText.WithName("reference"), LexemeType.QuotedText.WithName("outputKey") )
        {
        }

        protected override ICommand CreateCommand( IReadOnlyList< ILexeme > lexemes )
        {
            return new SaveOutputValueCommand( CommandName, lexemes[ 2 ].Text.Trim('"'), lexemes[ 4 ].Text.Trim('"') );
        }
    }

    public class SaveOutputValueCommand : BaseCommand, ICommand
    {
        private readonly string _reference;
        private readonly string _outputKey;

        public SaveOutputValueCommand( string commandName, string reference, string outputKey ) : base( commandName )
        {
            if( string.IsNullOrEmpty( reference ) )
                throw new ArgumentException( $"{nameof(reference)} can't be null or empty" );

            if( string.IsNullOrEmpty( outputKey ) )
                throw new ArgumentException( $"{nameof(outputKey)} can't be null or empty" );
            
            _reference  = reference;
            _outputKey  = outputKey;
        }

        public void Process( IContext context )
        {
            var by = ByOperatorProvider.GetBy( _reference.InContext(context) );
            
            var outputValue = context.WebDriver.FindElement( by ).Text;
            
            context.SetOutputValue( _outputKey.InContext( context ), outputValue );
        }
        
        public override string[] GetArgumentValues()
        {
            return new []{ _reference, _outputKey };
        }
    }
}