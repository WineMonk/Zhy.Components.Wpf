/****************************************
 * FileName:	ZFormFuncButtonAttribute
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 10:01:37
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
using Zhy.Components.Wpf._Enum;

namespace Zhy.Components.Wpf._Attribute._Base
{
    /// <summary>
    /// 表单功能按钮特性
    /// </summary>
    public class ZFormFuncButtonAttribute : Attribute, IZFormFuncButton
    {
        /// <summary>
        /// 索引，用于排序
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 按钮内容
        /// </summary>
        public string ButtonContent { get; set; }
        /// <summary>
        /// 按钮样式
        /// </summary>
        public ZFormButtonStyle ButtonStyle { get; set; } = ZFormButtonStyle.DefaultButton;
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
