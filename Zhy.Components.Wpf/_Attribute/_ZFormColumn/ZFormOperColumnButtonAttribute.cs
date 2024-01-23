/****************************************
 * FileName:	ZFormOperateColumnButton
 * Creater: 	shaozhy
 * Create Date:	2023/9/25 11:17:04
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using Zhy.Components.Wpf._Attribute._Base;

namespace Zhy.Components.Wpf._Attribute._ZFormColumn
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
