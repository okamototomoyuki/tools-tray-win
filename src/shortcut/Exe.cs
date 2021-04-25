using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace toolstray
{
    /// <summary>
    /// ツール実行
    /// </summary>
    class Exe : IShortcut
    {
        [DllImport("user32.dll")]
        static extern int SetForegroundWindow(IntPtr hwnd);

        string _cmd;
        bool _isDuply;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="cmd">コマンド、ツールのパスでもよい</param>
        /// <param name="isDuply">true:重複起動する</param>
        public Exe(string cmd, bool isDuply = true)
        {
            _cmd = cmd;
            _isDuply = isDuply;
        }

        /// <summary>
        /// 実行
        /// </summary>
        public void Run()
        {
            if (_isDuply)
            {
                Process.Start(_cmd);
            }
            else
            {
                var processList = Process.GetProcesses();

                foreach (var p in processList)
                {
                    if (p.ProcessName == Path.GetFileNameWithoutExtension(_cmd))
                    {
                        Console.Write(p.ProcessName);
                        SetForegroundWindow(p.MainWindowHandle);
                        return;
                    }
                }

                Process.Start(_cmd);
            }
        }
    }
}
