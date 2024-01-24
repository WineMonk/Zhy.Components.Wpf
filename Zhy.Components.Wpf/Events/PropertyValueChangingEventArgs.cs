using System;

namespace Zhy.Components.Wpf.Events
{
    /// <summary>
    /// 属性值变更事件参数类
    /// </summary>
    public class PropertyValueChangingEventArgs : EventArgs
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 新值
        /// </summary>
        public object? NewValue { get; set; }
        /// <summary>
        /// 旧值
        /// </summary>
        public object? OldValue { get; set; }
        /// <summary>
        /// 取消变更
        /// </summary>
        public bool Cancel { get; set; } = false;
        /// <summary>
        /// 属性值变更事件参数
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="newValue">新值</param>
        /// <param name="oldValue">旧值</param>
        public PropertyValueChangingEventArgs(string propertyName, object? newValue, object? oldValue)
        {
            this.PropertyName = propertyName;
            this.NewValue = newValue;
            this.OldValue = oldValue;
        }
    }
}
