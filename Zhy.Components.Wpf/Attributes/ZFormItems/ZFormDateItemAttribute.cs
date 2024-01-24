using Zhy.Components.Wpf.Attributes.Bases;

namespace Zhy.Components.Wpf.Attributes.ZFormItems
{
    /// <summary>
    /// 日期表单项
    /// </summary>
    /// <example>
    /// <code>
    /// [ZFormDateItem("创建日期")]
    /// public string CreateDate { get => _createDate; set => SetProperty(ref _createDate, value); }
    /// </code>
    /// </example>
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
