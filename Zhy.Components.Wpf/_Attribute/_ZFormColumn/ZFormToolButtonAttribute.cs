/****************************************
 * FileName:	ZFormToolButtonAttribute
 * Creater: 	shaozhy
 * Create Date:	2023/9/25 11:19:09
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
using Zhy.Components.Wpf._Attribute._Base;
using Zhy.Components.Wpf._Enum;

namespace Zhy.Components.Wpf._Attribute._ZFormColumn
{
    /// <summary>
    /// 表单操作列功能按钮
    /// </summary>
    public class ZFormToolButtonAttribute : ZFormFuncButtonAttribute
    {
        /// <summary>
        /// 表单操作列功能按钮
        /// </summary>
        /// <param name="buttonContent">按钮标题</param>
        public ZFormToolButtonAttribute(string buttonContent) : base(buttonContent)
        {
        }

        /// <summary>
        /// 停靠位置，默认左侧
        /// </summary>
        public Dock Dock { get; set; } = Dock.Left;
        /// <summary>
        /// 按钮位置，默认表单上方
        /// </summary>
        public ButtonLocation Location { get; set; } = ButtonLocation.Top;
    }
}
