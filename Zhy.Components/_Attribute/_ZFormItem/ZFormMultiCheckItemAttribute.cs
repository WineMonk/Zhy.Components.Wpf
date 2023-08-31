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
    /// 多项选择框表单项
    /// </summary>
    public class ZFormMultiCheckItemAttribute: ZFormItemAttribute
    {
        /// <summary>
        /// 多项选择框表单项
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="memberPath">成员路径</param>
        /// <param name="contentProperty">显示内容属性名</param>
        public ZFormMultiCheckItemAttribute(string title, string memberPath, string contentProperty) : base(title)
        {
            MemberPath = memberPath;
            ContentProperty = contentProperty;
        }
        /// <summary>
        /// 显示内容属性名
        /// </summary>
        public string ContentProperty { get; set; }
    }
}
