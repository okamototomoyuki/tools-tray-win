namespace toolstray
{
    /// <summary>
    /// 定義
    /// </summary>
    public static class AppDefines
    {
        public const int SEC_BK_AFTER_WRITE = 5;
        public const string WK_PATH = "C:\\tools-tray";

        public const string KEY_CONSUMER = "fHzdrLyFEsrb7VY6nNKbo9URK";
        public const string SECRET_CONSUMER = "URShvBJ1vtRmg4TPVHTfWAkEE6mmqAbutBEHVujguYj6xmRYQ3";

        /********************
         * フォルダorパス名 *
        *********************/
        public const string NAME_SHORTCUT = "shortcut.txt";
        public const string NAME_ACCESS_TOKEN = "userToken.txt";
        public const string NAME_ACCESS_TOKENSECRET = "userSceret.txt";

        public const string NAME_USER_DIR = "userData";

        /********
         * パス *
         ********/
        public static readonly string PATH_SHORTCUT = $"{WK_PATH}\\{NAME_SHORTCUT}";
        public static readonly string PATH_ACCESS_TOKEN = $"{WK_PATH}\\{NAME_ACCESS_TOKEN}";
        public static readonly string PATH_ACCESS_TOKENSECRET = $"{WK_PATH}\\{NAME_ACCESS_TOKENSECRET}";

        public static readonly string PATH_USER_DIR = $"{WK_PATH}\\{NAME_USER_DIR}";
    }
}