using System.Windows;

namespace Zhy.Components.Wpf.Commons.Utils
{
    /// <summary>
    /// TextBox 工具
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
