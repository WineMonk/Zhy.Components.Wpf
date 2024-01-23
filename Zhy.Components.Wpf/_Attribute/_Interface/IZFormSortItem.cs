/****************************************
 * FileName:	IZFormSortItem
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 11:10:50
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/

namespace Zhy.Components.Wpf._Attribute
{
    /// <summary>
    /// 可排序表单项接口
    /// </summary>
    internal interface IZFormItemSortable
    {
        int Index { get; set; }
    }
}
