using System.Collections.Generic;

namespace ScenarioBasedVideoUploader.Compiler.LexemeHandlers
{
    internal class TextHandler : BaseHandler
    {
        public override LexemeType Type => LexemeType.Text;

        public override bool CanStartProcessing( char symbol ) => char.IsLetter( symbol );
        
        //читаем столько, сколько можно
        public override ILexeme Process( IReadOnlyList<string> rows, IPosition currentPosition )
        {
            var position = currentPosition.ToReadOnlyPosition();
            
            return new Lexeme( Read( rows, currentPosition, CanStartProcessing ), Type, position );
        }
    }
}