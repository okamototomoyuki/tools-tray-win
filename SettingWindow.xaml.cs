using System;
using System.Windows;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;

namespace toolstray
{
    /// <summary>
    /// 設定画面
    /// </summary>
    public partial class SettingWindow : Window, IWindow
    {
        public static SettingWindow ins;
        static Dictionary<Key, IProc> _parsed = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SettingWindow()
        {
            ins = this;
            InitializeComponent();

            // 設定ファイル読み込み
            textBox.Text = _ReadSCSettings();
        }

        /// <summary>
        /// デフォルト値設定
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">データ</param>
        void _Default(object sender, RoutedEventArgs e)
        {
            textBox.Text = _default;
        }


        /// <summary>
        /// ショートカットをパースした内容取得
        /// </summary>
        public static Dictionary<Key, IProc> Parsed
        {
            get
            {
                if (_parsed == null)
                {
                    _parsed = new Dictionary<Key, IProc>();

                    var iWinType = typeof(IWindow);
                    var winTypes = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(p => iWinType.IsAssignableFrom(p))
                        .ToList();
                    var iScType = typeof(IShortcut);
                    var scTypes = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(p => iScType.IsAssignableFrom(p))
                        .ToList();

                    var fileContent = _ReadSCSettings();
                    var lines = fileContent.Split('\n');
                    foreach (var line in File.ReadAllLines(AppDefines.PATH_SHORTCUT))
                    {
                        var modLine = line.ReplaceAll("\\s+", " ");
                        var eles = modLine.Split(' ');
                        if (eles.Length >= 2)
                        {
                            var key = eles[0] switch
                            {
                                "esc" => Key.Escape,
                                ";" => Key.OemSemicolon,
                                "," => Key.OemComma,
                                "." => Key.OemPeriod,
                                string s => (Key)Enum.Parse(typeof(Key), s, true)
                            };

                            var procStr = eles[1];
                            var winType = winTypes.Where(e => e.Name == procStr).FirstOrDefault();
                            var scType = scTypes.Where(e => e.Name == procStr).FirstOrDefault();
                            if (winType != null)
                            {
                                object proc = null;
                                if (eles.Length >= 3)
                                {
                                    var args = eles[2..^0];

                                    proc = Activator.CreateInstance(winType, args);
                                }
                                else
                                {
                                    proc = Activator.CreateInstance(winType);
                                }
                                _parsed[key] = proc as IProc;
                            }
                            else if (scType != null)
                            {
                                _parsed[key] = Activator.CreateInstance(scType) as IProc;
                            }
                            else if (procStr.StartsWith("http"))
                            {
                                _parsed[key] = new WebViewWindow(procStr);
                            }
                            else
                            {
                                _parsed[key] = new Exe(procStr, eles.Length <= 2);
                            }
                        }
                    }
                }
                return _parsed;
            }
        }

        /// <summary>
        /// ショートカット設定取得
        /// </summary>
        /// <returns>ファイル内容</returns>
        static string _ReadSCSettings()
        {
            if (Directory.Exists(AppDefines.WK_PATH) == false)
            {
                Directory.CreateDirectory(AppDefines.WK_PATH);
            }
            if (File.Exists(AppDefines.PATH_SHORTCUT) == false)
            {
                File.WriteAllText(AppDefines.PATH_SHORTCUT, _default);
            }
            return File.ReadAllText(AppDefines.PATH_SHORTCUT);
        }

        /// <summary>
        /// キーマップのデフォルト値
        /// </summary>
        static string _default => @"esc SettingWindow
h GoogleWindow
i https://translate.google.co.jp/?hl=ja&sl=en&tl=ja&op=translate
u https://translate.google.co.jp/?hl=ja&sl=ja&tl=en&op=translate
";

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">データ</param>
        void _Save(object sender, RoutedEventArgs e)
        {
            var text = textBox.Text;
            File.WriteAllText(AppDefines.PATH_SHORTCUT, text);
        }
    }
}
