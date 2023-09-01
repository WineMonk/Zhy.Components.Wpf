/****************************************
 * FileName:	ZFormTextColumnAttribute
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 11:21:01
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Zhy.Components.Wpf._Attribute._ZFormItem;

namespace Zhy.Components.Wpf._Attribute._ZFormColumn
{
    /// <summary>
    /// 输入框表单列
    /// </summary>
    public class ZFormTextColumnAttribute : ZFormTextItemAttribute, IZFormColumn
    {
        /// <summary>
        /// 输入框表单列
        /// </summary>
        /// <param name="title">标题</param>
        public ZFormTextColumnAttribute(string title) : base(title) { }
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
        /// 是否为只读列
        /// </summary>
        public bool IsReadOnlyColumn { get; set; }
        /// <summary>
        /// 作为表单项时是否隐藏此项
        /// </summary>
        public bool IsHideFormItem { get; set; }
    }
}
