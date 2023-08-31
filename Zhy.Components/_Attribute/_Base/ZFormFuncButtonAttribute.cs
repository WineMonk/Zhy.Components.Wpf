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
    /// 
    /// </summary>
    public class ZFormFuncButtonAttribute : Attribute, IZFormFuncButton
    {
        public int Index { get; set; }
        public string ButtonContent { get; set; }
        public ZFormButtonStyle ButtonStyle { get; set; } = ZFormButtonStyle.DefaultButton;
    }
}
