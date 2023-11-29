/****************************************
 * FileName:	IZFormItem
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 10:07:14
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/

namespace Zhy.Components.Wpf._Attribute
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IZFormItem : IZFormSortItem
    {
        string Title { get; set; }
        string MemberPath { get; set; }
        bool IsReadOnlyItem { get; set; }
    }
}
