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

namespace Zhy.Components._Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public class ZFormTextColumnAttribute : ZFormTextItemAttribute, IZFormColumn
    {
        public ZFormTextColumnAttribute(string title) : base(title)
        {
        }

        public double Width { get; set; } = 1;
        public DataGridLengthUnitType WidthUnit { get; set; } = DataGridLengthUnitType.Star;
        public bool IsSearchProperty { get; set; }
        public bool IsReadOnlyColumn { get; set; }
        public bool IsHideFormItem { get; set; }
    }
}
