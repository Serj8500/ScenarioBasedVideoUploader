using System;
using System.Collections.Generic;

namespace ScenarioBasedVideoUploader.Compiler.CommandHandlers
{
    /// назначение: в уже открытый диалог выбора файла ввести имя файла и послать нажатие клавиши Enter
    /// параметры: название окна, имя файла
    public class SelectFileCommandHandler : BaseCommandHandler
    {
        public SelectFileCommandHandler() : base( "SelectFile", "Sends a file name into the opened system dialog and presses Enter (mode=0 - using clipboard, mode=1 - using SendKeys.SendWait(text))", 
            LexemeType.QuotedText.WithName("fileName"), LexemeType.QuotedText.WithName("mode_0_or_1") )
        {
        }

        protected override ICommand CreateCommand( IReadOnlyList< ILexeme > lexemes )
        {
            return new SelectFileCommand( CommandName, lexemes[ 2 ].Text.Trim('"'), lexemes[ 4 ].Text.Trim('"') );
        }
    }

    public class SelectFileCommand : BaseCommand, ICommand
    {
        private readonly string _fileName;
        private readonly string _mode;

        public SelectFileCommand( string commandName, string fileName, string mode ) : base( commandName )
        {
            if( string.IsNullOrEmpty( fileName ) )
                throw new ArgumentException($"{nameof(fileName)} can't be null or empty");
            
            _fileName       = fileName;
            _mode           = mode;
        }

        public void Process( IContext context )
        {
            var windowTitle = context.GetWindowTitleForCurrentBrowser();
            
            switch( _mode )
            {
                case "1":
                    WinApiFunctionProvider.SendTextAndPressEnter( windowTitle, _fileName.InContext( context ) );
                    break;
                default:
                    var expectedMode = "0"; 
                    if( _mode != expectedMode )
                        Console.WriteLine($"Unexpected value for the parameter 'mode': '{_mode}'. Continue processing with the default value: {expectedMode} instead.");
                    
                    WinApiFunctionProvider.InsertTextUsingClipboardAndPressEnter( windowTitle, _fileName.InContext( context ) );
                    break;
            }
        }
        
        public override string[] GetArgumentValues()
        {
            return new []{ _fileName, _mode };
        }
    }
}