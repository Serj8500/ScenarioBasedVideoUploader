using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ScenarioBasedVideoUploader.Compiler.CommandHandlers;
using ScenarioBasedVideoUploader.Compiler.LexemeHandlers;

namespace ScenarioBasedVideoUploader.Compiler
{
    public class Compiler
    {
        private readonly IReadOnlyList< IBaseHandler >      _lexemeHandlers;
        private readonly IReadOnlyList< ICommandHandler >   _commandHandlers;

        public IReadOnlyList<ICommandHandler> CommandHandlers => _commandHandlers;

        public Compiler()
        {
            _lexemeHandlers  = GetLexemeHandlers();
            _commandHandlers = GetCommandHandlers();
        }

        private IReadOnlyList<ICommandHandler> GetCommandHandlers()
        {
            var assembly            = Assembly.GetAssembly( typeof(ICommandHandler) );
            var commandHandlerTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(BaseCommandHandler)));
        
            return commandHandlerTypes.Select( e => (BaseCommandHandler) Activator.CreateInstance( e )).ToArray();
        }

        private IReadOnlyList< BaseHandler > GetLexemeHandlers()
        {
            var lexemeHandlers = new List< BaseHandler >
            {
                new TextHandler(),
                new WhiteSpaceHandler(),
                new QuotedTextHandler(),
                new IntegerHandler()
            };

            lexemeHandlers.AddRange( LexemeConstants.LexemeType2SingleCharacter.Select(
                pair => new SingleCharacterHandler( pair.Value, pair.Key ) ) );

            return lexemeHandlers;
        }

        internal IEnumerable<ILexeme> GetLexemes( IReadOnlyList<string> rows )
        {
            var currentPosition = new Position();

            do
            {
                rows.SkipEmptyLines( currentPosition );

                if( !rows.IsEndOfRows( currentPosition ) )
                {
                    var handler =
                        _lexemeHandlers.FirstOrDefault( e => e.CanStartProcessing( rows.GetSymbolAt( currentPosition ) ) );

                    if( handler is null )
                        throw new Exception(
                            $"Can't find any handler to process current position: {currentPosition} " +
                            $"symbol: '{rows.GetSymbolAt( currentPosition )}'" );


                    var lexeme = handler.Process( rows, currentPosition );
                    
                    if( lexeme.Type != LexemeType.WhiteSpace )
                        yield return lexeme;
                }
                
                rows.SkipEmptyLines( currentPosition );
            } 
            while( !rows.IsEndOfRows( currentPosition ) );
        }
        
        internal IEnumerable<ICommand> GetCommands( List<ILexeme> lexemes )
        {
            if( lexemes is null )
                throw new ArgumentException( $"{ nameof(lexemes) } cant' be null" );

            while( lexemes.Count > 0 )
            {
                var lexeme = lexemes[ 0 ];
                
                var handler = _commandHandlers.FirstOrDefault(e => e.CanProcess( lexeme ) );
                
                if( handler is null )
                    throw new Exception($"Can't find any handller to process command: {lexeme}");
                
                yield return handler.Process( lexemes );//внутри из списка удаляются обработанные лексемы
            }
        }

        public IReadOnlyList<ICommand> GetCompiledCommands( string[] rows )
        {
            var lexemes = GetLexemes( rows ).ToList();

            return GetCommands( lexemes ).ToArray();
        }
    }
}