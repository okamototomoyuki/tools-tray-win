using System.IO;

namespace toolstray
{
    /// <summary>
    /// File ユーティリティ 
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// ファイルがあれば削除
        /// </summary>
        /// <param name="path">ファイルパス</param>
        public static void DeleteIfExists(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}