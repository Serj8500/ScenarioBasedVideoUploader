using System;
using System.Collections.Generic;
using System.Linq;

namespace ScenarioBasedVideoUploader.Compiler.CommandHandlers
{
    //argument  - имя переменной      (например "url") 
    //parameter - значение переменной (например "{url}")

    public interface ICommandHandler
    {
        bool     CanProcess( ILexeme lexeme );
        
        ICommand Process( List<ILexeme> lexemes );
        
        string   GetInfo();
        string   GetCommandTemplate();
        
        string   CommandName                    { get; }
        string   Description                    { get; }
    }
    
    public abstract class BaseCommandHandler : ICommandHandler
    {
        protected readonly ArgumentInfo[]       ArgumentInfos;
        
        public             string               CommandName         { get; }
        public             string               Description         { get; }
        
        protected BaseCommandHandler( string commandName, string description, params ArgumentInfo[] argumentInfos )
        {
            if( string.IsNullOrEmpty( commandName ) )
                throw new ArgumentException( $"{ nameof(commandName) } can't be null or empty" );

            if( string.IsNullOrEmpty( description ) )
                throw new ArgumentException( $"{ nameof(description) } can't be null or empty" );
            
            if( argumentInfos is null || argumentInfos.Length == 0 )
                throw new ArgumentException( $"{ nameof(argumentInfos) } can't be null or empty" );
            
            CommandName     = commandName;
            ArgumentInfos   = argumentInfos;
            Description     = description;
        }

        public bool CanProcess( ILexeme lexeme )
        {
            if( lexeme is null )
                throw new ArgumentException( $"{nameof(lexeme)} can't be null" );
            
            return lexeme.Type == LexemeType.Text && string.Compare( lexeme.Text, CommandName, StringComparison.InvariantCultureIgnoreCase ) == 0;
        }

        private void ValidateLexemeSequence( List< ILexeme > lexemes, LexemeType[] expectedLexemeTypes )
        {
            var isLexemeCountCorrect    = lexemes.Count >= expectedLexemeTypes.Length;
            var isLexemeSequenceCorrect = isLexemeCountCorrect && 
                Enumerable.Range( 0, expectedLexemeTypes.Length ).All( index => lexemes[ index ].Type == expectedLexemeTypes[ index ] );
            
            if( !isLexemeSequenceCorrect )
            {
                var actualSequence   = string.Join( ", ", lexemes.Select( e => e.Type ).Take( expectedLexemeTypes.Length ) );
                var expectedSequence = string.Join( ", ", expectedLexemeTypes );
                
                throw new Exception($"Can't process {CommandName} command, unexpected sequence of lexeme types: " +
                                    $"{actualSequence}, the expected is: {expectedSequence}");
            }
        }

        private void RemoveLexemeSequence( List< ILexeme > lexemes, int initialCount )
        {
            if( lexemes.Count < initialCount )
                throw new ArgumentException($"Incorrect {nameof(initialCount)}: {initialCount}, total number is: {lexemes.Count}");
            
            lexemes.RemoveRange(0, initialCount);

            while( lexemes.Count > 0 && lexemes[ 0 ].Type == LexemeType.Semicolon )
                lexemes.RemoveAt( 0 );
        }

        public ICommand Process( List< ILexeme > lexemes )
        {
            if( lexemes is null || lexemes.Count == 0 )
                throw new ArgumentException($"{nameof(lexemes)} can't be null or empty");

            var expectedLexemeTypeSequence = GetExpectedLexemeTypeSequence();
            
            ValidateLexemeSequence( lexemes, expectedLexemeTypeSequence );

            var command = CreateCommand( lexemes ); 

            RemoveLexemeSequence( lexemes, expectedLexemeTypeSequence.Length );
            
            return command;
        }

        public string GetInfo()
        {
            var command = GetCommandTemplate();
            
            return $"{command, -50} - {Description}";
        }

        public string GetCommandTemplate()
        {
            return $"{CommandName}( {string.Join(", ", ArgumentInfos.Select( e => e.ArgumentName ))} )";
        }

        private LexemeType[] GetExpectedLexemeTypeSequence()
        {
            return GetExpectedLexemeTypesForCommand( ArgumentInfos.Select( e => e.LexemeType ).ToArray() ).ToArray();
        }

        private IEnumerable<LexemeType> GetExpectedLexemeTypesForCommand( IReadOnlyList<LexemeType> argumentTypes )
        {
            yield return LexemeType.Text;
            yield return LexemeType.OpenRoundBracket;
            
            if( argumentTypes is not null && argumentTypes.Count > 0 )
                for( int index = 0; index < argumentTypes.Count; index++ )
                {
                    yield return argumentTypes[ index ];
                    if( index != argumentTypes.Count - 1 )
                        yield return LexemeType.Comma;
                }
            
            yield return LexemeType.CloseRoundBracket;
        }

        protected abstract ICommand     CreateCommand                   ( IReadOnlyList< ILexeme > lexemes );

        public class ArgumentInfo
        {
            public LexemeType   LexemeType    { get; }
            public string       ArgumentName  { get; }

            public ArgumentInfo( LexemeType lexemeType, string argumentName )
            {
                if( string.IsNullOrEmpty(argumentName) )
                    throw new ArgumentException( $"{nameof(argumentName)} cant' be null or empty" );
                
                LexemeType    = lexemeType;
                ArgumentName  = argumentName;
            }
        }

        protected string[] GetArgumentNames()
        {
            return ArgumentInfos.Select( e => e.ArgumentName ).ToArray(); 
        }
    }
}