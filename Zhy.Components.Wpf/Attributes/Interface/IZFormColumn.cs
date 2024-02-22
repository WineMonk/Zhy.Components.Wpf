using System.Windows.Controls;

namespace Zhy.Components.Wpf.Attributes.Interfaces
{
    /// <summary>
    /// 表单列接口
    /// </summary>
    internal interface IZFormColumn : IZFormItemSortable
    {
        string Title { get; set; }
        string MemberPath { get; set; }
        double Width { get; set; }
        DataGridLengthUnitType WidthUnit { get; set; }
        bool IsReadOnlyColumn { get; set; }
        bool IsSearchProperty { get; set; }
        bool IsHideFormItem { get; set; }
        bool IsHideFormColumn { get; set; }
    }
}
