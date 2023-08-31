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
using Zhy.Components._Enum;

namespace Zhy.Components._Attribute._Base
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
    }
}
