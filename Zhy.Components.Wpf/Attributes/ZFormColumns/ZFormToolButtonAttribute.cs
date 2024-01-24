using System.Windows.Controls;
using Zhy.Components.Wpf.Attributes.Bases;
using Zhy.Components.Wpf.Enums;

namespace Zhy.Components.Wpf.Attributes.ZFormColumns
{
    /// <summary>
    /// 表单操作列功能按钮
    /// </summary>
    /// <example>
    /// <code>
    /// [ZFormToolButton("全 选", Index = 0, Dock = Dock.Right, ButtonStyle = ZFormButtonStyle.DefaultButton, Location = ButtonLocation.Bottom)]
    /// public RelayCommand&lt;IList?&gt; CommandCheckTotalItem => new RelayCommand&lt;IList?&gt;(CheckTotalItem);
    /// </code>
    /// </example>
    public class ZFormToolButtonAttribute : ZFormFuncButtonAttribute
    {
        /// <summary>
        /// 表单操作列功能按钮
        /// </summary>
        /// <param name="buttonContent">按钮标题</param>
        public ZFormToolButtonAttribute(string buttonContent) : base(buttonContent)
        {
        }

        /// <summary>
        /// 停靠位置，默认左侧
        /// </summary>
        public Dock Dock { get; set; } = Dock.Left;
        /// <summary>
        /// 按钮位置，默认表单上方
        /// </summary>
        public ButtonLocation Location { get; set; } = ButtonLocation.Top;
    }
}
