/****************************************
 * FileName:	ZItemAttribute
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 9:48:27
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Zhy.Components.Wpf._Attribute._Base
{
    /// <summary>
    /// 表单项特性
    /// </summary>
    public class ZFormItemAttribute : Attribute, IZFormItem
    {
        /// <summary>
        /// 索引，用于排序
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 成员路径
        /// 如为基础类型，则保持为空；如为引用类型，设置为相应成员属性名。
        /// </summary>
        public string MemberPath { get; set; }
        /// <summary>
        /// 是否为只读项
        /// </summary>
        public bool IsReadOnlyItem { get; set; }
        /// <summary>
        /// 表单项特性
        /// </summary>
        /// <param name="title">标题</param>
        public ZFormItemAttribute(string title)
        {
            Title = title;
        }
    }
}
