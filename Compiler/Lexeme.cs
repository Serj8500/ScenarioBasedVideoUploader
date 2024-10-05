using System;

namespace ScenarioBasedVideoUploader.Compiler
{
    public enum LexemeType
    {
        Text,
        OpenRoundBracket,
        CloseRoundBracket,
        WhiteSpace,
        Comma,
        QuotedText,
        Integer,
        Semicolon
    }

    public interface ILexeme
    {
        LexemeType                  Type        { get; }
        string                      Text        { get; }
        IReadOnlyPosition           Position    { get; }
    }    

    internal class Lexeme : ILexeme
    {
        public string               Text        { get; }
        public LexemeType           Type        { get; }
        public IReadOnlyPosition    Position    { get; }
        
        public Lexeme( string text, LexemeType type, IReadOnlyPosition position )
        {
            if( string.IsNullOrEmpty( text ) )
                throw new Exception( $"Passed parameter {nameof(text)} can't be null or empty" );
            
            Text     = text;
            Type     = type;
            Position = position ?? throw new Exception( $"Passed parameter {nameof(position)} can't be null or empty" );
        }
        
        public override string ToString() => $"({Type}:{Text} at {Position}))";
    }
}