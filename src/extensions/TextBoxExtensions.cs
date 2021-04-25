using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace toolstray
{
    /// <summary>
    /// TextBox 拡張 
    /// </summary>
    public static class TextBoxExtensions
    {
        /// <summary>
        /// ショートカット実行
        /// </summary>
        /// <param name="textBox">テキストボックス</param>
        /// <param name="e">イベント</param>
        public static void Shortcut(this TextBox textBox, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) && e.Key == Key.X)
            {
                if (textBox.SelectionLength == 0)
                {
                    var text = textBox.Text;
                    var index = textBox.CaretIndex;
                    if (text.Length == 0)
                        return;

                    var start = index;
                    var end = index;
                    Debug.WriteLine((index, text.Length));
                    if (index == text.Length || text[index] == '\n' || text[index] == '\r')
                    {
                        if (index != text.Length)
                            end += text[index] == '\r' ? 2 : 1;

                        if (start > 0)
                        {
                            start--;
                            while (true)
                            {
                                if (start <= 0)
                                    break;
                                else if (text[start] == '\n')
                                {
                                    start++;
                                    break;
                                }

                                start--;
                            }
                        }
                    }
                    else
                    {
                        while (true)
                        {
                            if (end == text.Length)
                                break;
                            else if (text[end] == '\n')
                            {
                                end++;
                                break;
                            }
                            end++;
                        }
                        while (true)
                        {
                            if (start == 0)
                                break;
                            else if (text[start] == '\n')
                            {
                                start++;
                                break;
                            }

                            start--;
                        }
                    }
                    var length = end - start;

                    textBox.Select(start, length);
                }
            }
        }
    }
}