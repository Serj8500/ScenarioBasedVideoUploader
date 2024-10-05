using System;
using System.Collections.Generic;

namespace ScenarioBasedVideoUploader.Compiler.CommandHandlers
{
    public class SaveOutputAttributeValueCommandHandler : BaseCommandHandler
    {
        public SaveOutputAttributeValueCommandHandler() 
            : base( 
                "SaveOutputAttributeValue", 
                "Selects an attribute value to be stored as an output value (for instance uploaded video's url stored in an attribute)", 
                LexemeType.QuotedText.WithName("reference"), LexemeType.QuotedText.WithName("attributeName"), LexemeType.QuotedText.WithName("outputKey") )
        {
        }

        protected override ICommand CreateCommand( IReadOnlyList< ILexeme > lexemes )
        {
            return new SaveOutputAttributeValueCommand( CommandName, lexemes[ 2 ].Text.Trim('"'), lexemes[ 4 ].Text.Trim('"'),  lexemes[ 6 ].Text.Trim('"') );
        }
    }
    
    public class SaveOutputAttributeValueCommand : BaseCommand, ICommand
    {
        private readonly string _reference;
        private readonly string _attributeName;
        private readonly string _outputKey;

        public SaveOutputAttributeValueCommand( string commandName, string reference, string attributeName, string outputKey ) : base( commandName )
        {
            if( string.IsNullOrEmpty( reference ) )
                throw new ArgumentException( $"{nameof(reference)} can't be null or empty" );

            if( string.IsNullOrEmpty( attributeName ) )
                throw new ArgumentException( $"{nameof(attributeName)} can't be null or empty" );
            
            if( string.IsNullOrEmpty( outputKey ) )
                throw new ArgumentException( $"{nameof(outputKey)} can't be null or empty" );
            
            _reference      = reference;
            _attributeName  = attributeName;
            _outputKey      = outputKey;
        }

        public void Process( IContext context )
        {
            var by = ByOperatorProvider.GetBy( _reference.InContext(context) );
            
            var outputValue = context.WebDriver.FindElement( by ).GetAttribute( _attributeName );
            
            context.SetOutputValue( _outputKey.InContext( context ), outputValue );
        }
        
        public override string[] GetArgumentValues()
        {
            return new []{ _reference, _attributeName, _outputKey };
        }
    }
}