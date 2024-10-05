using System.Collections.Generic;

namespace ScenarioBasedVideoUploader.Compiler.LexemeHandlers
{
    internal class IntegerHandler : BaseHandler
    {
        public override LexemeType Type => LexemeType.Integer;

        public override bool CanStartProcessing( char symbol ) => char.IsDigit( symbol );
        
        //пропускаем все разделители
        public override ILexeme Process( IReadOnlyList<string> rows, IPosition currentPosition )
        {
            var position = currentPosition.ToReadOnlyPosition(); 
            
            return new Lexeme( Read( rows, currentPosition, CanStartProcessing ), Type, position );        
        }
    }
}