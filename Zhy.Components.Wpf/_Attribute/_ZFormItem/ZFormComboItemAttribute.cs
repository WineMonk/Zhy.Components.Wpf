/****************************************
 * FileName:	ZFormComboItemAttribute
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 9:55:13
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using Zhy.Components.Wpf._Attribute._Base;

namespace Zhy.Components.Wpf._Attribute._ZFormItem
{
    /// <summary>
    /// 下拉列表框表单项
    /// </summary>
    /// <example>
    /// <code>
    /// [ZFormComboItem("角 色", ItemsSourceProperty = nameof(Roles))]
    /// public string Role { get => _role; set => SetProperty(ref _role, value); }
    /// public List&lt;string&gt; Roles { get => _roles; set => _roles = value; }
    /// </code>
    /// </example>
    public class ZFormComboItemAttribute : ZFormItemAttribute
    {
        /// <summary>
        /// 下拉列表框表单项
        /// </summary>
        /// <param name="title">标题</param>
        public ZFormComboItemAttribute(string title) : base(title) { }
        /// <summary>
        /// 数据项源属性名
        /// </summary>
        public string ItemsSourceProperty { get; set; }
    }
}
