/****************************************
 * FileName:	ZFormOperateColumnButton
 * Creater: 	shaozhy
 * Create Date:	2023/9/25 11:17:04
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

namespace Zhy.Components.Wpf._Attribute._ZFormColumn
{
    /// <summary>
    /// 表单操作列功能按钮
    /// </summary>
    public class ZFormOperColumnButtonAttribute : ZFormFuncButtonAttribute
    {
        /// <summary>
        /// 表单操作列功能按钮
        /// </summary>
        /// <param name="buttonContent">按钮标题</param>
        public ZFormOperColumnButtonAttribute(string buttonContent) : base(buttonContent)
        {

        }
    }
}
