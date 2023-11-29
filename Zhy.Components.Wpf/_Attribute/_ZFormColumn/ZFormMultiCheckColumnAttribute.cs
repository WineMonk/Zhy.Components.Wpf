/****************************************
 * FileName:	ZFormMultiCheckColumnAttribute
 * Creater: 	shaozhy
 * Create Date:	2023/9/4 15:08:12
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using System.Windows.Controls;
using Zhy.Components.Wpf._Attribute._ZFormItem;

namespace Zhy.Components.Wpf._Attribute._ZFormColumn
{
    /// <summary>
    /// 多项选择框表单列
    /// </summary>
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
        /// 是否为只读列
        /// </summary>
        public bool IsReadOnlyColumn { get; set; }
        /// <summary>
        /// 作为表单项时是否隐藏此项
        /// </summary>
        public bool IsHideFormItem { get; set; }
        /// <summary>
        /// 作为表单列时是否隐藏此项
        /// </summary>
        public bool IsHideFormColumn { get; set; }
    }
}
