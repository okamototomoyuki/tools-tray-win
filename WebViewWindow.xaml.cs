using Microsoft.Web.WebView2.Core;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace toolstray
{
    /// <summary>
    /// WebView表示
    /// </summary>
    public partial class WebViewWindow : Window, IWindow
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WebViewWindow() : this("https://google.com")
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="url">URL</param>
        public WebViewWindow(string url)
        {
            InitializeComponent();
            Loaded += async (_, __) => await _Routine(url);
        }

        /// <summary>
        /// 非同期処理
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>タスク</returns>
        async Task _Routine(string url)
        {
            // 最新のフォルダ検索
            var userPath = AppDefines.PATH_USER_DIR;
            var op = new CoreWebView2EnvironmentOptions("--disable-web-security");
            var env = await CoreWebView2Environment.CreateAsync(userDataFolder: userPath, options: op);
            await webView.EnsureCoreWebView2Async(env);

            webView.WebMessageReceived += _OnReceiveMsg;
            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri? result))
            {
                webView.Source = result;
            }
        }

        /// <summary>
        /// 活性時
        /// </summary>
        public void OnActive()
        {
            webView.ExecuteScriptAsync("window.OnActive()");
        }

        /// <summary>
        /// メッセージ受信
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="arg">データ</param>
        void _OnReceiveMsg(object? sender, CoreWebView2WebMessageReceivedEventArgs arg)
        {
            var msg = arg.TryGetWebMessageAsString();
            var datas = msg.Split(':');
            if (datas.Length > 0)
            {
                var key = datas[0];
                switch (key)
                {
                    case "google" when datas.Length > 1:
                        // Google 検索
                        var query = datas[1];
                        var encQuery = WebUtility.UrlEncode(query);
                        var info = new ProcessStartInfo
                        {
                            FileName = "cmd",
                            Arguments = $"/C start https://www.google.com/search?q={encQuery}",
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                        };
                        var process = new Process
                        {
                            StartInfo = info,
                            EnableRaisingEvents = true,
                        };
                        process.Start();

                        this.WindowClosing();
                        break;
                }
            }
        }
    }
}