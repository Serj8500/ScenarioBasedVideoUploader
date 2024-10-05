using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBasedVideoUploader.Compiler
{
    internal interface IBaseHandler
    {
        ILexeme            Process             ( IReadOnlyList< string > rows, IPosition currentPosition );
        bool               CanStartProcessing  ( char symbol );
    }
    
    internal abstract class BaseHandler : IBaseHandler
    {
        public abstract LexemeType Type { get ;}

        //игнорировать комментарии автоматически: всё что идёт после двух слешей - пропускается до конца строки
        internal string Read( IReadOnlyList<string> rows, IPosition currentPosition, Func<char, bool> isAcceptableCharFunc )
        {
            var stringBuilder = new StringBuilder();

            var isEndOfRows = rows.IsEndOfRows( currentPosition );

            while( !isEndOfRows && ( rows.IsEndOfLine( currentPosition ) || rows.IsStartOfComment( currentPosition ) ))
            {
                currentPosition.SwitchToNextLine();
                isEndOfRows = rows.IsEndOfRows( currentPosition );
            }

            if( !isEndOfRows )
            {
                var currentSymbol = rows.GetSymbolAt( currentPosition );

                while( isAcceptableCharFunc( currentSymbol ) )
                {
                    stringBuilder.Append( currentSymbol );
                    
                    currentPosition.GoToNextSymbol();
                    if( rows.IsEndOfLine( currentPosition ) )
                        break;
                    
                    currentSymbol = rows.GetSymbolAt( currentPosition );
                }
            }
            
            var result = stringBuilder.ToString();
            
            if( string.IsNullOrEmpty( result ) )
                throw new Exception( $"There were no symbols read for {Type} at the position: {currentPosition}" );
            
            return result;
        }

        public abstract ILexeme Process( IReadOnlyList< string > rows, IPosition currentPosition );
        public abstract bool CanStartProcessing( char symbol );
    }
}