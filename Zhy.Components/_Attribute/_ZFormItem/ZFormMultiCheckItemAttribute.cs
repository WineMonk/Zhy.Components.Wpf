/****************************************
 * FileName:	ZFormMultiCheckItemAttribute
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 9:58:42
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
    public class ZFormMultiCheckItemAttribute: ZFormItemAttribute
    {
        public ZFormMultiCheckItemAttribute(string title, string memberPath, string contentProperty) : base(title)
        {
            MemberPath = memberPath;
            ContentProperty = contentProperty;
        }

        public string ContentProperty { get; set; }
    }
}
