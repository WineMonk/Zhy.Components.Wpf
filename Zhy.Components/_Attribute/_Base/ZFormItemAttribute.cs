/****************************************
 * FileName:	ZItemAttribute
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 9:48:27
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

namespace Zhy.Components._Attribute._Base
{
    /// <summary>
    /// 
    /// </summary>
    public class ZFormItemAttribute : Attribute, IZFormItem
    {
        public int Index { get; set; }
        public string Title { get; set; }
        public string MemberPath { get; set; }
        public bool IsReadOnlyItem { get; set; }
        public object Data { get; set; }

        public ZFormItemAttribute(string title)
        {
            Title = title;
        }
    }
}
