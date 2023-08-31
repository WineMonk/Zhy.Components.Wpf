/****************************************
 * FileName:	TextBoxHelper
 * Creater: 	shaozhy
 * Create Date:	2023/8/25 14:12:39
 * Version: 	v0.0.1
 * Description:	
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zhy.Components._Common._Utils
{
    /// <summary>
    /// 
    /// </summary>
    internal class TextBoxHelper : DependencyObject
    {
        public static string GetTextMark(DependencyObject obj)
        {
            return (string)obj.GetValue(TextMarkProperty);
        }

        public static void SetTextMark(DependencyObject obj, string value)
        {
            obj.SetValue(TextMarkProperty, value);
        }

        public static readonly DependencyProperty TextMarkProperty =
            DependencyProperty.RegisterAttached("TextMark", typeof(string), typeof(TextBoxHelper), new PropertyMetadata("请输入值"));

    }
}
