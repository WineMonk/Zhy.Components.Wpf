using System.Windows.Controls;
using Zhy.Components.Wpf.Attributes.Interfaces;
using Zhy.Components.Wpf.Attributes.ZFormItems;

namespace Zhy.Components.Wpf.Attributes.ZFormColumns
{
    /// <summary>
    /// 多项选择框表单列
    /// </summary>
    /// <example>
    /// <code>
    /// [ZFormMultiCheckColumn("权 限", "IsChecked", "Name")]
    /// public List&lt;Permission&gt; Permission { get => _permission; set => SetProperty(ref _permission, value); }
    /// </code>
    /// </example>
    public class ZFormMultiCheckColumnAttribute : ZFormMultiCheckItemAttribute, IZFormColumn
    {
        /// <summary>
        /// 多项选择框表单列
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="memberPath">成员路径</param>
        /// <param name="contentProperty">显示内容属性名</param>
        public ZFormMultiCheckColumnAttribute(string title, string memberPath, string contentProperty) :
            base(title, memberPath, contentProperty)
        { }
        /// <summary>
        /// 列宽度
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// 列宽度单位
        /// </summary>
        public DataGridLengthUnitType WidthUnit { get; set; }
        /// <summary>
        /// 是否为查询列
        /// 查询时查询该列的值，则为true，否则为false。
        /// </summary>
        public bool IsSearchProperty { get; set; }
        /// <summary>
        /// 是否为只读列，默认false
        /// </summary>
        public bool IsReadOnlyColumn { get; set; }
        /// <summary>
        /// 作为表单项时是否隐藏此项，默认false
        /// </summary>
        public bool IsHideFormItem { get; set; }
        /// <summary>
        /// 作为表单列时是否隐藏此项，默认false
        /// </summary>
        public bool IsHideFormColumn { get; set; }
    }
}
