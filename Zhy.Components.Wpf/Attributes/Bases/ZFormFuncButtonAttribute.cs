using System;
using Zhy.Components.Wpf.Attributes.Interfaces;
using Zhy.Components.Wpf.Enums;

namespace Zhy.Components.Wpf.Attributes.Bases
{
    /// <summary>
    /// 表单功能按钮特性
    /// </summary>
    public class ZFormFuncButtonAttribute : Attribute, IZFormFuncButton
    {
        /// <summary>
        /// 索引，用于排序
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 按钮内容
        /// </summary>
        public string ButtonContent { get; set; }
        /// <summary>
        /// 按钮样式
        /// </summary>
        public ZFormButtonStyle ButtonStyle { get; set; } = ZFormButtonStyle.DefaultButton;
        /// <summary>
        /// 表单功能按钮特性
        /// </summary>
        /// <param name="buttonContent">按钮标题</param>
        public ZFormFuncButtonAttribute(string buttonContent)
        {
            ButtonContent = buttonContent;
        }
    }
}
