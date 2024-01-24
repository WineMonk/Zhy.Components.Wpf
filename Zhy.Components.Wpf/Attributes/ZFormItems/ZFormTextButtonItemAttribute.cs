using Zhy.Components.Wpf.Enums;

namespace Zhy.Components.Wpf.Attributes.ZFormItems
{
    /// <summary>
    /// 按钮选择框表单项
    /// </summary>
    /// <example>
    /// <code>
    /// [ZFormTextButtonItem("查看")]
    /// public RelayCommand&lt;Tuple&lt;object?, IList?&gt;&gt; CommandViewItem =&gt; new RelayCommand&lt;Tuple&lt;object?, IList?&gt;&gt;(ViewItem);
    /// </code>
    /// </example>
    public class ZFormTextButtonItemAttribute : ZFormTextItemAttribute
    {
        /// <summary>
        /// 按钮选择框表单项
        /// </summary>
        /// <param name="title">标题</param>
        public ZFormTextButtonItemAttribute(string title) : base(title) { }
        /// <summary>
        /// 中继命令属性名
        /// </summary>
        public string RelayCommandName { get; set; }
        /// <summary>
        /// 按钮内容
        /// </summary>
        public string ButtonContent { get; set; }
        /// <summary>
        /// 按钮样式
        /// </summary>
        public ZFormButtonStyle ButtonStyle { get; set; }
    }
}
