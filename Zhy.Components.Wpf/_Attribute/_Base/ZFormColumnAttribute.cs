/****************************************
 * FileName:	ZFormColumnAttribute
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 10:20:32
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
    public class ZFormColumnAttribute : ZFormItemAttribute, IZFormColumn
    {
        public double Width { get; set; }
        public DataGridLengthUnitType WidthUnit { get; set; }
        public bool IsSearchProperty { get; set; }
    }
}
