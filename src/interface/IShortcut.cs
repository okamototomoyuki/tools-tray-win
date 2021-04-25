namespace toolstray
{
    /// <summary>
    /// インターフェース：ショートカット機能
    /// </summary>
    interface IShortcut : IProc
    {
        /// <summary>
        /// 実行
        /// </summary>
        public void Run();
    }
}