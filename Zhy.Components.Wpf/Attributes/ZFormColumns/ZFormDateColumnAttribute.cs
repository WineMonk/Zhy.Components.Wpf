using System.Windows.Controls;
using Zhy.Components.Wpf.Attributes.Interfaces;
using Zhy.Components.Wpf.Attributes.ZFormItems;

namespace Zhy.Components.Wpf.Attributes.ZFormColumns
{
    /// <summary>
    /// 日期表单列
    /// </summary>
    /// <example>
    /// <code>
    /// [ZFormDateColumn("创建日期")]
    /// public string CreateDate { get => _createDate; set => SetProperty(ref _createDate, value); }
    /// </code>
    /// </example>
    public class ZFormDateColumnAttribute : ZFormDateItemAttribute, IZFormColumn
    {
        /// <summary>
        /// 日期表单列
        /// </summary>
        /// <param name="title">列标题</param>
        public ZFormDateColumnAttribute(string title) : base(title)
        {
        }

        /// <summary>
        /// 列宽度
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// 列宽度单位
        /// </summary>
        public DataGridLengthUnitType WidthUnit { get; set; } = DataGridLengthUnitType.Auto;
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
