namespace Cindy.UI.Binder
{
    /// <summary>
    /// 绑定器接口
    /// </summary>
    public interface IBinder
    {
        /// <summary>
        /// 绑定数据。（输入）
        /// </summary>
        void Bind();
        /// <summary>
        /// 更新数据。（输出）
        /// </summary>
        void Apply();
    }
}
