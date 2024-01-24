namespace Zhy.Components.Wpf.Attributes.Interfaces
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
