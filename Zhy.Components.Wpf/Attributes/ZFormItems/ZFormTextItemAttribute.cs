using Zhy.Components.Wpf.Attributes.Bases;

namespace Zhy.Components.Wpf.Attributes.ZFormItems
{
    /// <summary>
    /// 输入框表单项
    /// </summary>
    /// <example>
    /// <code>
    /// [ZFormTextColumn("姓 名", Index = 1, IsHideFormColumn = false, IsReadOnlyColumn = false, IsHideFormItem = false, IsReadOnlyItem = false,IsSearchProperty = true, MemberPath = null, Width = 100, WidthUnit = DataGridLengthUnitType.Pixel)]
    /// public string Username { get => _username; set => SetProperty(ref _username, value); }
    /// </code>
    /// </example>
    public class ZFormTextItemAttribute : ZFormItemAttribute
    {
        /// <summary>
        /// 输入框表单项
        /// </summary>
        /// <param name="title">标题</param>
        public ZFormTextItemAttribute(string title) : base(title) { }
    }
}
