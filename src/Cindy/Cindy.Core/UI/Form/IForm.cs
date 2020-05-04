namespace Cindy.UI.Form
{
    /// <summary>
    /// 表单接口。
    /// </summary>
    public interface IForm
    {
        /// <summary>
        /// 调教表单
        /// </summary>
        void Submit();
        /// <summary>
        /// 恢复表单。
        /// </summary>
        void Restore();
    }
}
