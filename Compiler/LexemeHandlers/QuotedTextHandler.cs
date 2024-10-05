using System;
using System.Collections.Generic;

namespace ScenarioBasedVideoUploader.Compiler.LexemeHandlers
{
    internal class QuotedTextHandler : BaseHandler
    {
        private const char _QUOTE_SYMBOL = '"';
        
        public override LexemeType Type => LexemeType.QuotedText;
        
        //весь текст должен быть на одной строке
        public override ILexeme Process( IReadOnlyList< string > rows, IPosition currentPosition )
        {
            var position = currentPosition.ToReadOnlyPosition();
            
            var currentRow = rows[ currentPosition.RowIndex ];

            //если текущий символ находится в самом конце строки
            var startOfTextPosition = currentPosition.ColumnIndex + 1;
            if( currentRow.Length == startOfTextPosition )
                throw new Exception( $"Unexpected end of line, second quote is expected to be at " +
                                     $"the same line at the position {position}" );
            
            var secondQuotePos = currentRow.IndexOf( _QUOTE_SYMBOL, startOfTextPosition );
            if( secondQuotePos == -1 )
                throw new Exception( "Unexpected end of line, second quote is expected to be at " +
                                     $"the same line at the position {position}" );

            if( startOfTextPosition == secondQuotePos )
                throw new Exception( $"Text inside quotes at the position {position} must not be " +
                                     $"empty. In case of empty argument consider not using the " +
                                     $"argument at all." );
            var text = currentRow.Substring( startOfTextPosition, secondQuotePos - startOfTextPosition );

            currentPosition.ColumnIndex = secondQuotePos + 1;
            
            return new Lexeme( text, LexemeType.QuotedText, position );
        }

        public override bool CanStartProcessing( char symbol ) => symbol == _QUOTE_SYMBOL;
    }
}