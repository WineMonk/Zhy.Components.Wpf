/****************************************
 * FileName:	IZFormColumn
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 10:09:01
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

namespace Zhy.Components.Wpf._Attribute
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IZFormColumn : IZFormSortItem
    {
        string Title { get; set; }
        string MemberPath { get; set; }
        double Width { get; set; }
        DataGridLengthUnitType WidthUnit { get; set; }
        bool IsReadOnlyColumn { get; set; }
        bool IsSearchProperty { get; set; }
        bool IsHideFormItem { get; set; }
        bool IsHideFormColumn { get; set; }
    }
}
