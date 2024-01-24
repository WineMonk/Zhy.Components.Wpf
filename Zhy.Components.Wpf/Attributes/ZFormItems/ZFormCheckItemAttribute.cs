using Zhy.Components.Wpf.Attributes.Bases;

namespace Zhy.Components.Wpf.Attributes.ZFormItems
{
    /// <summary>
    /// 选择框表单项
    /// </summary>
    /// <example>
    /// <code>
    /// [ZFormCheckItem("选 择")]
    /// public bool IsChecked { get => _isChecked; set => SetProperty(ref _isChecked, value); }
    /// </code>
    /// </example>
    public class ZFormCheckItemAttribute : ZFormItemAttribute
    {
        /// <summary>
        /// 选择框表单项
        /// </summary>
        /// <param name="title">标题</param>
        public ZFormCheckItemAttribute(string title) : base(title) { }
    }
}
