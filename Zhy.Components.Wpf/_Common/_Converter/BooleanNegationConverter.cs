/****************************************
 * FileName:	BooleanNegationConverter
 * Creater: 	shaozhy
 * Create Date:	2023/12/5 14:58:58
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Zhy.Components.Wpf._Common._Converter
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
            if(value is bool v)
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
