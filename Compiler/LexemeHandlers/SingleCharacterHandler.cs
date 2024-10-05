using System.Collections.Generic;

namespace ScenarioBasedVideoUploader.Compiler.LexemeHandlers
{
    internal class SingleCharacterHandler : BaseHandler
    {
        private readonly char _symbol;

        public override LexemeType Type { get; }

        public SingleCharacterHandler( char symbol, LexemeType lexemeType )
        {
            _symbol = symbol;
            Type = lexemeType;
        }

        //пропускаем 1 символ
        public override ILexeme Process( IReadOnlyList< string > rows, IPosition currentPosition )
        {
            var position = currentPosition.ToReadOnlyPosition();
            
            currentPosition.GoToNextSymbol();
            
            return new Lexeme( _symbol.ToString(), Type, position );
        }

        public override bool CanStartProcessing( char symbol ) => symbol == _symbol;
    }
}