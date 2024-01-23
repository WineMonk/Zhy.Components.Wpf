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
    /// 表单项接口
    /// </summary>
    internal interface IZFormItem : IZFormItemSortable
    {
        string Title { get; set; }
        string MemberPath { get; set; }
        bool IsReadOnlyItem { get; set; }
    }
}
