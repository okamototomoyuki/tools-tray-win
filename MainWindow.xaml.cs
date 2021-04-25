using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Win32;

namespace toolstray
{
    public partial class MainWindow : Window
    {
        static readonly Dictionary<Key, IProc> procs = new Dictionary<Key, IProc>
        {
            { Key.H, new GoogleWindow()},
            { Key.Escape, new SettingWindow()},
        };

        HotKeyHelper _hotkey;
        NotifyIcon _notifyIcon;

        public MainWindow()
        {
            InitializeComponent();

            RegistryKey regkey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced");
            regkey.SetValue("DisabledHotkeys", string.Join("", procs.Select(e => e.Key.ToString()).Where(e => e.Length == 1).ToList()));
            regkey.Close();

            _hotkey = new HotKeyHelper(this);
            foreach (var e in SettingWindow.Parsed)
            {
                var key = e.Key;
                var proc = e.Value;
                if (proc is IWindow win)
                {
                    win.win.ShowInTaskbar = false;
                    _hotkey.Register(ModifierKeys.Alt | ModifierKeys.Windows, key, (_, __) =>
                    {
                        SwitchWindow(win);
                    });
                }
                else if (proc is IShortcut sc)
                {
                    _hotkey.Register(ModifierKeys.Alt | ModifierKeys.Windows, key,
                    (_, __) =>
                    {
                        sc.Run();
                    });
                }
            }

            //タスクバーに表示されないようにする
            ShowInTaskbar = false;

            //タスクトレイアイコンを初期化する
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Text = "タスクトレイサンプル";
            _notifyIcon.Icon = System.Drawing.SystemIcons.Application;

            //タスクトレイに表示する
            _notifyIcon.Visible = true;

            //アイコンにコンテキストメニュー「終了」を追加する
            var menuStrip = new ContextMenuStrip();

            var exitItem = new ToolStripMenuItem();
            exitItem.Text = "終了";
            menuStrip.Items.Add(exitItem);
            exitItem.Click += new EventHandler(exitItem_Click);

            _notifyIcon.ContextMenuStrip = menuStrip;

            // WebView は先に読み込んでおく
            foreach (var e in SettingWindow.Parsed)
            {
                var proc = e.Value;
                if (proc is IWindow win and WebViewWindow)
                {
                    win.ActiveShow();
                    win.win.WindowClosing();
                }
            }
        }

        //終了メニューのイベントハンドラ
        void exitItem_Click(object? sender, EventArgs e)
        {
            _notifyIcon.Dispose();
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// ウインドウ切替
        /// </summary>
        /// <param name="w">ウインドウ</param>
        public static void SwitchWindow(IWindow w)
        {
            foreach (var v in SettingWindow.Parsed.Values)
            {
                if (v is IWindow win)
                {
                    if (v == w)
                    {
                        win.ActiveShow();
                    }
                    else
                    {
                        win.win.WindowClosing();
                    }
                }
            }
        }
    }
}