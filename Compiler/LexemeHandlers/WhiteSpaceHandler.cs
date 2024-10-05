using System.Collections.Generic;

namespace ScenarioBasedVideoUploader.Compiler.LexemeHandlers
{
    internal class WhiteSpaceHandler : BaseHandler
    {
        public override LexemeType Type => LexemeType.WhiteSpace;

        public override bool CanStartProcessing( char symbol ) => char.IsWhiteSpace( symbol );
        
        //пропускаем все разделители
        public override ILexeme Process( IReadOnlyList<string> rows, IPosition currentPosition )
        {
            var position = currentPosition.ToReadOnlyPosition(); 
            
            var _ = Read( rows, currentPosition, CanStartProcessing );
            
            return new Lexeme( " ", Type, position );
        }
    }
}