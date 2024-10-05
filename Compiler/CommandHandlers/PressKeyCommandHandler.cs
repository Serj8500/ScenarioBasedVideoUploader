using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;

namespace ScenarioBasedVideoUploader.Compiler.CommandHandlers
{
    public class PressKeyCommandHandler : BaseCommandHandler
    {
        public PressKeyCommandHandler() : base( 
            "PressKey", 
            "Sends the specified key to the referenced element (for example: 'Escape', see OpenQA.Selenium.Keys for more info)", 
            LexemeType.QuotedText.WithName("reference"), LexemeType.QuotedText.WithName("key") )
        {
        }

        protected override ICommand CreateCommand( IReadOnlyList< ILexeme > lexemes )
        {
            return new PressKeyCommand( CommandName, lexemes[ 2 ].Text.Trim('"'), lexemes[ 4 ].Text.Trim('"') );
        }
    }
    
    public class PressKeyCommand : BaseCommand, ICommand
    {
        private readonly string _reference;
        private readonly string _key;

        public PressKeyCommand( string commandName, string reference, string key ) : base( commandName )
        {
            if( string.IsNullOrEmpty( reference ) )
                throw new ArgumentException( nameof(reference) );

            if( string.IsNullOrEmpty( key ) )
                throw new ArgumentException( nameof(key) );
            
            _reference  = reference;
            _key        = key;
        }

        public void Process( IContext context )
        {
            var by   = ByOperatorProvider.GetBy( _reference.InContext( context ) );
            
            var element = context.WebDriver.FindElement( by );
            
            element.SendKeys( GetKey( _key ) );
        }

        private string GetKey( string key )
        {
            var parts = key.Split( new[]{ '.' }, StringSplitOptions.RemoveEmptyEntries ).ToArray();

            var keyName = parts.Length == 2 ? parts[ 1 ] : parts[ 0 ];//если начинается с Keys - взять только вторую часть 
            
            var fields = typeof(Keys).GetFields(BindingFlags.Static | BindingFlags.Public); 
            foreach (FieldInfo field in fields)
                if (field.FieldType == typeof(string) && field.Name == keyName )
                    return (string) field.GetValue( null );
            
            throw new Exception($"Incorrect key: '{key}'");
        }

        public override string[] GetArgumentValues()
        {
            return new []{ _reference, _key };
        }
    }
}