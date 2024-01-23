/****************************************
 * FileName:	IZFormFuncButton
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 11:05:58
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using Zhy.Components.Wpf._Enum;

namespace Zhy.Components.Wpf._Attribute
{
    /// <summary>
    /// 表单功能按钮接口
    /// </summary>
    internal interface IZFormFuncButton : IZFormItemSortable
    {
        string ButtonContent { get; set; }
        ZFormButtonStyle ButtonStyle { get; set; }
    }
}
