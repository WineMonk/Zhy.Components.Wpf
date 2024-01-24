using Zhy.Components.Wpf.Attributes.Bases;

namespace Zhy.Components.Wpf.Attributes.ZFormItems
{
    /// <summary>
    /// 多项选择框表单项
    /// </summary>
    /// <example>
    /// <code>
    /// [ZFormMultiCheckItem("权 限", "IsChecked", "Name")]
    /// public List&lt;Permission&gt; Permission { get => _permission; set => SetProperty(ref _permission, value); }
    /// </code>
    /// </example>
    public class ZFormMultiCheckItemAttribute : ZFormItemAttribute
    {
        /// <summary>
        /// 多项选择框表单项
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="memberPath">成员路径</param>
        /// <param name="contentProperty">显示内容属性名</param>
        public ZFormMultiCheckItemAttribute(string title, string memberPath, string contentProperty) : base(title)
        {
            MemberPath = memberPath;
            ContentProperty = contentProperty;
        }
        /// <summary>
        /// 显示内容属性名
        /// </summary>
        public string ContentProperty { get; set; }
    }
}
