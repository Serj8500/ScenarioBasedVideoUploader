using System.Collections.Generic;

namespace ScenarioBasedVideoUploader.Compiler
{
    internal static class LexemeConstants
    {
        private static readonly Dictionary< LexemeType, char > _lexemeType2SingleCharacter = new()
        {
            [ LexemeType.Comma             ] = ',',
            [ LexemeType.CloseRoundBracket ] = ')',
            [ LexemeType.OpenRoundBracket  ] = '(',
            [ LexemeType.Semicolon         ] = ';',
        };

        public static IReadOnlyDictionary< LexemeType, char > LexemeType2SingleCharacter => _lexemeType2SingleCharacter;
    }
}