/****************************************
 * FileName:	ZFormDateItemAttribute
 * Creater: 	shaozhy
 * Create Date:	2023/10/18 16:06:09
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using Zhy.Components.Wpf._Attribute._Base;

namespace Zhy.Components.Wpf._Attribute._ZFormItem
{
    /// <summary>
    /// 日期表单项
    /// </summary>
    public class ZFormDateItemAttribute : ZFormItemAttribute
    {
        /// <summary>
        /// 日期表单项
        /// </summary>
        /// <param name="title">标题</param>
        public ZFormDateItemAttribute(string title) : base(title)
        {

        }

        /// <summary>
        /// 日期格式
        /// </summary>
        public string DateFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";
    }
}
