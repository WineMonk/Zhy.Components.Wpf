using System;

namespace Zhy.Components.Wpf.Exceptions
{
    /// <summary>
    /// ZDataGrid控件错误
    /// </summary>
    [Serializable]
    public class ZDataGridException : Exception
    {
        /// <summary>
        /// ZDataGrid控件错误
        /// </summary>
        public ZDataGridException() : base() { }
        /// <summary>
        /// ZDataGrid控件错误
        /// </summary>
        /// <param name="message">错误信息</param>
        public ZDataGridException(string message) : base(message) { }
        /// <summary>
        /// ZDataGrid控件错误
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="inner">内部异常</param>
        public ZDataGridException(string message, Exception inner) : base(message, inner) { }
    }
}
