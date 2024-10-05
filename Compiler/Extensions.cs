using System;
using System.Collections.Generic;
using ScenarioBasedVideoUploader.Compiler.CommandHandlers;

namespace ScenarioBasedVideoUploader.Compiler
{
    internal static class Extensions
    {
        internal static bool IsStartOfComment( this IReadOnlyList< string > rows, IPosition currentPosition )
        {
            if( rows[ currentPosition.RowIndex ].Length <= currentPosition.ColumnIndex + 1 )
                return false;

            return rows[ currentPosition.RowIndex ][ currentPosition.ColumnIndex     ] == '/' &&
                   rows[ currentPosition.RowIndex ][ currentPosition.ColumnIndex + 1 ] == '/';
        }

        internal static char GetSymbolAt( this IReadOnlyList< string > rows, IPosition currentPosition )
        {
            if( rows.Count <= currentPosition.RowIndex || rows[ currentPosition.RowIndex ].Length <= currentPosition.ColumnIndex )
                throw new Exception( $"Incorrect position: {currentPosition}" );

            return rows[ currentPosition.RowIndex ][ currentPosition.ColumnIndex ];
        }

        internal static bool IsEndOfLine( this IReadOnlyList< string > rows, IPosition currentPosition )
        {
            return rows[ currentPosition.RowIndex ].Length <= currentPosition.ColumnIndex;
        }

        internal static bool IsEndOfRows( this IReadOnlyList< string > rows, IPosition currentPosition )
        {
            return currentPosition.RowIndex >= rows.Count;
        }
        
        internal static void SkipEmptyLines( this IReadOnlyList< string > rows, IPosition currentPosition )
        {
            while(  !rows.IsEndOfRows( currentPosition ) && 
                   ( rows.IsEndOfLine( currentPosition ) || rows.IsStartOfComment( currentPosition ) ) )
                currentPosition.SwitchToNextLine();
        }

        internal static BaseCommandHandler.ArgumentInfo WithName( this LexemeType lexemeType, string name )
        {
            return new BaseCommandHandler.ArgumentInfo( lexemeType, name );
        }
        
        internal static string InContext( this string source, IContext context )
        {
            return context.GetInterpolated( source );
        }
    }
}