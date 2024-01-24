using System.Windows.Controls;
using Zhy.Components.Wpf.Attributes.Interfaces;
using Zhy.Components.Wpf.Attributes.ZFormItems;

namespace Zhy.Components.Wpf.Attributes.ZFormColumns
{
    /// <summary>
    /// 下拉列表框表单列
    /// </summary>
    /// <example>
    /// <code>
    /// [ZFormComboColumn("角 色", ItemsSourceProperty = nameof(Roles))]
    /// public string Role { get => _role; set => SetProperty(ref _role, value); }
    /// public List&lt;string&gt; Roles { get => _roles; set => _roles = value; }
    /// </code>
    /// </example>
    public class ZFormComboColumnAttribute : ZFormComboItemAttribute, IZFormColumn
    {
        /// <summary>
        /// 下拉列表框表单列
        /// </summary>
        /// <param name="title">标题</param>
        public ZFormComboColumnAttribute(string title) : base(title) { }
        /// <summary>
        /// 列宽度
        /// </summary>
        public double Width { get; set; } = 1;
        /// <summary>
        /// 列宽度单位
        /// </summary>
        public DataGridLengthUnitType WidthUnit { get; set; } = DataGridLengthUnitType.Star;
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
