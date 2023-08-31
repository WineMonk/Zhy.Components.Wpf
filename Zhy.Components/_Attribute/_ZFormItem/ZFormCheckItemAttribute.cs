/****************************************
 * FileName:	ZFormCheckItemAttribute
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 9:55:54
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
    /// 选择框表单项
    /// </summary>
    public class ZFormCheckItemAttribute : ZFormItemAttribute
    {
        /// <summary>
        /// 选择框表单项
        /// </summary>
        /// <param name="title">标题</param>
        public ZFormCheckItemAttribute(string title) : base(title) { }
    }
}
