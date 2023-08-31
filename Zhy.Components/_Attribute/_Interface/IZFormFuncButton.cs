/****************************************
 * FileName:	IZFormFuncButton
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 11:05:58
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
    public interface IZFormFuncButton : IZFormSortItem
    {
        string ButtonContent { get; set; }
        ZFormButtonStyle ButtonStyle { get; set; }
    }
}
