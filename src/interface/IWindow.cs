using System.Windows;

namespace toolstray
{
    /// <summary>
    /// インターフェース：ウインドウ機能
    /// </summary>
    public interface IWindow : IProc
    {
        Window win { get => (Window)this; }

        /// <summary>
        /// ウインドウ表示
        /// </summary>
        void ActiveShow()
        {
            win.Activate();
            win.Show();
            OnActive();
        }

        /// <summary>
        /// 表示時
        /// </summary>
        void OnActive()
        {
        }

        /// <summary>
        /// 非表示時
        /// </summary>
        void OnDeactive()
        {
        }
    }
}