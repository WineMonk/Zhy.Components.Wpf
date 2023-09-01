/****************************************
 * FileName:	ZFromTextItemAttribute
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 9:50:22
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhy.Components.Wpf._Attribute._Base;

namespace Zhy.Components.Wpf._Attribute._ZFormItem
{
    /// <summary>
    /// 输入框表单项
    /// </summary>
    public class ZFormTextItemAttribute : ZFormItemAttribute
    {
        /// <summary>
        /// 输入框表单项
        /// </summary>
        /// <param name="title">标题</param>
        public ZFormTextItemAttribute(string title) : base(title) { }
    }
}
