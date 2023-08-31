/****************************************
 * FileName:	ZFormTextButtonItemAttribute
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 9:56:48
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

namespace Zhy.Components._Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public class ZFormTextButtonItemAttribute : ZFormTextItemAttribute
    {
        public ZFormTextButtonItemAttribute(string title) : base(title)
        {
        }

        public string RelayCommandName { get; set; }
        public string ButtonContent { get; set; }
        public ZFormButtonStyle ButtonStyle { get; set; }
    }
}
