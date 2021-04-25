using System.ComponentModel;
using System.Windows;

namespace toolstray
{
    public static class WindowExtensions
    {
        public static void WindowClosing(this Window window, object? sender = null, CancelEventArgs? e = null)
        {
            //閉じるのをキャンセルする
            if (e != null)
                e.Cancel = true;

            //ウィンドウを非可視にする
            window.Visibility = Visibility.Collapsed;

            IWindow iwin = window as IWindow;
            iwin.OnDeactive();
        }

        public static void ShowWindow(this Window win)
        {
            win.Visibility = Visibility.Visible;
            win.WindowState = WindowState.Normal;
        }

    }
}