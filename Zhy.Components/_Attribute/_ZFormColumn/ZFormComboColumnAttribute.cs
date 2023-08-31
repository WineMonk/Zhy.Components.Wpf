/****************************************
 * FileName:	ZFormComboColumnAttribute
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 11:24:34
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
using Zhy.Components._Attribute._ZFormItem;

namespace Zhy.Components._Attribute._ZFormColumn
{
    /// <summary>
    /// 
    /// </summary>
    public class ZFormComboColumnAttribute:ZFormComboItemAttribute,IZFormColumn
    {
        public ZFormComboColumnAttribute(string title) : base(title)
        {
        }

        public double Width { get; set; } = 1;
        public DataGridLengthUnitType WidthUnit { get; set; } = DataGridLengthUnitType.Star;
        public bool IsSearchProperty { get; set; }
        public bool IsReadOnlyColumn { get; set; }
        public bool IsHideFormItem { get; set; }
    }
}
