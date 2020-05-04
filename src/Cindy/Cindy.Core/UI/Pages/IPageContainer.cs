using Cindy.Logic;

namespace Cindy.UI.Pages
{
    /// <summary>
    /// 页面容器接口
    /// </summary>
    public interface IPageContainer<P> where P : IPage
    {
        /// <summary>
        /// 将一个页面入栈
        /// </summary>
        /// <param name="page">被入栈的页面</param>
        /// <param name="context">页面上下文</param>
        void Push(P page, Context context = null);

        /// <summary>
        /// 弹出一个页面
        /// </summary>
        /// <returns>弹出的页面</returns>
        P Pop();

        /// <summary>
        /// 加载一个页面并将其入栈
        /// </summary>
        /// <param name="name">页面资源名称</param>
        void Load(string name);

        /// <summary>
        /// 加载一个页面并将其入栈
        /// </summary>
        /// <param name="name">页面资源名称</param>
        /// <param name="context">页面上下文</param>
        /// <returns>页面实例</returns>
        P LoadPage(string name, Context context = null);

        /// <summary>
        /// 加载一个页面并将其入栈
        /// </summary>
        /// <typeparam name="T">页面类型</typeparam>
        /// <param name="name">页面资源名称</param>
        /// <param name="context">页面上下文</param>
        /// <returns>页面实例</returns>
        T LoadPage<T>(string name, Context context = null) where T : P;

        /// <summary>
        /// 结束一个页面并将其出栈
        /// </summary>
        /// <param name="page"></param>
        void FinishPage(P page);
    }
}