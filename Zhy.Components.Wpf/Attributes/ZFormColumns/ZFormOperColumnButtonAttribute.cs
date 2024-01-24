using Zhy.Components.Wpf.Attributes.Bases;

namespace Zhy.Components.Wpf.Attributes.ZFormColumns
{
    /// <summary>
    /// 表单操作列功能按钮
    /// </summary>
    /// <example>
    /// <code>
    /// [ZFormTextButtonColumn("档案路径", Index = 5, ButtonContent = "更 改", RelayCommandName = nameof(CommandModifyArchivesPath), Width = 200, WidthUnit = DataGridLengthUnitType.Pixel)]
    /// public string ArchivesPath { get => _archivesPath; set => SetProperty(ref _archivesPath, value); }
    /// </code>
    /// </example>
    public class ZFormOperColumnButtonAttribute : ZFormFuncButtonAttribute
    {
        /// <summary>
        /// 表单操作列功能按钮
        /// </summary>
        /// <param name="buttonContent">按钮标题</param>
        public ZFormOperColumnButtonAttribute(string buttonContent) : base(buttonContent)
        {

        }
    }
}
