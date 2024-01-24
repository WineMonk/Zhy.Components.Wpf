using Zhy.Components.Wpf.Enums;

namespace Zhy.Components.Wpf.Attributes.Interfaces
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
