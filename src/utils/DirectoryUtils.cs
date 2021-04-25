using System.IO;

namespace toolstray
{
    /// <summary>
    /// Directory ユーティリティ 
    /// </summary>
    public static class DirectoryUtils
    {
        /// <summary>
        /// ディレクトリがあれば削除
        /// </summary>
        /// <param name="path">ディレクトリパス</param>
        public static void DeleteIfExists(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        /// <summary>
        /// ディレクトリが無ければ作成
        /// </summary>
        /// <param name="path">ディレクトリパス</param>
        public static void CreateIfNotExists(string path)
        {
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}