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
using Zhy.Components.Wpf._Attribute._Base;

namespace Zhy.Components.Wpf._Attribute._ZFormItem
{
    /// <summary>
    /// 下拉列表框表单项
    /// </summary>
    public class ZFormComboItemAttribute : ZFormItemAttribute
    {
        /// <summary>
        /// 下拉列表框表单项
        /// </summary>
        /// <param name="title">标题</param>
        public ZFormComboItemAttribute(string title) : base(title) { }
        /// <summary>
        /// 数据项源属性名
        /// </summary>
        public string ItemsSourceProperty { get; set; }
    }
}
