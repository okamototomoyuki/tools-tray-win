using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace toolstray
{
    public static class StringExtensions
    {
        /// <summary>
        /// 空か？
        /// </summary>
        /// <param name="s">文字列</param>
        /// <returns>true:空</returns>
        public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);

        /// <summary>
        /// 空ではないか？
        /// </summary>
        /// <param name="s">文字列</param>
        /// <returns>true:空ではない</returns>
        public static bool NotEmpty(this string s) => string.IsNullOrEmpty(s) == false;


        /// <summary>
        /// int に変換
        /// </summary>
        /// <param name="s">文字列</param>
        /// <param name="error">変換失敗時の値</param>
        /// <returns>int</returns>
        public static int ToInt(this string s, int error = -1) => int.TryParse(s, out int result) ? result : error;

        /// <summary>
        /// int に変換
        /// </summary>
        /// <param name="s">文字列</param>
        /// <param name="error">変換失敗時の値</param>
        /// <returns>int</returns>
        public static double ToDouble(this string s, double error = -1) => double.TryParse(s, out double result) ? result : error;

        /// <summary>
        /// 正規表現で置換
        /// </summary>
        /// <param name="s">文字列</param>
        /// <param name="regex">正規表現</param>
        /// <param name="replace">置換文字列</param>
        /// <returns>置換後の文字列</returns>
        public static string ReplaceAll(this string s, string regex, string replace) => Regex.Replace(s, regex, replace);

        /// <summary>
        /// 正規表現にマッチすれば true
        /// </summary>
        /// <param name="s">文字列</param>
        /// <param name="regex">正規表現</param>
        /// <returns>true: マッチ</returns>
        public static bool IsMatch(this string s, string regex) => Regex.IsMatch(s, regex);

        /// <summary>
        /// 全角数字を半角数字に変換する。
        /// 参考：https://trapemiya.hatenablog.com/entry/20160704/1467612534
        /// </summary>
        /// <param name="s">変換する文字列</param>
        /// <returns>変換後の文字列</returns>
        public static string ZenToHanNum(this string s) => Regex.Replace(s, "[０-９]", p => ((char)(p.Value[0] - '０' + '0')).ToString());

        /// <summary>
        /// 全角数字をパースする
        /// </summary>
        /// <param name="s">変換する文字列</param>
        /// <param name="error">変換失敗時の値</param>
        /// <returns>変換後の文字列</returns>
        public static double ParseZen(this string s, double error = -1) => s.ZenToHanNum().ReplaceAll("．", ".").ToDouble(error);
    }
}