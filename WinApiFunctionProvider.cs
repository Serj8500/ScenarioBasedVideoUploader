using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ScenarioBasedVideoUploader
{
    /// Selenium не позволяет взаимодействовать с системным диалогом выбора файла
    ///
    /// Здесь через WinApi ищем окно с указанным заголовком, посылаем туда строку с именем файла и нажатием Enter
    /// отсюда: https://stackoverflow.com/questions/11256732/how-to-handle-windows-file-upload-using-selenium-webdriver
    ///
    /// Реализовано 2 способа:
    /// - напрямую через SendKeys.SendWait(text); - эмулируется посимвольная печать (долго), соответствует mode = 1 в SelectFileCommand
    /// - через помещение строки в буфер обмена и последующей вставкой через SendKeys.SendWait("^v"), соответствует mode = 0 в SelectFileCommand (дефолтный способ)
    public static class WinApiFunctionProvider
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string? lpClassName, string lpWindowName);

        public static void SendTextAndPressEnter( string windowTitle, string text )
        {
            var hWnd = FindWindow( null, windowTitle );
            if( hWnd == IntPtr.Zero )
                throw new Exception($"Can't find any window with the title: {windowTitle}");
            
            var setFocus = SetForegroundWindow(hWnd);
            if( !setFocus )
                throw new Exception($"Can't set focus to the window with the title: {windowTitle}");
            
            Thread.Sleep(200);
            SendKeys.SendWait(text);
            SendKeys.SendWait("{ENTER}");
        }

        public static void InsertTextUsingClipboardAndPressEnter(string windowTitle, string text )
        {
            var hWnd = FindWindow( null, windowTitle );
            if( hWnd == IntPtr.Zero )
                throw new Exception($"Can't find any window with the title: {windowTitle}");
            
            var previousText = Clipboard.ContainsText() ? Clipboard.GetText() : null;
            Clipboard.SetText( text );
            
            var setFocus = SetForegroundWindow(hWnd);
            if( !setFocus )
                throw new Exception($"Can't set focus to the window with the title: {windowTitle}");
            
            Thread.Sleep(200);
            SendKeys.SendWait("^v");
            SendKeys.SendWait("{ENTER}");
            
            if( previousText is not null )
                Clipboard.SetText( previousText );
            else
                Clipboard.Clear();
        }
    }
}