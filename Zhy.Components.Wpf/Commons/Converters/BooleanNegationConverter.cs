using System;
using System.Globalization;
using System.Windows.Data;

namespace Zhy.Components.Wpf.Commons.Converters
{
    /// <summary>
    /// 布尔取反转换器
    /// </summary>
    public class BooleanNegationConverter : IValueConverter
    {
        /// <summary>
        /// 转换
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool v)
            {
                return !v;
            }
            return value;
        }
        /// <summary>
        /// 反转
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
