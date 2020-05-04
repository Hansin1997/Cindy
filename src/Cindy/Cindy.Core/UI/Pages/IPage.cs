using Cindy.Logic;

namespace Cindy.UI.Pages
{
    /// <summary>
    /// 页面接口
    /// </summary>
    /// <typeparam name="P"></typeparam>
    public interface IPage<P> where P : IPage<P>
    {
        /// <summary>
        /// 获取页面上下文
        /// </summary>
        /// <returns></returns>
        Context GetContext();

        /// <summary>
        /// 页面初始化
        /// </summary>
        void OnPageStart();

        /// <summary>
        /// 页面结束
        /// </summary>
        void OnPageFinish();

        /// <summary>
        /// 页面失焦
        /// </summary>
        void OnPageBlur();

        /// <summary>
        /// 页面聚焦
        /// </summary>
        void OnPageFocus();

        /// <summary>
        /// 页面Update
        /// </summary>
        void OnPageUpdate();

        /// <summary>
        /// 页面FixedUpdate
        /// </summary>
        void OnPageFixedUpdate();

        /// <summary>
        /// 页面OnGUI
        /// </summary>
        void OnPageGUI();

        /// <summary>
        /// 结束页面
        /// </summary>
        void Finish();

        /// <summary>
        /// 单例模式显示页面
        /// </summary>
        /// <param name="context">上下文</param>
        void ShowSingleton(Context context);

        /// <summary>
        /// 单例模式显示页面
        /// </summary>
        void ShowSingleton();

        /// <summary>
        /// 显示页面
        /// </summary>
        /// <param name="context">上下文</param>
        void Show(Context context);

        /// <summary>
        /// 显示页面
        /// </summary>
        void Show();

        /// <summary>
        /// 显示页面并获取实例
        /// </summary>
        /// <typeparam name="T">页面类型</typeparam>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        T ShowAndReturn<T>(Context context = null) where T : P;

        /// <summary>
        /// 判断页面是否激活
        /// </summary>
        /// <returns></returns>
        bool IsActive();
    }
}
