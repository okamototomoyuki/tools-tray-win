using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace toolstray
{
    public partial class GoogleWindow : Window, IWindow
    {
        public GoogleWindow()
        {
            InitializeComponent();
        }

        void OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var encQuery = WebUtility.UrlEncode(textBox.Text);
                textBox.Text = "";

                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "cmd";
                info.Arguments = $"/C start https://www.google.com/search?q={encQuery}";
                info.RedirectStandardOutput = true;
                info.RedirectStandardError = true;
                info.UseShellExecute = false;
                info.CreateNoWindow = true;

                Process process = new Process();
                process.StartInfo = info;
                process.EnableRaisingEvents = true;
                process.Start();

                this.WindowClosing();
            }
        }

        public void OnActive()
        {
            textBox.Focus();
        }
    }
}