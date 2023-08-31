/****************************************
 * FileName:	ZFormComboItemAttribute
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 9:55:13
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhy.Components._Attribute._Base;

namespace Zhy.Components._Attribute._ZFormItem
{
    /// <summary>
    /// 
    /// </summary>
    public class ZFormComboItemAttribute : ZFormItemAttribute
    {
        public ZFormComboItemAttribute(string title) : base(title)
        {
        }

        public string ItemsSourceProperty { get; set; }
    }
}
